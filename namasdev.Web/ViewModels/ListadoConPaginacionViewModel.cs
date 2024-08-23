using System;
using System.Linq;

using namasdev.Core.Types;
using namasdev.Web.Models;

namespace namasdev.Web.ViewModels
{
    public class ListadoConPaginacionViewModel<TItem> : ListadoViewModel<TItem>
        where TItem : class
    {
        public int Pagina { get; set; }
        public int ItemsPorPagina { get; set; }

        public Paginacion Paginacion { get; set; }

        public void CargarPaginacion(int cantTotalItems)
        {
            if (TieneItems)
            {
                Paginacion = new Paginacion(Pagina, Items.Count, cantTotalItems, (int)Math.Ceiling((decimal)cantTotalItems / ItemsPorPagina));
            }
        }

        public void CargarPaginacion(OrdenYPaginacionParametros op)
        {
            CargarPaginacion(op.CantRegistrosTotales);
        }

        public void OrdenarYPaginarItems()
        {
            if (Items == null)
            {
                return;
            }

            int cantTotalRegistros = Items.Count;

            Items = Items.AsQueryable()
                .Ordenar(Orden)
                .Paginar(Pagina, ItemsPorPagina)
                .ToList();

            CargarPaginacion(cantTotalRegistros);
        }

        public OrdenYPaginacionParametros CrearOrdenYPaginacionParametros()
        {
            return new OrdenYPaginacionParametros
            {
                Orden = Orden,
                Pagina = Pagina,
                CantRegistrosPorPagina = ItemsPorPagina
            };
        }
    }
}