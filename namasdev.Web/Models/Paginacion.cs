
namespace namasdev.Web.Models
{
    public class Paginacion
    {
        public Paginacion(int paginaActual, int cantItemsPaginaActual, int cantTotalItems, int cantTotalPaginas)
        {
            PaginaActual = paginaActual > 0 ? paginaActual : 1;
            CantItemsPaginaActual = cantItemsPaginaActual;
            CantTotalItems = cantTotalItems;
            CantTotalPaginas = cantTotalPaginas;
        }

        public int PaginaActual { get; private set; }

        public int CantItemsPaginaActual { get; private set; }

        public int CantTotalItems { get; private set; }

        public int CantTotalPaginas { get; private set; }

        public bool TieneUnicaPagina
        {
            get { return CantTotalPaginas == 1; }
        }

        public bool TienePaginaAnterior
        {
            get { return PaginaActual > 1; }
        }

        public bool TienePaginaSiguiente
        {
            get { return PaginaActual < CantTotalPaginas; }
        }

        public int PaginaAnterior
        {
            get
            {
                return TienePaginaAnterior
                    ? PaginaActual - 1
                    : 1;
            }
        }

        public int PaginaSiguiente
        {
            get
            {
                return TienePaginaSiguiente
                    ? PaginaActual + 1
                    : CantTotalPaginas;
            }
        }
    }
}