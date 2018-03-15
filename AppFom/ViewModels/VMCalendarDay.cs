using System;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Interfaces;
using AppFom.Models;
using AppFom.Pages;
using Xamarin.Forms;

namespace AppFom.ViewModels
{
    public class VMCalendarDay : ViewModelMaster
    {
        #region Vars & Properties

        private readonly IOperationServices Services;
        private Amazon.S3.Transfer.TransferUtility transferUtility;

        #endregion


        public VMCalendarDay(INavigation navigation)
        {
            this.Navigation = navigation;
            Services = new OperationServices();

        }

        Event selectedEvents;
        public const string SelectedEventsPropertyName = "SelectedEvents";
        public Event SelectedEvents
        {
            get { return selectedEvents; }
            set
            {
                selectedEvents = value;
                if (selectedEvents != null)
                {
                    this.Navigation.PushAsync(new PageEventDetail(selectedEvents));
                }
                selectedEvents = null;
            }
        }

    }
}
