using System;
using AppFom.MasterDetail;

namespace AppFom.Helpers
{
    public class Fom
    {
        #region Vars 

        private static ScreenManager _screenManager;
        private static ColorManager _colormanager;
        private static GlobalsManager _globalsManager;
        private static VMMenuPage _vmmenu;
        private static DeviceManager _deviceManager;
        static CacheManager cachemanager;

        //private static RegexManager _regexmanager;
        //private static ConfigManager _configmanager;
        //private static DialogsManager _dialogsManager;


        #endregion

        #region Constructor

        public Fom() { }

        #endregion

        #region Singletons

        /// <summary>
        /// Singleton Manager Device
        /// </summary>      
        public static DeviceManager Device
        {

            get
            {
                if (_deviceManager == null)
                    _deviceManager = new DeviceManager();

                return _deviceManager;
            }

        }

        /// <summary>
        /// Singleton Manager Screen
        /// </summary>      
        public static ScreenManager Screen
        {

            get
            {
                if (_screenManager == null)
                    _screenManager = new ScreenManager();

                return _screenManager;
            }

        }

        /// <summary>
        /// Singleton Manager Application Color
        /// </summary>
        public static ColorManager Colors
        {

            get
            {
                if (_colormanager == null)
                    _colormanager = new ColorManager();

                return _colormanager;
            }

        }

        /// <summary>
        /// Singleton Manager Application Globals
        /// </summary>
        public static GlobalsManager Globals
        {

            get
            {
                if (_globalsManager == null)
                    _globalsManager = new GlobalsManager();

                return _globalsManager;
            }

        }


        /// <summary>
        /// Singleton Manager Application Menu Master Detail
        /// </summary>
        /// <value>The VM odel.</value>
        public static VMMenuPage VMmenu
        {

            get
            {
                if (_vmmenu == null)
                    _vmmenu = new VMMenuPage(App.INavPage);

                return _vmmenu;
            }

        }

        public static CacheManager Cache
        {

            get
            {
                if (cachemanager == null)
                    cachemanager = new CacheManager();

                return cachemanager;
            }
        }


        /*
        /// <summary>
        /// Singleton Manager Application Dialogs
        /// </summary>
        public static DialogsManager Dialogs
        {

            get
            {
                if (_dialogsManager == null)
                    _dialogsManager = new DialogsManager();

                return _dialogsManager;
            }

        }

        /// <summary>
        /// Singleton Manager Application Configuration
        /// </summary>
        public static ConfigManager Config
        {

            get
            {
                if (_configmanager == null)
                    _configmanager = new ConfigManager();

                return _configmanager;
            }

        }
        

        /// <summary>
        /// Singleton Manager Application Regex
        /// </summary>
        public static RegexManager Regex
        {

            get
            {
                if (_regexmanager == null)
                    _regexmanager = new RegexManager();

                return _regexmanager;
            }
        }
        */
        #endregion
    }
}