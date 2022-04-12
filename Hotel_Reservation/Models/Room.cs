using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Reservation.Models
{
    public class Room
    {
      
        public int Id { get; set; }


        [Required(ErrorMessage = "Room Type is rquired")]
        [Display(Name = "Room Type")]
        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; }
       
        public bool IsReserved { get; set; }
        public virtual RoomType RoomType { get; set; }
        

    }
}
