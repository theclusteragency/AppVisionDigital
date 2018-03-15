using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Models;
using Xamarin.Forms;
using XamForms.Controls;

namespace AppFom.Pages
{
    public class PageCalendar : ContentPage
    {
        #region Vars & Properties

        private StackLayout SlRoot = new StackLayout() { Padding = new Thickness(20) };
        private List<Event> leventos;

        #endregion

        public PageCalendar()
        {
            Title = "Calendario";

            var BgLayout = new RelativeLayout();

            //var BgImage = new Image { Source = ImageSource.FromResource("AppFom.Images.bg_fom_blelogin.png"), Aspect = Aspect.AspectFill };
            var BgImage = new Image { Source = ImageSource.FromResource("AppFom.Images.bg_fom_blelogin_green.png"), Aspect = Aspect.AspectFill };

            BgLayout.Children.Add(BgImage,
                              Constraint.Constant(0),
                              Constraint.Constant(0),
                              Constraint.RelativeToParent((Parent) =>
                              {
                                  return Parent.Width;
                              }),
                              Constraint.RelativeToParent((Parent) =>
                              {
                                  return Parent.Height;
                              })
                         );




            // Contruye pantalla
            ScreenBuilder(SlRoot);
            BgLayout.Children.Add(SlRoot,
                             Constraint.Constant(0),
                             Constraint.Constant(0),
                             Constraint.RelativeToParent((Parent) =>
                             {
                                 return Parent.Width;
                             }),
                             Constraint.RelativeToParent((Parent) =>
                             {
                                 return Parent.Height;
                             })
                        );

            //this.BindingContext = new VMLogin(this.Navigation);

            Content = BgLayout;

        }

        public async Task ScreenBuilder(StackLayout root)
        {

            // Pedimos eventos
            //var services = new OperationServices();
            //var events = await services.GetOperEvents();

            leventos = Fom.Globals.MISEVENTOS;// events.data;

            var frCalendar = new Frame
            {

                CornerRadius = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 0, 0, 20)
            };

            var calendar = new Calendar()
            {
                WidthRequest = Fom.Screen.Width,
                BackgroundColor = Fom.Colors.UIKitWhite,

            };

            // Agrupamos los evntos por fecha
            var grupedEvents = leventos
                .GroupBy(u => u.fecha_evento)
                .Select(grp => grp.ToList())
                .ToList();


            // Cargamos eventos en calendario
            foreach (var item in grupedEvents)
            {

                // Obtenemos los badge y su color
                var lbadge = new List<Pattern>();
                foreach (var ev in item)
                {

                    Color colorBadge = Color.Gray;
                    // Obtenemos color del badge
                    if (ev.estatus == "Programado")
                    {
                        colorBadge = Color.Green;
                    }
                    else if (ev.estatus == "Iniciado")
                    {
                        colorBadge = Color.Blue;
                    }
                    else if (ev.estatus == "Finalizado")
                    {
                        colorBadge = Color.Red;
                    }

                    lbadge.Add(new Pattern { WidthPercent = 1f, HightPercent = 0.25f, Color = colorBadge });
                }


                // Obtenemos la fecha del evento de su primer objeto
                calendar.SpecialDates.Add(new SpecialDate(Convert.ToDateTime(item[0].fecha_evento).AddDays(1))
                {
                    Selectable = true,
                    BackgroundPattern = new BackgroundPattern(1)
                    {

                        Pattern = lbadge
                        //new List<Pattern>
                        //{

                        //    new Pattern { WidthPercent = 1f, HightPercent = 0.25f, Color = colorBadge }                                
                        //   // new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Purple},
                        //   // new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Green},
                        //   // new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Yellow,Text = "Test", TextColor=Color.DarkBlue, TextSize=11, TextAlign=TextAlign.Middle}
                        //}
                    }
                });
            }

            calendar.DateClicked += async (sender, e) =>
            {

                var dateSelect = Convert.ToDateTime(calendar.SelectedDate).ToString("dd/MM/yyyy");
                Debug.WriteLine(dateSelect);

                var eventsSelect = leventos.Where(item => Convert.ToDateTime(item.fecha_evento).AddDays(1).ToString("dd/MM/yyyy") == dateSelect).ToList();
                await Navigation.PushAsync(new PageCalendarDay(Convert.ToDateTime(dateSelect), eventsSelect));
            };

            frCalendar.Content = calendar;
            root.Children.Add(frCalendar);
        }
    }
}

