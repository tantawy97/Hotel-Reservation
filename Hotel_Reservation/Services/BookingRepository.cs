using Hotel_Reservation.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hotel_Reservation.Services
{
    public class BookingRepository : IBookingRepository
    {

        HotelBookingContext context;
        public BookingRepository(HotelBookingContext _context)
        {
            context = _context;
        }
        public List<Booking> GetAll()
        {

            List<Booking> bookings = context.Bookings.ToList();
            return bookings;

        }





        public Booking GetByID(int id)
        {
            Booking booking = context.Bookings.FirstOrDefault(c => c.Id == id);
            return booking;
        }
        public void Insert(Booking booking)
        {
            context.Bookings.Add(booking);
            context.SaveChanges();

        }

        public int Update(Booking newBooking, int id)
        {
            var oldbooking = GetByID(id);
            if (oldbooking != null)
            {
                oldbooking.From = newBooking.From;
                oldbooking.To = newBooking.To;
                oldbooking.Status = newBooking.Status;


            }
            return context.SaveChanges();
        }

        public int Delete(int id)
        {
            Booking booking = context.Bookings.FirstOrDefault(c => c.Id == id);
            context.Bookings.Remove(booking);
            int rows = context.SaveChanges();
            return rows;

        }

        public List<Booking> GetStatus()
        {
          return  context.Bookings.Where(w => w.Status == null).ToList();
        }
    }
}
