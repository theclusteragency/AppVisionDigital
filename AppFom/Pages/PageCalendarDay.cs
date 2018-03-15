using System;
using System.Collections.Generic;
using AppFom.CellViews;
using AppFom.Models;
using AppFom.ViewModels;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageCalendarDay : ContentPage
    {
        #region Vars & Properties

        private StackLayout SlRoot = new StackLayout() { Padding = new Thickness(20) };

        private List<Event> leventos;

        #endregion

        public PageCalendarDay(DateTime dateselect, List<Event> eventos)
        {

            Title = String.Format("{0:m}", dateselect);

            // Cargamos eventos
            leventos = eventos;

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

            this.BindingContext = new VMCalendarDay(this.Navigation);

            Content = BgLayout;

        }

        public void ScreenBuilder(StackLayout root)
        {

            var slWrap = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 20,
                Padding = new Thickness(0, 0, 0, 0)
            };

            var listEvents = new ListView();
            listEvents.BackgroundColor = Color.Transparent;
            listEvents.ItemTemplate = new DataTemplate(typeof(VCEvent));
            //listEvents.SetBinding(ListView.ItemsSourceProperty, "SourceListBranch");
            listEvents.ItemsSource = leventos;
            listEvents.SetBinding(ListView.SelectedItemProperty, "SelectedEvents");
            //listEvents.SetBinding(ListView.SelectedItemProperty, "SelectedEvents");
            //listEvents.SetBinding(ListView.IsRefreshingProperty, "IsBusy");
            listEvents.RowHeight = 120;
            listEvents.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                ((ListView)sender).SelectedItem = null;
            };
            listEvents.SeparatorColor = Color.Transparent;

            slWrap.Children.Add(listEvents);

            root.Children.Add(slWrap);

        }
    }
}

