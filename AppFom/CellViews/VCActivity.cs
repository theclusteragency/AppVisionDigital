using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AppFom.Helpers;
using AppFom.Implementations;
using Xamarin.Forms;

namespace AppFom.CellViews
{
    public class VCActivity : ViewCell
    {
        public VCActivity()
        {
            Actividade Act = new Actividade();

            Tapped += (sender, e) =>
            {
                this.View.BackgroundColor = Color.Transparent;
            };

            var slWrap = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Spacing = 10
            };


            var img = new Image
            {
                Source = ImageSource.FromResource("AppFom.Images.img_fom_report.png"),
                //Aspect = Aspect.AspectFit
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 55,
                HeightRequest = 55
            };

            slWrap.Children.Add(img);

            var LblActivity = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
                LineBreakMode = LineBreakMode.TailTruncation,
                WidthRequest = Fom.Screen.Width - 140,
                //BackgroundColor = Color.Red
            };
            LblActivity.SetBinding(Label.TextProperty, "descripcion");
            slWrap.Children.Add(LblActivity);


            var entryValue = new Entry
            {
                Placeholder = "...",
                WidthRequest = 70,
                //HeightRequest = 40,
                //BackgroundColor = Color.Green,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            entryValue.SetBinding(Entry.TextProperty, "valor");

            var swActivity = new Switch()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            swActivity.BindingContextChanged += (sender, i) =>
            {
                base.OnBindingContextChanged();
                dynamic c = BindingContext;
                if (c != null)
                {
                    Act = (Actividade)c;

                    if (c.hecha == 1)
                    {
                        swActivity.IsToggled = true;
                    }
                    else
                    {

                        swActivity.IsToggled = false;
                    }
                }
            };

            swActivity.Toggled += async (sender, e) =>
            {
                Act.hecha = swActivity.IsToggled ? 1 : 0;
                await UpdateActivity(Act, entryValue.Text);
            };
            // TODO UPDATE ACTIVITY AFTER CHECKED IT

            //entryValue.TextChanged += async (sender, e) =>
            //{

            //    await Update(Act, sender);
            //};

            //slWrap.Children.Add(entryValue);
            //slWrap.Children.Add(swActivity);

            this.View = slWrap;
        }

        public async Task UpdateActivity(Actividade act, string value)
        {

            Debug.WriteLine("Actualizar actividad :" + act.id_actividad);
            try
            {
                var Services = new OperationServices();


                UserDialogs.Instance.ShowLoading();

                var generic = new
                {
                    id_evento_actividad = act.id_actividad,
                    hecho = 1,
                    id_usuario = Fom.Globals.USERFOM.id_usuario,
                    valor = string.IsNullOrEmpty(value) ? " " : value
                };
                // Terminar evento
                await Services.UpdStatusActivity(generic);


                UserDialogs.Instance.HideLoading();


            }
            catch (Exception ex)
            {

                Debug.WriteLine("Error en login : " + ex.Message);
                UserDialogs.Instance.HideLoading();
            }
        }

        public async Task Update(Actividade act, object sender)
        {

            try
            {
                var Services = new OperationServices();
                var entry = (Entry)sender;

                //UserDialogs.Instance.ShowLoading();

                var generic = new
                {
                    id_evento_actividad = act.id_actividad,
                    hecho = 1,
                    id_usuario = Fom.Globals.USERFOM.id_usuario,
                    valor = entry.Text
                };
                // Terminar evento
                await Services.UpdStatusActivity(generic);


                //UserDialogs.Instance.HideLoading();


            }
            catch (Exception ex)
            {

                Debug.WriteLine("Error en login : " + ex.Message);
                UserDialogs.Instance.HideLoading();
            }
        }
    }

}
