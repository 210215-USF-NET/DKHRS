using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubModels
{
    public class Location
    {
        private string state;
        private string city;
        private string zipcode;

        public int Id { get; set; }
        public string State
        {
            get { return state; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("State cannot be null.");
                }
                state = value;
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("City cannot be null.");
                }
                city = value;
            }
        }

        public string Zipcode
        {
            get { return zipcode; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Zipcode cannot be null.");
                }
                zipcode = value;
            }
        }

        public int CharityId
        { get; set; }

    }
}
