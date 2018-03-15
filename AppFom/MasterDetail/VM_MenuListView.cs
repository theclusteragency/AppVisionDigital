using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AppFom.MasterDetail
{
    public class VM_MenuListView : INotifyPropertyChanged
    {


        #region Implementa NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
            {

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Contructors

        public VM_MenuListView()
        {

            Start_LoadDefault();
        }

        #endregion

        #region Bindeos                 

        private List<MenuItem> menuListData;
        public const string MenuListDataPropertyName = "MenuListData";
        public List<MenuItem> MenuListData
        {
            get
            {
                return menuListData;
            }
            set
            {
                menuListData = value;
            }
        }


        #endregion


        private bool changeMenu;

        public bool ChangeMenu
        {
            get { return changeMenu; }
            set
            {
                changeMenu = value;
                if (changeMenu)
                {
                    ////var nmenu = new EnableMenuListData();
                    //menuListData = nmenu;
                    //OnPropertyChanged(MenuListDataPropertyName);
                }
            }
        }


        private void Start_LoadDefault()
        {
            List<MenuItem> data = new MenuListData();
            menuListData = data;
            OnPropertyChanged(MenuListDataPropertyName);
        }
    }
}