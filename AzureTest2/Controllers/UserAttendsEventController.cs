using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzureTest2.Data;
using AzureTest2.Models;
using System.Threading.Tasks;

namespace AzureTest2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAttendsEventController : ControllerBase
    {
        private readonly MyAzureContext _context;

        public UserAttendsEventController(MyAzureContext context)
        {
            _context = context;
        }
        
        [HttpGet("/events/{id}/members")]
        public async Task<ActionResult<IEnumerable<Member>>> GetEventMembers(int id)
        {
            var memberIDs = await _context.UserAttendsEvent
                .Where(uae => uae.EventID == id)
                .Select(uae => uae.MemberID)
                .Distinct()
                .ToListAsync();

            if (memberIDs == null)
            {
                return NotFound();
            }

            var members = await _context.Member
                .Where(m => memberIDs.Contains(m.MemberID))
                .Select(m => new
                {
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Email = m.Email,
                    Phone = m.Phone,
                    Username = m.Username
                })
                .ToListAsync();
            return Ok(members);
        }
        
        [HttpDelete("/UserAttendsEvent/{eventID}/{memberID}")]
        public async Task<ActionResult<UserAttendsEvent>> UserLeavesEvent(int eventID, int memberID)
        {
            var uae = await _context.UserAttendsEvent.FindAsync(memberID, eventID);
         
            if (uae == null)
                return NotFound($"Uae with eventID = {eventID} and memberID = {memberID} not found"); 
            _context.Remove(uae);
            await _context.SaveChangesAsync();

            return Ok(uae);
        }
        
        [HttpPut("/UserAttendsEvent/{eventID}/{memberID}/checkIn")]
        public async Task<ActionResult<UserAttendsEvent>> CheckInUser(int eventID, int memberID, UserAttendsEventUpdate uae)
        {
            var uaeToUpdate = await _context.UserAttendsEvent.FindAsync(memberID, eventID);
         
            if (uaeToUpdate == null)
                return NotFound($"Uae with eventID = {eventID} and memberID = {memberID} not found");
            
            uaeToUpdate.CheckIn = uae.datetime;
            
            _context.UserAttendsEvent.Update(uaeToUpdate);
            await _context.SaveChangesAsync();

            return Ok(uae);
        }
        
        [HttpPut("/UserAttendsEvent/{eventID}/{memberID}/checkOut")]
        public async Task<ActionResult<UserAttendsEvent>> CheckOutUser(int eventID, int memberID, UserAttendsEventUpdate uae)
        {
            var uaeToUpdate = await _context.UserAttendsEvent.FindAsync(memberID, eventID);
         
            if (uaeToUpdate == null)
                return NotFound($"Uae with eventID = {eventID} and memberID = {memberID} not found");
            
            uaeToUpdate.CheckOut = uae.datetime;
            
            _context.UserAttendsEvent.Update(uaeToUpdate);
            await _context.SaveChangesAsync();

            return Ok(uae);
        }

        [HttpPost]
        public async Task<ActionResult<UserAttendsEvent>> MemberAttendsEvent(UserAttendsEvent userAttendsEvent)
        {
            _context.UserAttendsEvent.Add(userAttendsEvent);
            await _context.SaveChangesAsync();

            return Ok(userAttendsEvent);
        }
        
    }
}