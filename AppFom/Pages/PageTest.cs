using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AppFom.Helpers;
using AppFom.Models;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageTest : ContentPage
    {
        public PageTest()
        {

            // Titulo de la pagina
            Title = "Calendario";


            // Agrupamos los evntos por fecha
            var grupedEvents = Fom.Globals.MISEVENTOS
                .GroupBy(u => u.fecha_evento)
                .Select(grp => grp.ToList())
                .ToList();


            // Cargamos eventos en calendario
            var lbadge = new List<Appointment>();
            foreach (var item in grupedEvents)
            {
                int h = 1;
                int hh = 2;
                // Obtenemos los badge y su color
                foreach (var ev in item)
                {

                    h++;
                    hh++;
                    var badage = new Appointment();
                    // Obtenemos color del badge
                    if (ev.estatus == "Programado")
                    {
                        badage.Title = ev.descripcion;
                        badage.Detail = ev.direccion;
                        badage.StartDate = Convert.ToDateTime(ev.fecha_evento).AddHours(-Convert.ToDateTime(ev.fecha_evento).Hour + h);
                        badage.EndDate = Convert.ToDateTime(ev.fecha_evento).AddHours(-Convert.ToDateTime(ev.fecha_evento).Hour + hh);
                        badage.Color = Color.Green;
                        badage.Evento = ev;
                    }
                    else if (ev.estatus == "Iniciado")
                    {
                        badage.Title = ev.descripcion;
                        badage.Detail = ev.direccion;
                        badage.StartDate = Convert.ToDateTime(ev.fecha_evento).AddHours(-Convert.ToDateTime(ev.fecha_evento).Hour + h);
                        badage.EndDate = Convert.ToDateTime(ev.fecha_evento).AddHours(-Convert.ToDateTime(ev.fecha_evento).Hour + hh);
                        badage.Color = Color.Blue;
                        badage.Evento = ev;
                    }
                    else if (ev.estatus == "Finalizado")
                    {
                        badage.Title = ev.descripcion;
                        badage.Detail = ev.direccion;
                        badage.StartDate = Convert.ToDateTime(ev.fecha_evento).AddHours(-Convert.ToDateTime(ev.fecha_evento).Hour + h);
                        badage.EndDate = Convert.ToDateTime(ev.fecha_evento).AddHours(-Convert.ToDateTime(ev.fecha_evento).Hour + hh);
                        badage.Color = Color.Red;
                        badage.Evento = ev;
                    }

                    lbadge.Add(badage);
                }

            }
            // >> Usar solo si queremo especifica rfecha a mostar
            //var date = new DateTime(2017, 4, 12);

            var calendar = new RadCalendar
            {
                // DisplayDate = date,
                AppointmentsSource = lbadge
            };

            calendar.NativeControlLoaded += (sender, e) =>
            {
                calendar.TrySetViewMode(CalendarViewMode.Day);
            };

            calendar.AppointmentTapped += async (sender, e) =>
            {
                var app = (Appointment)e.Appointment;
                Debug.WriteLine(app.Evento.descripcion);
                await Navigation.PushAsync(new PageEventDetail(app.Evento));

            };

            this.Content = calendar;

        }

        // >> calendar-getting-started-appointment-class
        public class Appointment : IAppointment
        {
            public DateTime StartDate { get; set; }

            public Color Color { get; set; }

            public DateTime EndDate { get; set; }

            public string Title { get; set; }

            public bool IsAllDay { get; set; }

            public string Detail { get; set; }

            public Event Evento { get; set; }
        }
        // << calendar-getting-started-appointment-class
    }
}
