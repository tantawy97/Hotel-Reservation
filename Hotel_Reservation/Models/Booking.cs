using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Reservation.Models
{
    public class Booking
    {  

        public int Id { get; set; }

        [ForeignKey("customer")]
        
        public string customerId { get; set; }

        [Required(ErrorMessage = "Booking From Date IS Required")]

        public DateTime From { get; set; }
        [Required(ErrorMessage = "Booking To Date IS Required")]

        public DateTime To { get; set; }


        [ForeignKey("RoomType")]
        
        public int RoomTypeId { get; set; }

        public int? Status { get; set; }
        public virtual RoomType RoomType { get; set; }

        public virtual User customer { get; set; }

        

    }
}
