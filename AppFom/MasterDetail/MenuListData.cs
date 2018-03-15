using System;
using System.Collections.Generic;
using AppFom.Pages;

namespace AppFom.MasterDetail
{
    public class MenuListData : List<MenuItem>
    {
        public MenuListData()
        {
            this.Add(new MenuItem()
            {
                Title = "Mis eventos",
                IconSource = "ico_menu_events",
                TargetType = typeof(PageEvents),
                Enable = true
            });

            this.Add(new MenuItem()
            {
                Title = "Calendario",
                IconSource = "ico_menu_calendar",
                TargetType = typeof(PageTest),
                Enable = true
            });

            this.Add(new MenuItem()
            {
                Title = "Mapa",
                IconSource = "ico_menu_map",
                TargetType = typeof(PageMap),
                Enable = true
            });

            this.Add(new MenuItem()
            {
                Title = "Chat",
                IconSource = "ico_menu_chat",
                TargetType = typeof(PageChat),
                Enable = true
            });


            //this.Add(new MenuItem()
            //{
            //    Title = "Calendario",
            //    IconSource = "ico_menu_calendar",
            //    TargetType = typeof(PageCalendar),
            //    Enable = true
            //});


            this.Add(new MenuItem()
            {
                Title = "Soporte",
                IconSource = "ico_menu_soporte",
                TargetType = typeof(PageSupport),
                Enable = true
            });

            //this.Add(new MenuItem()
            //{
            //    Title = "Cerrar Sesión",
            //    IconSource = "ico_nmenu_activated",
            //    //TargetType = typeof(Page_Account),
            //    //TargetType = typeof(Page_Tabbed),
            //    //TargetType = typeof(Page_Activated),
            //    Enable = true
            //});

        }
    }
}
