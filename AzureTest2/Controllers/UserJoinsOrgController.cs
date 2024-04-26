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
        [HttpGet("/organizations/{id}/members")]
        public async Task<ActionResult<IEnumerable<UserJoinsOrg>>> GetOrganizationMembers(int id)
        {
            var members = await _context.UserJoinsOrg
                .Where(ujo => ujo.OrgID == id)
                .Select(ujo => ujo)
                .Distinct()
                .ToListAsync();

            if (members == null)
            {
                return NotFound();
            }

            return Ok(members);
        }
        
        [HttpDelete("/UserJoinsOrg/{orgID}/{memberID}")]
        public async Task<ActionResult<UserJoinsOrg>> UserLeavesOrganization(int orgID, int memberID)
        {
            var ujo = await _context.UserJoinsOrg.FindAsync(memberID, orgID);

            if (ujo == null)
                return NotFound($"Ujo with organizationID = {orgID} and memberID = {memberID} not found"); 
            _context.Remove(ujo);
            await _context.SaveChangesAsync();

            return Ok(ujo);
        }

        [HttpPost]
        public async Task<ActionResult<UserJoinsOrg>> UserJoinsOrganization(UserJoinsOrg userJoinsOrg)
        {
            _context.UserJoinsOrg.Add(userJoinsOrg);
            await _context.SaveChangesAsync();

            return Ok(userJoinsOrg);
        }
        
        [HttpPut("/UserJoinsOrg/{orgID}/{memberID}")]
        public async Task<ActionResult<UserJoinsOrg>> UpdateUserHours(int orgID, int memberID, UserJoinsOrgHoursUpdate ujohu)
        {
            var ujo = await _context.UserJoinsOrg.FindAsync(memberID, orgID);

            if (ujo == null)
                return NotFound($"Ujo with organizationID = {orgID} and memberID = {memberID} not found");

            ujo.HoursWorked = Decimal.Add(ujo.HoursWorked, ujohu.HoursWorked);
            
            _context.UserJoinsOrg.Update(ujo);
            await _context.SaveChangesAsync();

            return Ok(ujo);
        }
    }
}