using System;
using System.Collections.Generic;
using AppFom.CellViews;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageActivities : ContentPage
    {
        #region Vars & Properties

        private StackLayout SlRoot = new StackLayout() { Padding = new Thickness(20) };

        private List<Actividade> lactividades;

        #endregion

        public PageActivities(List<Actividade> actividades)
        {

            Title = "Estadísticas";

            // Cargamos actividades
            lactividades = actividades;

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

            //this.BindingContext = new VMCalendarDay(this.Navigation);

            Content = BgLayout;

        }

        public void ScreenBuilder(StackLayout root)
        {

            var slWrap = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 20,
                Padding = new Thickness(0, 0, 0, 0),
                BackgroundColor = Color.Transparent
            };

            var listActivities = new ListView();
            listActivities.BackgroundColor = Color.Transparent;
            listActivities.ItemTemplate = new DataTemplate(typeof(VCActivity));
            //listEvents.SetBinding(ListView.ItemsSourceProperty, "SourceListBranch");
            listActivities.ItemsSource = lactividades;
            listActivities.SetBinding(ListView.SelectedItemProperty, "SelectedActivity");
            //listEvents.SetBinding(ListView.SelectedItemProperty, "SelectedEvents");
            //listEvents.SetBinding(ListView.IsRefreshingProperty, "IsBusy");
            listActivities.RowHeight = 75;
            listActivities.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                //((ListView)sender).SelectedItem = null;
                await Navigation.PushAsync(new PageReporte());

            };
            //listActivities.SeparatorColor = Color.Transparent;

            slWrap.Children.Add(listActivities);

            root.Children.Add(slWrap);

        }
    }
}

