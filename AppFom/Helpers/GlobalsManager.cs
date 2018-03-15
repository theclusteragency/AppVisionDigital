using System;
using System.Collections.Generic;
using AppFom.Models;

namespace AppFom.Helpers
{
    public class GlobalsManager
    {
        public string APIKEY { get { return "TKfZC1SXnKauJiYxkJMB98wyPONtFoA22IAc2qyq"; } }

        public User USERFOM { get; set; }

        public string TOKENAPNS { get; set; }

        public List<Event> MISEVENTOS { get; set; }
    }
}
