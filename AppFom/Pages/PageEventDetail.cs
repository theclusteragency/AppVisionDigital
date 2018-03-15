using System;
using System.Diagnostics;
using Acr.UserDialogs;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Interfaces;
using AppFom.Models;
using AppFom.ViewModels;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageEventDetail : ContentPage
    {
        #region Vars & Properties

        private StackLayout SlRoot = new StackLayout() { Padding = new Thickness(20) };
        private readonly IOperationServices Services;
        private EventDetail detail;

        #endregion


        public PageEventDetail(Event evento)
        {
            //NavigationPage.SetHasNavigationBar(this, false);

            try
            {
                Services = new OperationServices();
                Title = "Detalle Evento";

                Device.BeginInvokeOnMainThread(async () =>
                {

                    UserDialogs.Instance.ShowLoading();

                    // Solicitamos el detalle del evento
                    var result = await Services.GetEventDetail(evento.id_evento);
                    detail = result.data;

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
                                     Constraint.Constant(Fom.Screen.Height * .02),
                                     Constraint.RelativeToParent((Parent) =>
                                     {
                                         return Parent.Width;
                                     }),
                                     Constraint.RelativeToParent((Parent) =>
                                     {
                                         return Parent.Height;
                                     })
                                );

                    this.BindingContext = new VMEventDetail(this.Navigation, detail);//Pasar detalle de evento

                    Content = BgLayout;


                    UserDialogs.Instance.HideLoading();
                });

            }
            catch (Exception ex)
            {

                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.Message);
            }

        }


        public void ScreenBuilder(StackLayout root)
        {

            var SlWrap = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                //VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 20,
                Padding = new Thickness(0, 10, 0, 0),
                BackgroundColor = Color.Green
            };
            SlWrap.SetBinding(StackLayout.BackgroundColorProperty, "ColorDetailEvent");

            var SlContent = new StackLayout
            {
                BackgroundColor = Fom.Colors.UIKitWhite,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 20,
                Padding = new Thickness(10, 0, 10, 10)
            };


            var LblTitle = new Label
            {

                //Text = "Entrega de libros",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black
            };
            LblTitle.SetBinding(Label.TextProperty, "TextTitle");

            SlContent.Children.Add(LblTitle);

            var SLWrapCateg = new StackLayout
            {

                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
                Spacing = 10
            };

            var LblTitCateg = new Label
            {

                Text = "Categoria:",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitOrange
            };

            var LblTextCateg = new Label
            {

                //Text = "Categoria 01",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitGray
            };
            LblTextCateg.SetBinding(Label.TextProperty, "TextCategory");

            SLWrapCateg.Children.Add(LblTitCateg);
            SLWrapCateg.Children.Add(LblTextCateg);
            // SlContent.Children.Add(SLWrapCateg);

            var Line = new StackLayout
            {
                BackgroundColor = Fom.Colors.UIKitGray,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1
            };
            SlContent.Children.Add(Line);


            var tgLocation = new TapGestureRecognizer();
            tgLocation.SetBinding(TapGestureRecognizer.CommandProperty, "CommandLocation");
            var SLWrapPlace = new StackLayout
            {

                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Spacing = 10
            };
            SLWrapPlace.GestureRecognizers.Add(tgLocation);

            var LblTitPlace = new Label
            {

                Text = "Lugar:",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitOrange
            };

            var PinAddress = new Image
            {
                Source = ImageSource.FromResource("AppFom.Images.img_fom_pin.png"),
                WidthRequest = 25,
                HeightRequest = 31
            };
            //slAddress.Children.Add(PinAddress);


            var LblTextPlace = new Label
            {

                //Text = "Polanco Ansures",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitGray
            };
            LblTextPlace.SetBinding(Label.TextProperty, "TextPlace");

            //SLWrapPlace.Children.Add(LblTitPlace);
            SLWrapPlace.Children.Add(PinAddress);
            SLWrapPlace.Children.Add(LblTextPlace);
            //SlContent.Children.Add(SLWrapPlace);

            var LineTwo = new StackLayout
            {
                BackgroundColor = Fom.Colors.UIKitGray,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1
            };
            //SlContent.Children.Add(LineTwo);


            // Agregamos operadores 
            var LblTitOpers = new Label
            {

                Text = "Operadores responsable",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitOrange
            };

            //SlContent.Children.Add(LblTitOpers);

            var SlWrapOperator = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 20,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,

            };

            //var operators = new[] {
            //    new { name = "Jorge", url = "http://lorempixel.com/80/80/"}
            //    ,new { name = "Jorge", url = "http://lorempixel.com/80/80/" }
            //    ,new { name = "Pedro", url = "http://lorempixel.com/80/80/"}
            //    ,new { name = "Luis", url = "http://lorempixel.com/80/80/"}
            //    ,new { name = "Jorge", url = "http://lorempixel.com/80/80/" }
            //    ,new { name = "Jorge", url = "http://lorempixel.com/80/80/" }
            //    ,new { name = "Jorge", url = "http://lorempixel.com/80/80/" }
            //};
            //var operators = detail.operadores;

            //foreach (var item in operators)
            //{

            //    if (item.encargado == 1)
            //    {
            //        var SlOper = new StackLayout
            //        {
            //            Orientation = StackOrientation.Vertical,
            //            WidthRequest = 50,
            //            HeightRequest = 70,
            //            Spacing = 1
            //        };

            //        var ImgUser = new CircleImage
            //        {
            //            BorderColor = Fom.Colors.UIKitOrange,
            //            BorderThickness = 1,
            //            HeightRequest = 50,
            //            WidthRequest = 50,
            //            Aspect = Aspect.AspectFill,
            //            HorizontalOptions = LayoutOptions.Center,
            //            Source = string.IsNullOrEmpty(item.url_avatar) ? ImageSource.FromResource("AppFom.Images.img_fom_nopic.png") : ImageSource.FromUri(new Uri(item.url_avatar))
            //        };

            //        SlOper.Children.Add(ImgUser);

            //        var LblName = new Label
            //        {
            //            TextColor = Color.Gray,
            //            Text = item.nombre,
            //            FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
            //            HorizontalOptions = LayoutOptions.FillAndExpand,
            //            HorizontalTextAlignment = TextAlignment.Center,
            //            VerticalTextAlignment = TextAlignment.Center
            //        };

            //        SlOper.Children.Add(LblName);

            //        SlWrapOperator.Children.Add(SlOper);
            //    }
            //}

            //SlContent.Children.Add(new ScrollView { Orientation = ScrollOrientation.Horizontal, Content = SlWrapOperator });

            //var LineThree = new StackLayout
            //{
            //    BackgroundColor = Fom.Colors.UIKitGray,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    HeightRequest = 1
            //};
            //SlContent.Children.Add(LineThree);

            /*/ Agregamos analistas 
            var LblTitAnalistas = new Label
            {

                Text = "Analistas",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitOrange
            };

            SlContent.Children.Add(LblTitAnalistas);

            var SlWrapAnalist = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 20,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,

            };
                       
            var analists = detail.analistas;

            foreach (var item in analists)
            {
                var SlAnal = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    WidthRequest = 50,
                    HeightRequest = 70,
                    Spacing = 1
                };

                var ImgUser = new CircleImage
                {
                    BorderColor = Fom.Colors.UIKitOrange,
                    BorderThickness = 1,
                    HeightRequest = 50,
                    WidthRequest = 50,
                    Aspect = Aspect.AspectFill,
                    HorizontalOptions = LayoutOptions.Center,
                    Source = string.IsNullOrEmpty(item.url_avatar) ? ImageSource.FromResource("AppFom.Images.img_fom_nopic.png") : ImageSource.FromUri(new Uri(item.url_avatar))
                };

                SlAnal.Children.Add(ImgUser);

                var LblName = new Label
                {
                    TextColor = Color.Gray,
                    Text = item.nombre,
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };

                SlAnal.Children.Add(LblName);

                SlWrapAnalist.Children.Add(SlAnal);
            }

            SlContent.Children.Add(new ScrollView { Orientation = ScrollOrientation.Horizontal, Content = SlWrapAnalist });

            var LineFour = new StackLayout
            {
                BackgroundColor = Fom.Colors.UIKitGray,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1
            };
            SlContent.Children.Add(LineFour);
            */

            // Agregamos Galeria 
            var TgActivity = new TapGestureRecognizer();
            TgActivity.SetBinding(TapGestureRecognizer.CommandProperty, "GoActivity");
            var SlWrapActivities = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            SlWrapActivities.GestureRecognizers.Add(TgActivity);

            var LblTitActivities = new Label
            {
                Text = "Estadísticas",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitOrange
            };

            SlWrapActivities.Children.Add(LblTitActivities);

            var LblToActivities = new Label
            {
                Text = "  >  ",
                HorizontalOptions = LayoutOptions.End,
                HorizontalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitOrange
            };

            SlWrapActivities.Children.Add(LblToActivities);

            SlContent.Children.Add(SlWrapActivities);

            var LineAct = new StackLayout
            {
                BackgroundColor = Fom.Colors.UIKitGray,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1
            };
            SlContent.Children.Add(LineAct);

            // Agregamos Galeria 
            var TgGalery = new TapGestureRecognizer();
            TgGalery.SetBinding(TapGestureRecognizer.CommandProperty, "GoGalery");
            var SlWrapGalery = new StackLayout
            {

                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            SlWrapGalery.GestureRecognizers.Add(TgGalery);

            var LblTitGaleria = new Label
            {

                Text = "Galeria",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitOrange
            };

            SlWrapGalery.Children.Add(LblTitGaleria);

            var LblToGalery = new Label
            {
                Text = "  >  ",
                HorizontalOptions = LayoutOptions.End,
                HorizontalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitOrange
            };

            SlWrapGalery.Children.Add(LblToGalery);

            SlContent.Children.Add(SlWrapGalery);

            var LineFive = new StackLayout
            {
                BackgroundColor = Fom.Colors.UIKitGray,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1
            };
            SlContent.Children.Add(LineFive);

            // Agregamos Comentarios 
            var TgComments = new TapGestureRecognizer();
            TgComments.SetBinding(TapGestureRecognizer.CommandProperty, "GoComments");
            var SlWrapComments = new StackLayout
            {

                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            SlWrapComments.GestureRecognizers.Add(TgComments);

            var LblTitComments = new Label
            {

                Text = "Comentarios",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitOrange
            };

            SlWrapComments.Children.Add(LblTitComments);

            var LblToComments = new Label
            {
                Text = "  >  ",
                HorizontalOptions = LayoutOptions.End,
                HorizontalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitOrange
            };

            SlWrapComments.Children.Add(LblToComments);

            SlContent.Children.Add(SlWrapComments);

            var LineSix = new StackLayout
            {
                BackgroundColor = Fom.Colors.UIKitGray,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1
            };
            SlContent.Children.Add(LineSix);


            // Cambiar Status 
            var SlWrapStatus = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Spacing = 10
            };


            var BtnProgram = new Image { Source = ImageSource.FromResource("AppFom.Images.img_fom_btnprog.png"), Aspect = Aspect.AspectFit };

            var TgIniciar = new TapGestureRecognizer();
            TgIniciar.SetBinding(TapGestureRecognizer.CommandProperty, "CommandStart");
            var BtnIni = new Image { Source = ImageSource.FromResource("AppFom.Images.img_fom_btnini.png"), Aspect = Aspect.AspectFit };
            BtnIni.GestureRecognizers.Add(TgIniciar);

            var TgEnd = new TapGestureRecognizer();
            TgEnd.SetBinding(TapGestureRecognizer.CommandProperty, "CommandFinish");
            var BtnEnd = new Image { Source = ImageSource.FromResource("AppFom.Images.img_fom_btnend.png"), Aspect = Aspect.AspectFit };
            BtnEnd.GestureRecognizers.Add(TgEnd);

            SlWrapStatus.Children.Add(BtnIni);
            SlWrapStatus.Children.Add(BtnEnd);
            //SlWrapStatus.Children.Add(BtnProgram); // no se muestra pro que ya es programado

            SlContent.Children.Add(SlWrapStatus);



            SlWrap.Children.Add(new ScrollView { Content = SlContent });
            root.Children.Add(SlWrap);
        }
    }
}


