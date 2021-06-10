using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSpaceInvaders.Models
{
    public class BookingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BookingDate { get; set; }

        public string CustomerId { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)]

        public string Name { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)]

        public string Surname { get; set; }

        [RegularExpression(@"^\d{9}$")]
        [Required(ErrorMessage = "Enter a valid phone number (9 digits).")]

        public string PhoneNumber { get; set; }

        public string Email { get; set; }


        public DesksModel Desk { get; set; }
        public int DeskId { get; set; }


    }
}
