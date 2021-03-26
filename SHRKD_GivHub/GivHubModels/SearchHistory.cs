using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubModels
{
    public class SearchHistory
    {
        private string phrase;
        private string email;
        public int Id { get; set; }

        public string Email
        {
            get { return email; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Email must not be null.");
                }
                email = value;
            }
        }

        public string Phrase
        {
            get { return phrase; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Phrase must not be null.");
                }
                phrase = value;
            }
        }
    }
}
