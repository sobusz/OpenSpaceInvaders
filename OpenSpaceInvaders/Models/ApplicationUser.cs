using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace OpenSpaceInvaders.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UserMedia = new HashSet<BlobModel>();
        }

        public virtual ICollection<BlobModel> UserMedia { get; set; }
    }
}
