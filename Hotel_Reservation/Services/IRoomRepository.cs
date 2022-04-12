using Hotel_Reservation.Models;
using System.Collections.Generic;

namespace Hotel_Reservation.Services
{
    public interface IRoomRepository
    {
        int Delete(int id);
        List<Room> GetAll();
        Room GetByID(int id);
        void Insert(Room room);
        //int Update(Room newroom, int id);
    }
}