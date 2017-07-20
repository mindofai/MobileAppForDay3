using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppForDay3.Models
{
    public class Debt
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public bool IsPaid { get; set; }
    }
}
