using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel_Reservation.Models;
using Microsoft.AspNetCore.Http;
using Hotel_Reservation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Hotel_Reservation.Controllers
{
    [Authorize] 
    public class BookingsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly HotelBookingContext _context;
        IBookingRepository bookingRepository;
        public BookingsController(UserManager<User> userManager,HotelBookingContext context,
            IBookingRepository bookingRepository)
        {
            this.userManager = userManager;
            _context = context;
            this.bookingRepository = bookingRepository;
        }

        // GET: Bookings
        public  IActionResult Index()
        {
            return View("Reservation", bookingRepository.GetStatus());
        }

        // GET: Bookings/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var booking = await _context.Bookings
        //        .Include(b => b.Room)
        //        .Include(b => b.customer)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(booking);
        //}

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["RoomType"] = _context.RoomTypes.ToList();
            
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Booking booking)
        {
            booking.customerId = userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                bookingRepository.Insert(booking);
                    
                return RedirectToAction("index","home");
            }
           
            ViewData["RoomType"] = _context.RoomTypes.ToList();
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public IActionResult Edit(int id)
        {

            var booking = bookingRepository.GetByID(id);
            if (booking == null)
            {
                return NotFound();
            }
            //ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomId", booking.RoomTypeId);
            //ViewData["customerId"] = new SelectList(_context.Set<User>(), "Id", "Id", booking.customerId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bookingRepository.Update(booking, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomId", booking.RoomTypeId);
            //ViewData["customerId"] = new SelectList(_context.Set<User>(), "Id", "Id", booking.customerId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var booking = await _context.Bookings
        //        .Include(b => b.Room)
        //        .Include(b => b.customer)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(booking);
        //}

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = bookingRepository.Delete(id);
          
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
        public IActionResult accept(Booking booking)
        {
            booking.Status = 1;
            bookingRepository.Update(booking,booking.Id);
            return View("Reservation", bookingRepository.GetStatus());


        } public IActionResult Refuse(Booking booking)
        {
            booking.Status = 0;
            bookingRepository.Update(booking,booking.Id);
            return View("Reservation", bookingRepository.GetStatus());


        }

       public IActionResult GetMyActivity()
        {
            string id = userManager.GetUserId(User);
            return View(bookingRepository.GetAll().Where(w => w.customerId == id).ToList());
        }
    }
}
