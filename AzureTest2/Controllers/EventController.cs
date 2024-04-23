using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzureTest2.Data;
using AzureTest2.Models;

namespace AzureTest2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly MyAzureContext _context;

        public EventController(MyAzureContext context)
        {
            _context = context;
        }

        [HttpGet("/events/{id}")]
        public async Task<ActionResult<Event>> GetEventById(int id)
        {
            var eventToGet = await _context.Event.FindAsync(id);

            if (eventToGet == null)
            {
                return NotFound();
            }

            return Ok(eventToGet);
        }

        [HttpGet("/events")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            return await _context.Event.ToListAsync();
        }
        
        [HttpPost("/events")]
        public async Task<IActionResult> AddEvent(EventCreate eventToAdd)
        {
            Event newEvent = new Event
            {
                EventOwnerID = eventToAdd.EventOwnerID,
                Name = eventToAdd.Name,
                Date = eventToAdd.Date,
                Street = eventToAdd.Street,
                City = eventToAdd.City,
                State = eventToAdd.State,
                Zip = eventToAdd.Zip,
                VolunteersNeeded = eventToAdd.VolunteersNeeded
            };
            _context.Event.Add(newEvent);
            await _context.SaveChangesAsync();

            return Ok(eventToAdd);
        }

        [HttpPut("/events/{id}")]
        public async Task<ActionResult<Event>> UpdateMember(int id, Event updatedEvent)
        {
            try
            {
                if (id != updatedEvent.EventID)
                    return BadRequest("Event ID mismatch");

                var eventToUpdate = await _context.Event.FindAsync(id);

                if (eventToUpdate == null)
                    return NotFound($"Event with Id = {id} not found");
                //Look into mappers to be able to eventToUpdate = updatedEvent
                //dotnet map model to context model
                eventToUpdate.Name = updatedEvent.Name;
                eventToUpdate.Date = updatedEvent.Date;
                eventToUpdate.Street = updatedEvent.Street;
                eventToUpdate.City = updatedEvent.City;
                eventToUpdate.State = updatedEvent.State;
                eventToUpdate.Zip = updatedEvent.Zip;
                eventToUpdate.EventImage = updatedEvent.EventImage;
                eventToUpdate.VolunteersNeeded = updatedEvent.VolunteersNeeded;
                eventToUpdate.EventID = updatedEvent.EventID;
                _context.Event.Update(eventToUpdate);
                await _context.SaveChangesAsync();

                return Ok(eventToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        [HttpDelete("/events/{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var eventToDelete = await _context.Event.FindAsync(id);

            if (eventToDelete == null)
                return NotFound($"Event with Id = {id} not found"); 
            _context.Remove(eventToDelete);
            await _context.SaveChangesAsync();
            return Ok(eventToDelete);
        }
    }
}