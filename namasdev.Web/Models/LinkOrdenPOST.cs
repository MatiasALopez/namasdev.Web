
namespace namasdev.Web.Models
{
    public class LinkOrdenPOST
    {
        public string Orden { get; set; }
        public string Texto { get; set; }
        public string Operacion { get; set; }
        public bool AplicarDescSoloAlPrimerElementoDeExpresion { get; set; }
    }
}