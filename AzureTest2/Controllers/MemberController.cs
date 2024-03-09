using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzureTest2.Models; 
using AzureTest2.Data;   

namespace AzureTest2.Controllers;

[ApiController]
[Route("[controller]")]
public class MembersController : ControllerBase
{
    private readonly MyAzureContext _context;

    public MembersController(MyAzureContext context)
    {
        _context = context;
    }

    // This is to GET: /Members
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
    {
        return await _context.Member.ToListAsync();
    }
}