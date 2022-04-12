using Hotel_Reservation.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hotel_Reservation.Services
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelBookingContext context;

        public RoomRepository(HotelBookingContext _context)
        {
            context = _context;
        }
        public List<Room> GetAll()
        {

            List<Room> rooms = context.Rooms.ToList();
            return rooms;

        }





        public Room GetByID(int id)
        {
            Room room = context.Rooms.FirstOrDefault(c => c.Id == id);
            return room;
        }
        public void Insert(Room room)
        {
            context.Rooms.Add(room);
            context.SaveChanges();

        }

        //public int Update(Room newroom, int id)
        //{
        //    var oldroom = GetByID(id);
        //    if (oldroom == null)
        //    {
        //        oldroom.price = newroom.price;
        //    }
        //    return context.SaveChanges();
        //}

        public int Delete(int id)
        {
            Room room = context.Rooms.FirstOrDefault(c => c.Id == id);
            context.Rooms.Remove(room);
            int rows = context.SaveChanges();
            return rows;

        }
    }
}
