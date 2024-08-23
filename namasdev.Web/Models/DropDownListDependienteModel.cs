using System.Collections;
using System.Collections.Generic;

namespace namasdev.Web.Models
{
    public class DropDownListDependienteModel
    {
        public string Id { get; set; }
        public IEnumerable Seleccionados { get; set; }
        public string OpcionVaciaTexto { get; set; }
        public string CssClass { get; set; }
        public string IdComboPadre { get; set; }
        public string UrlBusquedaItems { get; set; }
        public string ItemsAddedCallback { get; set; }
        public Dictionary<string, string> HtmlAttributes { get; set; }
    }
}