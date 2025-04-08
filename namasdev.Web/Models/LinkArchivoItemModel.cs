using System;
using System.IO;
using System.Linq;

using namasdev.Core.IO;
using namasdev.Core.Types;

namespace namasdev.Web.Models
{
    public class LinkArchivoItemModel
    {
        private static readonly string[] EXTENSIONES_VISUALIZABLES = new[] 
        { 
            ArchivoExtensiones.Application.PDF, 
            ArchivoExtensiones.Image.GIF,
            ArchivoExtensiones.Image.JPG,
            ArchivoExtensiones.Image.JPE,
            ArchivoExtensiones.Image.JPEG,
            ArchivoExtensiones.Image.PNG,
            ArchivoExtensiones.Image.TIF,
            ArchivoExtensiones.Image.TIFF
        };

        public string ArchivoURL { get; set; }
        public string ClassCss { get; set; }
        
        public string Nombre
        {
            get { return Formateador.NombreArchivoDesdeUrl(ArchivoURL); }
        }

        public bool SePuedeVisualizar
        {
            get { return EXTENSIONES_VISUALIZABLES.Contains(Path.GetExtension(ArchivoURL), StringComparer.CurrentCultureIgnoreCase); }
        }

        public string LinkDescargaURLBase { get; set; }

        public string LinkDescargaURL
        {
            get { return LinkDescargaURLBase + $"&descargar={(!SePuedeVisualizar).ToString().ToLower()}"; }
        }
    }
}