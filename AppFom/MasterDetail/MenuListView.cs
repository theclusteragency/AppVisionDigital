using System;
using AppFom.Helpers;
using Xamarin.Forms;

namespace AppFom.MasterDetail
{
    public class MenuListView : ListView
    {
        public MenuListView()
        {
            //this.BindingContext = RTD_Manager.VModel;// new VM_MenuListView();
            this.BindingContext = new VM_MenuListView();
            this.SetBinding(ListView.ItemsSourceProperty, new Binding("MenuListData", BindingMode.TwoWay));
            var viewTemplate = new DataTemplate(typeof(MenuCellBaster));
            //viewTemplate.SetBinding(MenuCell.TextProperty, "Title");
            //viewTemplate.SetBinding(MenuCell.ImageSourceProperty, "IconSource");
            //viewTemplate.SetBinding(MenuCell.IsEnabledProperty, "Enable");
            ItemTemplate = viewTemplate;

        }

    }

    public class MenuCellBaster : ViewCell
    {

        public MenuCellBaster()
        {
            var SL_Wrap = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, 0, 10, 0),
                Orientation = StackOrientation.Vertical
            };

            var SL_Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Spacing = 20
            };

            var Img_Ico = new Image() { HorizontalOptions = LayoutOptions.Start };
            Img_Ico.SetBinding(Image.SourceProperty, "IconSource");

            var Lbl_Title = new Label
            {
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start,
            };
            Lbl_Title.SetBinding(Label.TextProperty, "Title");
            Lbl_Title.WidthRequest = Fom.Screen.Width;

            SL_Content.Children.Add(Img_Ico);
            SL_Content.Children.Add(Lbl_Title);

            var Sl_Separator = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 1 };
            Sl_Separator.BackgroundColor = Color.White;

            SL_Wrap.Children.Add(SL_Content);

            this.View = SL_Wrap;
        }
    }

}