using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubModels
{
    public class Donation
    {
        private decimal amount;
        public int Id { get; set; }

        public User User_id { get; set; }

        public decimal Amount { get; set; }

        public Charity Charity_id { get; set; }
    }
}
