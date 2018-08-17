using System;
using System.Collections.Generic;
using AppFom.Models;

namespace AppFom.Helpers
{
    public class GlobalsManager
    {
        //public string APIKEY { get { return "TKfZC1SXnKauJiYxkJMB98wyPONtFoA22IAc2qyq"; } }

		public string APIKEY { get { return "V6FjW2trvR07jgRab7St1S2IjY8g9Zg94d3aRll6"; } }

        public User USERFOM { get; set; }

        public string TOKENAPNS { get; set; }

        public List<Event> MISEVENTOS { get; set; }
    }
}
