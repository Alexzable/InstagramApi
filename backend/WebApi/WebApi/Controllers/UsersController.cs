using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserContext _context;
    
        public UsersController(UserContext context)
        {
            _context = context;
        }
     
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUserContexts()
        {
            return await _context.UserContexts.ToListAsync();
        }
        

        // GET: api/username/password
        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<User>> GetUser(string username, string password)
        {
            var user = await _context.UserContexts.FindAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            else if (user.Password != password)
            {
                return NotFound();
            }



            return user;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Username)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.UserContexts.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Username }, user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            var user = await _context.UserContexts.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.UserContexts.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(string id)
        {
            return _context.UserContexts.Any(e => e.Username == id);
        }


    }
}
