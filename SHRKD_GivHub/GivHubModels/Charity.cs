using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubModels
{
    public class Charity
    {
        private string name;
        private string missionstatement;
        private string website;
        private string category;
        private string logourl;
        private string eid;

        public int Id { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Name must contain a value.");
                }
                name = value;
            }
        }

        public Location Location { get; set; }
        public string Missionstatement
        {
            get { return missionstatement; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Mission statement must not be null.");
                }
                missionstatement = value;
            }
        }

        public string Website
        {
            get { return website; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Website must not be null.");
                }
                website = value;
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Category must not be null.");
                }
                category = value;
            }
        }

        public string Logourl
        {
            get { return logourl; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("URL must not be null.");
                }
                logourl = value;
            }
        }

        public string EID
        {
            get { return eid; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("eid must not be null.");
                }
                eid = value;
            }
        }


    }
}
