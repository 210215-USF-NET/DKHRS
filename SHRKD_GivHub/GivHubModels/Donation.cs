using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubModels
{
    public class Donation
    {
        private string email;

        public int Id { get; set; }

        public string Email
        {
            get { return email; }
            set
            {
                if (value == null || value.Equals(""))
                {
                    throw new Exception("Phrase must not be null.");
                }
                email = value;
            }
        }

        public decimal Amount { get; set; }

        public Charity Charity { get; set; }
    }
}
