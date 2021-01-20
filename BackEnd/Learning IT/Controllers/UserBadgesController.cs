﻿using Learning_IT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Learning_IT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBadgesController : ControllerBase
    {
        private readonly MyContext _context;

        public UserBadgesController(MyContext context)
        {
            _context = context;
        }
        // GET: api/<UserBadgesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBadge>>> Get()
        {
            return await _context.UserBadges.ToListAsync();
        }

        // GET api/<UserBadgesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserBadge>> Get(int id)
        {
            var userBadge = await _context.UserBadges.FindAsync(id);

            if (userBadge == null)
            {
                return NotFound();
            }

            return userBadge;
        }

        // POST api/<UserBadgesController>
        [HttpPost]
        public async Task<ActionResult<UserBadge>> Post([FromBody] UserBadge userBadge)
        {
            _context.UserBadges.Add(userBadge);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserBadgeExists(userBadge.UserID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserCourse", new { id = userBadge.UserID }, userBadge);
        }

        // PUT api/<UserBadgesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserBadge userBadge)
        {
            if (id != userBadge.UserID)
            {
                return BadRequest();
            }

            _context.Entry(userBadge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserBadgeExists(id))
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

        // DELETE api/<UserBadgesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userBadges = await _context.UserBadges.FindAsync(id);
            if (userBadges == null)
            {
                return NotFound();
            }

            _context.UserBadges.Remove(userBadges);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserBadgeExists(int id)
        {
            return _context.UserBadges.Any(e => e.UserID == id);
        }
    }
}
