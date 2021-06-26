using ChatUI.DB;
using ChatUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatUI.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        AppDbContext _context;

        public RoomViewComponent(AppDbContext context) => _context = context;

        public IViewComponentResult Invoke()
        { 
            var userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var chats = _context.ChatUsers
                .Include(x => x.Chat)
                .Where(x => x.UserId == userid && x.Chat.Type == ChatType.ROOM)
                .Select(x => x.Chat)
                .ToList();

            return View(chats);
        }
    }
}
