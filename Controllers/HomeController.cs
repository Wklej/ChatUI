using ChatUI.DB;
using ChatUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatUI.Controllers
{
    [Authorize]
    public class HomeController: Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context) => _context = context;

        public IActionResult index() => View();

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat = _context.Chats
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.Id == id);

            return View(chat);
        }        

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var msg = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(msg);

            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = chatId });
        }


        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.ROOM
            };

            chat.Users.Add(new ChatUser
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Admin
            });

            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> JoinRoom(int id)
        {
            var chatUser = new ChatUser
            {
                ChatId = id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Member
            };

            _context.ChatUsers.Add(chatUser);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
