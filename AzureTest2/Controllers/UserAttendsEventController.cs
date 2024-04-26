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

        [HttpGet("/UserAttendsEvents")]
        public async Task<ActionResult<IEnumerable<UserAttendsEvent>>> GetUserAttendsEvents()
        {
            var uaeList = await _context.UserAttendsEvent.ToListAsync();
            return uaeList;
        }
        
        [HttpGet("/UserAttendsEvent/{eventID}/{memberID}")]
        public async Task<ActionResult<IEnumerable<UserAttendsEvent>>> GetSingleUser(int eventID, int memberID)
        {
            var uae = await _context.UserAttendsEvent.FindAsync(memberID, eventID);
            if (uae == null)
            {
                return NotFound();
            }

            return Ok(uae);
        }
        
        [HttpGet("/events/{id}/members")]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetEventMembers(int id)
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
                    MemberID = m.MemberID,
                    LastName = m.LastName,
                    Email = m.Email,
                    Role = m.Role,
                    TotalHours = m.TotalHours,
                    Phone = m.Phone,
                    Username = m.Username
                })
                .ToListAsync();
            List<MemberDTO> membersDTO = new List<MemberDTO>();
            foreach (var member in members)
            {
                var memberDTO = new MemberDTO
                {
                    Email = member.Email,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    MemberID = member.MemberID,
                    Phone = member.Phone,
                    Role = member.Role,
                    TotalHours = member.TotalHours,
                    Username = member.Username
                };
                membersDTO.Add(memberDTO);
            }
            return Ok(membersDTO);
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