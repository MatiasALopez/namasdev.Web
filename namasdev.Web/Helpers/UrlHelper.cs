using System;
using System.Web;

namespace namasdev.Web.Helpers
{
    public class URLHelper
    {
        public const string ORDEN_SUFIJO_DESC = " desc";
        public const string NOMBRE_PARAMETRO_ORDEN = "orden";

        public static string GenerarUrlConPagina(Uri urlActual, int pagina)
        {
            return GenerarUrlConParametro(urlActual, "pagina", pagina.ToString());
        }

        public static string GenerarUrlConOrden(Uri urlActual, string orden,
            bool aplicarDescSoloAlPrimerElementoDeExpresion = false)
        {
            var qs = HttpUtility.ParseQueryString(urlActual.Query);
            
            orden = GenerarExpresionOrden(orden, qs[NOMBRE_PARAMETRO_ORDEN],
                 aplicarDescSoloAlPrimerElementoDeExpresion);

            return GenerarUrlConParametro(urlActual, NOMBRE_PARAMETRO_ORDEN, orden);
        }

        public static string GenerarExpresionOrden(string orden, string ordenActual,
            bool aplicarDescSoloAlPrimerElementoDeExpresion = false)
        {
            if (string.Equals(orden, ordenActual))
            {
                int iSeparador = orden.IndexOf(',');
                if (iSeparador >= 0)
                {
                    if (aplicarDescSoloAlPrimerElementoDeExpresion)
                    {
                        orden = orden.Insert(iSeparador, ORDEN_SUFIJO_DESC);
                    }
                    else
                    {
                        orden = orden.Replace(",", ORDEN_SUFIJO_DESC + ",") + ORDEN_SUFIJO_DESC;
                    }
                }
                else
                {
                    orden = orden + ORDEN_SUFIJO_DESC;
                }
            }

            return orden;
        }


        public static string GenerarUrlConOrdenPersonalizado(Uri urlActual, string nombreParametroOrden, string orden)
        {
            var qs = HttpUtility.ParseQueryString(urlActual.Query);
            if (String.Equals(qs[nombreParametroOrden], orden))
            {
                orden =
                    orden.Contains(",")
                    ? orden.Replace(",", ORDEN_SUFIJO_DESC + ",") + ORDEN_SUFIJO_DESC
                    : orden + ORDEN_SUFIJO_DESC;
            }

            return GenerarUrlConParametro(urlActual, nombreParametroOrden, orden);
        }

        public static string GenerarUrlConParametro(Uri urlActual, string nombreParametro, string valorParametro)
        {
            if (urlActual == null)
            {
                throw new ArgumentNullException("urlActual");
            }

            var url = urlActual.GetLeftPart(UriPartial.Path);
            var qs = HttpUtility.ParseQueryString(urlActual.Query);
            if (!String.IsNullOrWhiteSpace(valorParametro))
            {
                qs[nombreParametro] = valorParametro;
            }
            else
            {
                qs.Remove(nombreParametro);
            }

            return url + "?" + qs.ToString();
        }

        public static string GenerarRutaAbsoluta(string urlRutaRelativa)
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + urlRutaRelativa;
        }
    }
}