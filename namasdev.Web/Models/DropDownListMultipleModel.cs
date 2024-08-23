using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace namasdev.Web.Models
{
    public class DropDownListMultipleModel
    {
        public string Id { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }
        public IEnumerable Seleccionados { get; set; }
        public string OpcionVaciaTexto { get; set; }
        public string CssClass { get; set; }
        public Dictionary<string,string> HtmlAttributes { get; set; }
    }
}