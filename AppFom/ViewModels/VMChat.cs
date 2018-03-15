using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Interfaces;
using AppFom.Models;
using AppFom.Pages;
using Xamarin.Forms;

namespace AppFom.ViewModels
{
    public class VMChat : ViewModelMaster
    {
        #region Vars & Properties

        private readonly IOperationServices Services;
        private Amazon.S3.Transfer.TransferUtility transferUtility;

        #endregion


        public VMChat(INavigation navigation)
        {
            this.Navigation = navigation;
            Services = new OperationServices();

            LoadDefault();
        }

        private ObservableCollection<User> sourceUsers;
        public ObservableCollection<User> SourceUsers
        {
            get { return sourceUsers; }
            set { SetProperty(ref sourceUsers, value); }
        }

        User selectedUser;
        public const string SelectedUserPropertyName = "SelectedUser";
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                if (selectedUser != null)
                {
                    this.Navigation.PushAsync(new PageChatDetail(selectedUser));
                    Debug.WriteLine(selectedUser.nombre);
                }
                selectedUser = null;
            }
        }

        public async Task LoadDefault()
        {

            //var request = await Services.getAllUsers();

            //var users = new ObservableCollection<User>();
            //foreach (var item in request.data)
            //{

            //    users.Add(item);
            //}

            //sourceUsers = users;
            //SourceUsers = sourceUsers;
        }

    }
}
