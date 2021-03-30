using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GivHubModels
{
    public class Follow
    {
        private string useremail;
        private string followingemail;
        public int Id { get; set; }
        public string UserEmail
        {
            get { return useremail; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }
                useremail = value;
            }
        }
        public string FollowingEmail
        {
            get { return followingemail; }
            set
            {
                if (value == null || String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }
                followingemail = value;
            }
        }
    }
}
