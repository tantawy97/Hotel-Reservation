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
    public class RoomsController : Controller
    {
        
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IRoomRepository roomRepository;

        private readonly HotelBookingContext _context;

        public RoomsController(HotelBookingContext context,IRoomTypeRepository roomTypeRepository,IRoomRepository roomRepository)
        {
            _context = context;
            this.roomTypeRepository = roomTypeRepository;
            this.roomRepository = roomRepository;
        }

        // GET: Rooms
        public  IActionResult Index()
        {
            return View( roomRepository.GetAll());
        }

        // GET: Rooms/Details/5
        public IActionResult Details(int id)
        {

            var room = roomRepository.GetByID(id);
               
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,RoomId,price,IsReserved")] Room room)
        {
            if (ModelState.IsValid)
            {
                List<RoomType> roomTypes = roomTypeRepository.GetAll();
                ViewBag["RoomTypes"] = roomTypes;
                roomRepository.Insert(room);
             
               
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public IActionResult Edit(int id)
        {


            var room = roomRepository.GetByID(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,RoomId,price,IsReserved")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //roomRepository.Update(room, id);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public IActionResult Delete(int id)
        {


            var room = roomRepository.GetByID(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var room = roomRepository.Delete(id);
            return RedirectToAction("Index");
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
