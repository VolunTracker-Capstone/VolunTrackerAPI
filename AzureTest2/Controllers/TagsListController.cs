using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzureTest2.Data;
using AzureTest2.Models;
using System.Threading.Tasks;

namespace AzureTest2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsListController : ControllerBase
    {
        private readonly MyAzureContext _context;

        public TagsListController(MyAzureContext context)
        {
            _context = context;
        }
        
        [HttpGet("/organizations/{id}/tags")]
        public async Task<ActionResult<IEnumerable<string>>> GetOrganizationTags(int id)
        {
            var tagIDs = await _context.TagsList
                .Where(tl => tl.OrgID == id)
                .Select(tl => tl.TagID)
                .Distinct()
                .ToListAsync();

            if (tagIDs == null)
            {
                return NotFound();
            }

            var tags = await _context.TagsList
                .Where(t => tagIDs.Contains(t.TagID))
                .Select(t => t.TagTitle)
                .ToListAsync();
            return Ok(tags);
        }
        
        [HttpPost]
        public async Task<ActionResult<TagsList>> AddTag(TagsList tagsList)
        {
            _context.TagsList.Add(tagsList);
            await _context.SaveChangesAsync();

            return Ok(tagsList);
        }
    }
}