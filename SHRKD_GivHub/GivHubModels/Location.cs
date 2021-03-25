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
        private int charityid;

        public int Id { get; set; }
        public string State
        {
            get { return state; }
            set
            {
                if (value == null || value.Equals(""))
                {
                    throw new Exception("State cannot be null.");
                }
                state = value;
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                if (value == null || value.Equals(""))
                {
                    throw new Exception("City cannot be null.");
                }
                city = value;
            }
        }

        public string Zipcode
        {
            get { return zipcode; }
            set
            {
                if (value == null || value.Equals(""))
                {
                    throw new Exception("Zipcode cannot be null.");
                }
                zipcode = value;
            }
        }

        public int Charityid { get; set; }

    }
}
