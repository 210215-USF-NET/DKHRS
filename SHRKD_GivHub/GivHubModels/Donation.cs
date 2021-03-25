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
        private decimal amount;
        public int Id { get; set; }

        public string Email
        {
            get { return email; }
            set
            {
                if (value == null || value.Equals(""))
                {
                    throw new Exception("Email must not be null.");
                }
                email = value;
            }
        }

        public decimal Amount
        {
            get { return amount; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("amount must not be null.");
                }
                amount = value;
            }
        }

        public int CharityId
        { get; set; }
    }
}
