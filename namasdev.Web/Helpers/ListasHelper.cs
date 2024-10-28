using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using namasdev.Core.Types;
using namasdev.Tipos;

namespace namasdev.Web.Helpers
{
    public class ListasHelper
    {
        public static SelectList ObtenerDiasSelectList()
        {
            var items = Enumerable.Range(0, 7)
                .Select(dia => new SelectListItem
                {
                    Text = Formateador.FormatoDia(dia),
                    Value = dia.ToString(),
                });
            return CrearSelectListDesdeItems(items);
        }

        public static IEnumerable<SelectListItem> ObtenerHorasSelectListItems(
            IEnumerable<string> seleccionados = null)
        {
            var items = new List<SelectListItem>();
            var hora = new TimeSpan();
            string horaTexto;
            while (true)
            {
                horaTexto = Formateador.FormatoHora(hora);
                items.Add(new SelectListItem
                {
                    Text = horaTexto,
                    Value = horaTexto,
                    Selected = seleccionados != null && seleccionados.Contains(horaTexto)
                });

                if (hora != new TimeSpan(23, 30, 0))
                {
                    hora = hora.Add(TimeSpan.FromMinutes(30));
                }
                else
                {
                    break;
                }
            }
            return CrearSelectListDesdeItems(items);
        }
        
        public static SelectList ObtenerMesesSelectList(IEnumerable<MesYAño> meses)
        {
            return CrearSelectListDesdeLista(
                meses, 
                m => new SelectListItem { Value = m.AñoYMes, Text = m.ToString() });
        }

        public static SelectList ObtenerSiNoSelectList(
            bool? valorSeleccionado = null)
        {
            return CrearSelectListDesdeItems(new List<SelectListItem>
                {
                    new SelectListItem { Text = Formateador.FormatoSiNo(true), Value = true.ToString() },
                    new SelectListItem { Text = Formateador.FormatoSiNo(false), Value = false.ToString() },
                },
                valorSeleccionado?.ToString());
        }

        public static SelectList ObtenerHoraMinSelectList()
        {
            var horas = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                horas.Add(i.ToString().PadLeft(2, '0') + ":00");
                horas.Add(i.ToString().PadLeft(2, '0') + ":30");
            }

            var items = new List<SelectListItem>();

            foreach (var hora in horas)
            {
                items.Add(new SelectListItem { Text = hora, Value = hora });
            }          

            return CrearSelectListDesdeItems(items);
        }

        public static SelectList ObtenerMesesSelectList()
        {
            var nombreMeses = DateTimeFormatInfo.CurrentInfo.MonthNames
                .Take(12)
                .Select((name, index) => new SelectListItem
                {
                    Value = (index + 1).ToString(),
                    Text = name,
                })
                .ToList();

            return CrearSelectListDesdeItems(nombreMeses);
        }

        public static SelectList ObtenerAñosHastaAñoActualSelectList(int añoDesde)
        {
            int añoActual = DateTime.Now.Year;
            var años = new List<int>();
            for (int año = añoDesde; año <= añoActual; año++)
            {
                años.Add(año);
            }

            return CrearSelectListDesdeLista(años);
        }

        public static IEnumerable<SelectListItem> ObtenerRolesSelectListItems(IEnumerable<string> roles,
            IEnumerable<string> seleccionados = null)
        {
            return roles
                .Select(r => new SelectListItem
                {
                    Text = r,
                    Value = r,
                    Selected = seleccionados != null && seleccionados.Contains(r)
                })
                .ToArray();
        }

        public static SelectList ObtenerAñosSelectList(IEnumerable<short> años)
        {
            return CrearSelectListDesdeLista(
                años,
                a => new SelectListItem
                {
                    Text = a.ToString(),
                    Value = a.ToString()
                });
        }

        public static SelectList ObtenerMesesSelectList(IEnumerable<short> meses)
        {
            return CrearSelectListDesdeLista(
                meses,
                a => new SelectListItem
                {
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(a),
                    Value = a.ToString()
                });
        }

        public static SelectList ObtenerTrimestresSelectList()
        {
            var items = new List<SelectListItem>();

            for (int i = 1; i <= 4; i++)
            {
                items.Add(new SelectListItem { Text = $"{i}Q", Value = i.ToString() });
            }

            return new SelectList(items, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> ObtenerMesesDelTrimestreSelectListItems(
            TrimestreYAño trimestre,
            IEnumerable<short> seleccionados = null)
        {
            return trimestre
                .ObtenerMesesDeTrimestre()
                .Select(t => new SelectListItem
                {
                    Text = t.MesNombre.Capitalize(),
                    Value = t.Mes.ToString(),
                    Selected = seleccionados != null && seleccionados.Contains(t.Mes)
                })
                .ToList();
        }

        public static SelectList ObtenerSelectListVacio()
        {
            return CrearSelectListDesdeItems(new SelectListItem[0]);
        }

        public static SelectList CrearSelectListDesdeLista<T>(IEnumerable<T> items,
            Func<T, SelectListItem> selector)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            return new SelectList(items.Select(selector), "Value", "Text");
        }

        public static SelectList CrearSelectListDesdeLista(IEnumerable items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            return new SelectList(items);
        }

        public static SelectList CrearSelectListDesdeItems(IEnumerable<SelectListItem> items,
            string valorSeleccionado = null)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            return new SelectList(items, "Value", "Text", valorSeleccionado);
        }
    }
}