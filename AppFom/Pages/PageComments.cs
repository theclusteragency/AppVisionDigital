using System;
using System.Collections.Generic;
using AppFom.CellViews;
using AppFom.Helpers;
using AppFom.ViewModels;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageComments : ContentPage
    {
        #region Vars & Properties

        private StackLayout SlRoot = new StackLayout() { Padding = new Thickness(20) };

        private List<Comentario> lcomentarios;

        #endregion

        public PageComments(List<Comentario> comentarios, int idevento)
        {

            Title = "Comentarios";

            // Cargamos actividades
            lcomentarios = comentarios;

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

            // Comentario
            var slWrapComment = new StackLayout
            {
                BackgroundColor = Color.Gray,
                Padding = new Thickness(0, 1, 0, 0),
                Spacing = 0
            };

            var slComment = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.White,
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                Padding = new Thickness(10, 0, 10, 0)
            };

            var entryComment = new Entry
            {

                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = Fom.Screen.Height * .1,
                Placeholder = "..........."
            };
            entryComment.SetBinding(Entry.TextProperty, "TextMessage");
            slComment.Children.Add(entryComment);

            var tgComment = new TapGestureRecognizer();
            tgComment.SetBinding(TapGestureRecognizer.CommandProperty, "CommandComment");
            var imageSend = new Image { Source = ImageSource.FromResource("AppFom.Images.ico_btn_sendmsj.png"), Aspect = Aspect.AspectFit, VerticalOptions = LayoutOptions.Center };
            imageSend.GestureRecognizers.Add(tgComment);

            slComment.Children.Add(imageSend);

            slWrapComment.Children.Add(slComment);

            BgLayout.Children.Add(slWrapComment,
                            Constraint.Constant(0),
                            Constraint.Constant(Fom.Screen.Height * .78),
                            Constraint.RelativeToParent((Parent) =>
                            {
                                return Parent.Width;
                            }),
                            Constraint.Constant(Fom.Screen.Height * .1)
                       );

            this.BindingContext = new VMComments(this.Navigation, lcomentarios, idevento);

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
                HeightRequest = Fom.Screen.Height * .7,
            };

            var listActivities = new ListView();
            listActivities.BackgroundColor = Color.Transparent;
            listActivities.ItemTemplate = new DataTemplate(typeof(VCComment));
            listActivities.SetBinding(ListView.ItemsSourceProperty, "SourceListActivities");
            listActivities.SetBinding(ListView.SelectedItemProperty, "SelectedActivity");
            listActivities.RowHeight = 100;
            listActivities.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                ((ListView)sender).SelectedItem = null;
            };
            listActivities.SeparatorColor = Fom.Colors.UIKitOrange;

            slWrap.Children.Add(listActivities);

            root.Children.Add(slWrap);

        }
    }
}


