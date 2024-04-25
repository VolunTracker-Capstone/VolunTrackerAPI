using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzureTest2.Data;
using AzureTest2.Models;

namespace AzureTest2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly MyAzureContext _context;

        public OrganizationController(MyAzureContext context)
        {
            _context = context;
        }

        [HttpGet("/organizations/{id}")]
        public async Task<ActionResult<Organization>> GetOrganizationById(int id)
        {
            var organization = await _context.Organization.FindAsync(id);

            if (organization == null)
            {
                return NotFound();
            }

            return Ok(organization);
        }

        [HttpGet("/organizations")]
        public async Task<ActionResult<IEnumerable<Organization>>> GetAllOrganizations()
        {
            var organizations = await _context.Organization.ToListAsync();
            return Ok(organizations);
        }
        
        [HttpPost("/organizations")]
        public async Task<ActionResult> AddOrganization(OrganizationCreate organizationCreateDTO)
        {
            Organization organization = new Organization
            {
                OrganizationOwnerID = organizationCreateDTO.OrganizationOwnerID,
                Name = organizationCreateDTO.Name,
                Street = organizationCreateDTO.Street,
                City = organizationCreateDTO.City,
                State = organizationCreateDTO.State,
                Zip = organizationCreateDTO.Zip,
                Website = organizationCreateDTO.Website,
                Description = organizationCreateDTO.Description
            };
            _context.Organization.Add(organization);
            await _context.SaveChangesAsync();

            return Ok(organization);
        }

        [HttpPut("/organizations/{id}")]
        public async Task<ActionResult<Organization>> UpdateOrganization(int id, Organization organization)
        {
            try
            {
                if (id != organization.organizationID)
                    return BadRequest("Organization ID mismatch");

                var organizationToUpdate = await _context.Organization.FindAsync(id);

                if (organizationToUpdate == null)
                    return NotFound($"Organization with Id = {id} not found");
                //Look into mappers to be able to organizationToUpdate = organization
                //dotnet map model to context model
                organizationToUpdate.organizationID = organization.organizationID;
                organizationToUpdate.OrganizationOwnerID = organization.OrganizationOwnerID;
                organizationToUpdate.Name = organization.Name;
                organizationToUpdate.Street = organization.Street;
                organizationToUpdate.City = organization.City;
                organizationToUpdate.State = organization.State;
                organizationToUpdate.Zip = organization.Zip;
                organizationToUpdate.Website = organization.Website;
                organizationToUpdate.Description = organization.Description;
                _context.Organization.Update(organizationToUpdate);
                await _context.SaveChangesAsync();

                return Ok(organizationToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        [HttpDelete("/organizations/{id}")]
        public async Task<ActionResult<Organization>> DeleteOrganization(int id)
        {
            var organization = await _context.Organization.FindAsync(id);

            if (organization == null)
                return NotFound($"Event with Id = {id} not found"); 
            _context.Remove(organization);
            await _context.SaveChangesAsync();
            return Ok(organization);
        }
    }
}