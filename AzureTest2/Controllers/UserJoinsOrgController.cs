﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<Member>>> GetOrganizationMembers(int id)
        {
            var memberIDs = await _context.UserJoinsOrg
                .Where(ujo => ujo.OrgID == id)
                .Select(ujo => ujo.MemberID)
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

        [HttpPost]
        public async Task<ActionResult<UserJoinsOrg>> UserJoinsOrganization(UserJoinsOrg userJoinsOrg)
        {
            _context.UserJoinsOrg.Add(userJoinsOrg);
            await _context.SaveChangesAsync();

            return Ok(userJoinsOrg);
        }
    }
}