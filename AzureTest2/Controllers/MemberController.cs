using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzureTest2.Data;
using AzureTest2.Models;
using System.Threading.Tasks;

namespace AzureTest2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly MyAzureContext _context;

        public MembersController(MyAzureContext context)
        {
            _context = context;
        }

        [HttpGet("/members/{id}")]
        public async Task<ActionResult<Member>> GetMemberById(int id)
        {
            var member = await _context.Member.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        [HttpGet("/members")]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllMembers()
        {
            return await _context.Member.ToListAsync();
        }
        
        [HttpPost("/members")]
        public async Task<IActionResult> AddMember(Member member)
        {
            _context.Member.Add(member);
            await _context.SaveChangesAsync();

            return Ok(member);
        }

        [HttpPut("/members/{id}")]
        public async Task<ActionResult<Member>> UpdateMember(int id, Member member)
        {
            try
            {
                if (id != member.MemberID)
                    return BadRequest("Member ID mismatch");

                var memberToUpdate = await _context.Member.FindAsync(id);

                if (memberToUpdate == null)
                    return NotFound($"Member with Id = {id} not found");
                memberToUpdate.ProfilePicture = member.ProfilePicture;
                memberToUpdate.LastName = member.LastName;
                memberToUpdate.FirstName = member.FirstName;
                memberToUpdate.Password = member.Password;
                memberToUpdate.TotalHours = member.TotalHours;
                memberToUpdate.Username = member.Username;
                memberToUpdate.Phone = member.Phone;
                memberToUpdate.Email = member.Email;
                memberToUpdate.MemberID = member.MemberID;
                _context.Member.Update(memberToUpdate);
                await _context.SaveChangesAsync();

                return Ok(memberToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        [HttpDelete("/members/{id}")]
        public async Task<ActionResult<Member>> DeleteMember(int id)
        {
            var memberToDelete = await _context.Member.FindAsync(id);

            if (memberToDelete == null)
                return NotFound($"Member with Id = {id} not found"); 
            _context.Remove(memberToDelete);
            await _context.SaveChangesAsync();
            return Ok(memberToDelete);
        }
    }
}