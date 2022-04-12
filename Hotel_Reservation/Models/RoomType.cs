using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Reservation.Models
{   [Index(nameof(Type), IsUnique = true, Name = "Unique Type")]

    public class RoomType
    {
        
        public int Id { get; set; }


        [Required(ErrorMessage = "Room Type is rquired")]
        [Display(Name = "Room Type")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Price is rquired")]

        public int price { get; set; }

        public virtual List<Room>Rooms { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
