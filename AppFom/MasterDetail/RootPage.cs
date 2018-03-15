using System;
using AppFom.Helpers;
using AppFom.Pages;
using Xamarin.Forms;

namespace AppFom.MasterDetail
{
    public class RootPage : MasterDetailPage
    {
        MenuPage menuPage;

        public RootPage()
        {
            MasterBehavior = MasterBehavior.Popover;
            menuPage = new MenuPage();
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);
            Master = menuPage;
            //Detail = new NavigationPage(new PageMap()) { BarBackgroundColor = Fom.Colors.UIKitBlue, BarTextColor = Color.White }; 
            Detail = new NavigationPage(new PageEvents()) { BarBackgroundColor = Fom.Colors.UIKitGreen, BarTextColor = Color.White };

        }

        public RootPage(string root)
        {
            MasterBehavior = MasterBehavior.Popover;
            menuPage = new MenuPage();
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);
            Master = menuPage;
            Detail = new NavigationPage(new PageAccount()) { BarBackgroundColor = Fom.Colors.UIKitGreen, BarTextColor = Color.White }; ;
        }


        async void NavigateTo(MenuItem menu)
        {
            if (menu == null)
                return;


            switch (menu.Title)
            {
                case "Cerrar Sesión":
                    {

                        //var x = await Look.Dialogs.DisplayCautionMessageCloseSession();
                        //if (x)
                        //{
                        //	//Limpiamos objeto que contengan info de la sesion

                        //	var generic = new
                        //	{
                        //		availability = "0",
                        //		idAssociated = Look.Globals.IDASSOCIATED
                        //		//token = Look.Globals.AWSARN,
                        //		//latitude = Look.Globals.POSITION.Latitude,
                        //		//longitude = Look.Globals.POSITION.Longitude,
                        //	};
                        //	var result = await _associatedServices.UpdateAssociates(generic);


                        //	//await DependencyService.Get<IHelperServices>().ChangeRootAppPage("login",null);
                        //	Application.Current.MainPage = new Page_Login();
                        //}

                    }
                    break;
                default:
                    {
                        Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

                        Detail = new NavigationPage(displayPage) { BarBackgroundColor = Fom.Colors.UIKitGreen, BarTextColor = Color.White };

                        menuPage.Menu.SelectedItem = null;
                        IsPresented = false;

                    }
                    break;
            }
        }
    }
}