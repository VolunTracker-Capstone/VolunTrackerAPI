using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzureTest2.Data;
using AzureTest2.Models;
using System.Threading.Tasks;

namespace AzureTest2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserJoinsOrgController : ControllerBase
    {
        private readonly MyAzureContext _context;

        public UserJoinsOrgController(MyAzureContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<UserJoinsOrg>> UserJoinsOrganization(UserJoinsOrg userJoinsOrg)
        {
            _context.UserJoinsOrg.Add(userJoinsOrg);
            await _context.SaveChangesAsync();

            return Ok(userJoinsOrg);
        }
    }
}