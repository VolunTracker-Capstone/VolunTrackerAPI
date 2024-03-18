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

        [HttpPost]
        public async Task<ActionResult<TagsList>> AddTag(TagsList tagsList)
        {
            _context.TagsList.Add(tagsList);
            await _context.SaveChangesAsync();

            return Ok(tagsList);
        }
    }
}