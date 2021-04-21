using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kobus.Models
{
    public class Voyage
    {
        public string Route { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Capacity { get; set; }
        public string TicketPrice { get; set; }
        public string Plaqa { get; set; }
        public string Captain { get; set; }
        public string Bus { get; set; }
    }
}