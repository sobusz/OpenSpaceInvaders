using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSpaceInvaders.Models
{
    public class BookingModel
    {
        [Key]
        public int Id { get; set; }

        public List<BookingDate> Date { get; set; }

        public int CustomerId { get; set; }

        public int DeskId { get; set; }

        public virtual CustomerModel Customer { get; set; }

        public virtual DesksModel Desk { get; set; }
    }

    public class BookingDate
    {
        [Key]
        public int Id { get; set; }
        public DateTime date { get; set; }
    }
}
