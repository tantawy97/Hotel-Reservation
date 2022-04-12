using Hotel_Reservation.Models;
using System.Collections.Generic;

namespace Hotel_Reservation.Services
{
    public interface IBookingRepository
    {
        int Delete(int id);
        List<Booking> GetAll();
        Booking GetByID(int id);
        void Insert(Booking booking);
        int Update(Booking newBooking, int id);
        List<Booking> GetStatus();
    }
}