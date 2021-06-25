using ChatUI.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatUI.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        AppDbContext _context;

        public RoomViewComponent(AppDbContext context) => _context = context;

        public IViewComponentResult Invoke()
        {
            var chats = _context.Chats.ToList();

            return View(chats);
        }
    }
}
