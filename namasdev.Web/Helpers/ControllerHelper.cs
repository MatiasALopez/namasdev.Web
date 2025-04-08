using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using namasdev.Core.IO;
using namasdev.Core.Types;

namespace namasdev.Web.Helpers
{
    public class ControllerHelper
    {
        private Controller _controller;

        public ControllerHelper(Controller controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            _controller = controller;
        }

        public void CargarMensajesError(params string[] mensajes)
        {
            _controller.ViewBag.Errores = FormatearMensajes(mensajes);
        }

        public void CargarMensajeResultadoOk(string mensaje)
        {
            _controller.ViewBag.ResultadoOk = FormatearMensaje(mensaje);
        }

        public void CargarMensajeAlerta(string mensaje)
        {
            _controller.ViewBag.Alerta = FormatearMensaje(mensaje);
        }

        public void CargarMensajeInformacion(string mensaje)
        {
            _controller.ViewBag.Informacion = FormatearMensaje(mensaje);
        }

        private IEnumerable<string> FormatearMensajes(IEnumerable<string> mensajes)
        {
            return mensajes != null && mensajes.Any()
                ? mensajes.Select(FormatearMensaje).ToArray()
                : null;
        }

        private string FormatearMensaje(string mensaje)
        {
            return Formateador.Html(mensaje);
        }

        public ActionResult CrearActionResultArchivo(string archivoNombre, byte[] archivoContenido,
            bool descargar = true,
            string nombreAlternativo = null)
        {
            var result = new FileContentResult(archivoContenido, MimeMapping.GetMimeMapping(archivoNombre));
            if (descargar)
            {
                result.FileDownloadName = 
                    !string.IsNullOrWhiteSpace(nombreAlternativo)
                    ? nombreAlternativo
                    : Path.GetFileName(archivoNombre);
            }
            return result;
        }

        public ActionResult CrearActionResultArchivo(string archivoPath,
            bool descargar = true,
            string nombreAlternativo = null)
        {
            return CrearActionResultArchivo(archivoPath, File.ReadAllBytes(archivoPath), 
                descargar,
                nombreAlternativo);
        }

        public ActionResult CrearActionResultArchivo(Archivo archivo,
            bool descargar = true,
            string nombreAlternativo = null)
        {
            return CrearActionResultArchivo(archivo.Nombre, archivo.Contenido, 
                descargar,
                nombreAlternativo);
        }

        public byte[] ObtenerHttpPostedFileBytes(HttpPostedFileBase archivo)
        {
            if (archivo == null || archivo.ContentLength == 0)
            {
                throw new Exception("Debe seleccionar un archivo válido.");
            }

            var bytes = new byte[archivo.ContentLength];
            archivo.InputStream.Read(bytes, 0, bytes.Length);

            return bytes;
        }

        public Archivo ObtenerArchivoDesdeHttpPostedFile(HttpPostedFileBase archivo, 
            string descripcion = null,
            bool esRequerido = true)
        {
            if (archivo == null || archivo.ContentLength == 0)
            {
                if (esRequerido)
                {
                    throw new Exception($"Debes seleccionar un archivo {descripcion} válido.");
                }
                else
                {
                    return null;
                }
            }

            var bytes = new byte[archivo.ContentLength];
            archivo.InputStream.Read(bytes, 0, bytes.Length);

            return new Archivo { Nombre = archivo.FileName, Contenido = bytes };
        }

        public Archivo[] ObtenerArchivosDesdeHttpPostedFile(HttpPostedFileBase[] archivos, string descripcion,
            bool esRequerido = true)
        {
            if (archivos == null || !archivos.Any(a => a != null && a.ContentLength != 0))
            {
                if (esRequerido)
                {
                    throw new Exception(String.Format("Debe seleccionar al menos un archivo válido de {0}.", descripcion));
                }
                else
                {
                    return null;
                }
            }

            return archivos
                .Where(a => a != null && a.ContentLength > 0)
                .Select(a =>
                {
                    var bytes = new byte[a.ContentLength];
                    a.InputStream.Read(bytes, 0, bytes.Length);

                    return new Archivo { Nombre = a.FileName, Contenido = bytes };
                })
                .ToArray();
        }

        public string MensajesDeErrorDelModelState()
        {
            return String.Join("<br />", ObtenerErroresMensajesDeModelState());
        }

        public void AgruparMensajesDeErrorDelModelState()
        {
            var elementos = ObtenerElementosConErroresDeModelState(soloDePropiedades: true);

            var mensajes = new List<string>();
            foreach (var e in elementos)
            {
                foreach (var em in ObtenerErroresMensajesDeElemento(e))
                {
                    if (!mensajes.Contains(em))
                    {
                        mensajes.Add(em);

                        _controller.ModelState.AddModelError("", em);
                    }
                }

                e.Errors.Clear();
            }
        }

        private IEnumerable<string> ObtenerErroresMensajesDeModelState(
            bool soloDePropiedades = false)
        {
            var elementos = ObtenerElementosConErroresDeModelState(soloDePropiedades);
            return elementos
                .SelectMany(ObtenerErroresMensajesDeElemento)
                .ToArray();
        }

        private IEnumerable<ModelState> ObtenerElementosConErroresDeModelState(
            bool soloDePropiedades = false)
        {
            return _controller.ModelState
                .Where(ms =>
                    ms.Value.Errors.Count > 0
                    && (!soloDePropiedades || !string.IsNullOrWhiteSpace(ms.Key)))
                .Select(ms => ms.Value)
                .ToList();
        }

        private IEnumerable<string> ObtenerErroresMensajesDeElemento(ModelState elemento)
        {
            return elemento.Errors.Select(e => e.ErrorMessage).ToArray();
        }
    }
}