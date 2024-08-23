using System;
using System.Collections.Generic;
using System.Linq;

using namasdev.Core.Types;

namespace namasdev.Web.ViewModels
{
    public class ListadoViewModel<TItem>
        where TItem : class
    {
        public string Orden { get; set; }

        public List<TItem> Items { get; set; }

        public bool TieneItems
        {
            get { return Items != null && Items.Any(); }
        }

        public void OrdenarItems()
        {
            if (Items != null && !String.IsNullOrWhiteSpace(Orden))
            {
                Items = Items.AsQueryable()
                    .Ordenar(Orden)
                    .ToList();
            }
        }
    }
}