using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSpaceInvaders.Models
{
    public class DesksModel
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual List<BookingModel> Bookings { get; set; }

    }
}
