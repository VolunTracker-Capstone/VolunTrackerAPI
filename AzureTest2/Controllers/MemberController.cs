using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzureTest2.Data;
using AzureTest2.Models;
using System.Threading.Tasks;

namespace AzureTest2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly MyAzureContext _context;

        public MemberController(MyAzureContext context)
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
            return Ok(memberDTO);
        }

        [HttpGet("/members")]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetAllMembers()
        {
            var members = await _context.Member.ToListAsync();
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

            return membersDTO;
        }
        
        [HttpPost("/members")]
        public async Task<IActionResult> AddMember(MemberCreate memberCreateDTO)
        {
            var member = new Member
            {
                Email = memberCreateDTO.Email,
                FirstName = memberCreateDTO.FirstName,
                LastName = memberCreateDTO.LastName,
                Phone = memberCreateDTO.Phone,
                Username = memberCreateDTO.Username,
                Password = memberCreateDTO.Password
            };
            _context.Member.Add(member);
            await _context.SaveChangesAsync();
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
            return Ok(memberDTO);
        }

        [HttpPut("/members/{id}")]
        public async Task<ActionResult<MemberDTO>> UpdateMember(int id, MemberUpdate memberUpdate)
        {
            try
            {
                if (id != memberUpdate.MemberID)
                    return BadRequest("Member ID mismatch");

                var memberToUpdate = await _context.Member.FindAsync(id);

                if (memberToUpdate == null)
                    return NotFound($"Member with Id = {id} not found");
                memberToUpdate.LastName = memberUpdate.LastName;
                memberToUpdate.FirstName = memberUpdate.FirstName;
                memberToUpdate.Username = memberUpdate.Username;
                memberToUpdate.Phone = memberUpdate.Phone;
                memberToUpdate.Email = memberUpdate.Email;
                memberToUpdate.MemberID = memberUpdate.MemberID;
                _context.Member.Update(memberToUpdate);
                await _context.SaveChangesAsync();
                
                var memberDTO = new MemberDTO
                {
                    Email = memberToUpdate.Email,
                    FirstName = memberToUpdate.FirstName,
                    LastName = memberToUpdate.LastName,
                    MemberID = memberToUpdate.MemberID,
                    Phone = memberToUpdate.Phone,
                    Role = memberToUpdate.Role,
                    TotalHours = memberToUpdate.TotalHours,
                    Username = memberToUpdate.Username
                };
                return Ok(memberDTO);
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