using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel_Reservation.Models;
using Hotel_Reservation.Services;

namespace Hotel_Reservation.Controllers
{
    public class RoomTypesController : Controller
    {
        private readonly HotelBookingContext _context;
        private readonly IRoomTypeRepository roomTypeRepository;

        public RoomTypesController(HotelBookingContext context,IRoomTypeRepository roomTypeRepository)
        {
            _context = context;



            this.roomTypeRepository = roomTypeRepository;
        }

        // GET: RoomTypes
        public IActionResult Index()
        {
            return View(roomTypeRepository.GetAll());
        }

        // GET: RoomTypes/Details/5
        public IActionResult Details(int id)
        {
            var roomType = roomTypeRepository.GetByID(id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // GET: RoomTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( RoomType roomType)
        {
            if (ModelState.IsValid)
            {
               roomTypeRepository.Insert(roomType);
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }

        // GET: RoomTypes/Edit/5
        public IActionResult Edit(int id)
        {

            var roomType =roomTypeRepository.GetByID(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  RoomType roomType)
        {
            if (id != roomType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    roomTypeRepository.Update(roomType,id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomTypeExists(roomType.Id))
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
            return View(roomType);
        }

        // GET: RoomTypes/Delete/5
        public IActionResult Delete(int id)
        {
          

            var roomType = roomTypeRepository.GetByID(id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // POST: RoomTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var roomType = roomTypeRepository.Delete(id);
          
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTypeExists(int id)
        {
            return _context.RoomTypes.Any(e => e.Id == id);
        }
    }
}
