using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hotel_Reservation.Models
{   [Index(nameof(NationalId),IsUnique =true,Name ="Unique National Id")]
     
    public class User : IdentityUser
    {  

        public double NationalId { get; set; }
        public string Nationality { get; set; }

        public virtual List<Booking> Bookings { get; set; }
         
    } 
}
