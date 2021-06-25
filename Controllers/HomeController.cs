using ChatUI.DB;
using ChatUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatUI.Controllers
{
    public class HomeController: Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context) => _context = context;

        public IActionResult index() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            _context.Chats.Add(new Chat
            {
                Name = name,
                Type = ChatType.ROOM
            }); ;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
