using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubModels
{
    public class Subscription
    {

        public int Id { get; set; }

        public User User_id { get; set; }

        public Charity Charity_id { get; set; }
    }
}
