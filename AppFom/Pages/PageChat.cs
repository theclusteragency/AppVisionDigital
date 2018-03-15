using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AppFom.CellViews;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Models;
using AppFom.ViewModels;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageChat : ContentPage
    {
        #region Vars & Properties

        private StackLayout SlRoot = new StackLayout() { Padding = new Thickness(20) };

        #endregion


        public PageChat()
        {
            //NavigationPage.SetHasNavigationBar(this, false);
            Title = "Chat";
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

            this.BindingContext = new VMChat(this.Navigation);

            Content = BgLayout;
        }


        public async Task ScreenBuilder(StackLayout root)
        {

            try
            {

                UserDialogs.Instance.ShowLoading();

                var Services = new OperationServices();

                var request = await Services.getAllUsers();

                var users = new ObservableCollection<User>();
                foreach (var item in request.data)
                {
                    if (item.id_rol == 1)
                        users.Add(item);
                }


                var slWrap = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Spacing = 20,
                    Padding = new Thickness(0, 0, 0, 0)
                };

                var listUsers = new ListView();
                listUsers.BackgroundColor = Color.Transparent;
                listUsers.ItemTemplate = new DataTemplate(typeof(VCUser));
                listUsers.ItemsSource = users;
                //listUsers.SetBinding(ListView.ItemsSourceProperty, "SourceUsers");
                //listEvents.ItemsSource = Fom.Globals.MISEVENTOS;
                listUsers.SetBinding(ListView.SelectedItemProperty, "SelectedUser");
                //listEvents.SetBinding(ListView.IsRefreshingProperty, "IsBusy");
                listUsers.RowHeight = 120;
                listUsers.ItemSelected += (sender, e) =>
                {
                    if (e.SelectedItem == null)
                        return;

                    ((ListView)sender).SelectedItem = null;
                };
                listUsers.SeparatorColor = Fom.Colors.UIKitOrange;

                slWrap.Children.Add(listUsers);

                UserDialogs.Instance.HideLoading();

                root.Children.Add(slWrap);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.Message);
            }
        }

    }
}


