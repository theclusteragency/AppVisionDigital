using System;
using System.Collections.Generic;
using AppFom.CellViews;
using AppFom.Helpers;
using AppFom.ViewModels;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageGalery : ContentPage
    {
        #region Vars & Properties

        private StackLayout SlRoot = new StackLayout() { Padding = new Thickness(20) };

        private List<Foto> lfotos;

        #endregion

        public PageGalery(List<Foto> fotos, int idevent)
        {

            Title = "Fotos";

            // Cargamos actividades
            lfotos = fotos;

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


            var tgPhoto = new TapGestureRecognizer();
            tgPhoto.SetBinding(TapGestureRecognizer.CommandProperty, "CommandPhoto");
            var BgBtnAddPhoto = new Image { Source = ImageSource.FromResource("AppFom.Images.img_btn_addphoto.png"), Aspect = Aspect.AspectFit };
            BgBtnAddPhoto.GestureRecognizers.Add(tgPhoto);

            BgLayout.Children.Add(BgBtnAddPhoto,
                                  Constraint.Constant((Fom.Screen.Width / 2) - (256 * .5 / 2)),
                            Constraint.Constant(Fom.Screen.Height * .8),
                            Constraint.Constant(256 * .5),
                            Constraint.Constant(58 * .5)
                       );

            this.BindingContext = new VMGalery(this.Navigation, fotos, idevent);

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

            var listFotos = new ListView();
            listFotos.BackgroundColor = Color.Transparent;
            listFotos.ItemTemplate = new DataTemplate(typeof(VCPhoto));
            listFotos.SetBinding(ListView.ItemsSourceProperty, "SourceListPhotos");
            //listFotos.ItemsSource = lfotos;
            listFotos.SetBinding(ListView.SelectedItemProperty, "SelectedPhoto");
            //listEvents.SetBinding(ListView.SelectedItemProperty, "SelectedEvents");
            //listEvents.SetBinding(ListView.IsRefreshingProperty, "IsBusy");
            listFotos.RowHeight = 100;
            listFotos.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                ((ListView)sender).SelectedItem = null;
            };
            //listActivities.SeparatorColor = Color.Transparent;

            slWrap.Children.Add(listFotos);

            root.Children.Add(slWrap);

        }
    }
}



