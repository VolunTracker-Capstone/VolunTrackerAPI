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

        [HttpPost]
        public async Task<ActionResult<UserAttendsEvent>> MemberAttendsEvent(UserAttendsEvent userAttendsEvent)
        {
            _context.UserAttendsEvent.Add(userAttendsEvent);
            await _context.SaveChangesAsync();

            return Ok(userAttendsEvent);
        }
    }
}