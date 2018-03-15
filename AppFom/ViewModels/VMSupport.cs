using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Interfaces;
using Xamarin.Forms;

namespace AppFom.ViewModels
{
    public class VMSupport : ViewModelMaster
    {
        #region Vars & Properties

        private readonly IOperationServices Services;

        #endregion


        public VMSupport(INavigation navigation)
        {
            this.Navigation = navigation;
            Services = new OperationServices();
        }

        Command commandCallNumber;
        public const string CommandCallNumberPropertyName = "CommandCallNumber";
        public Command CommandCallNumber
        {
            get
            {
                return commandCallNumber ??
                    (commandCallNumber = new Command(async () => await ExecuteCommandCallNumber()));
            }
        }

        Command commandSendMail;
        public const string CommandSendMailPropertyName = "CommandSendMail";
        public Command CommandSendMail
        {
            get
            {
                return commandSendMail ??
                    (commandSendMail = new Command(async () => await ExecuteCommandSendMail()));
            }
        }

        async Task ExecuteCommandCallNumber()
        {
            Debug.WriteLine("Llamando");

            Debug.WriteLine("Llamando....");
            await DependencyService.Get<ICallingServices>().CallingNumber("5212467570002");
        }


        async Task ExecuteCommandSendMail()
        {
            Debug.WriteLine("Send Email");
            Device.OpenUri(new Uri("mailto:ayuda@rafagana2018.com"));

        }
    }
}


