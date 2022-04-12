using Hotel_Reservation.Models;
using System.Collections.Generic;

namespace Hotel_Reservation.Services
{
    public interface IRoomTypeRepository
    {
        int Delete(int id);
        List<RoomType> GetAll();
        RoomType GetByID(int id);
        RoomType GetByType(string roomtype);

        void Insert(RoomType roomType);
        int Update(RoomType newroomtype, int id);
    }
}