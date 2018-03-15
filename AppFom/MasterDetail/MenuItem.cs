using System;
namespace AppFom.MasterDetail
{
    public class MenuItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }

        public bool Enable { get; set; }
    }

}