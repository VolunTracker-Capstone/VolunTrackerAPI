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
    }
}