using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GivHubModels
{
    // Add profile data for application users by adding properties to the StoreMVCUser class
    public class User : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "character varying(256)")]
        public string FirstName { get; set; }
        [PersonalData]
        [Column(TypeName = "character varying(256)")]
        public string LastName { get; set; }

        public bool IsManager { get; set; }
    }
}
