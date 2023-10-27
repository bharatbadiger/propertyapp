namespace RealEstate.Controllers
{
    using global::RealEstate.Models;
    using global::RealEstate.Models.DTO;
    using global::RealEstate.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly IRepository<Lead> _leadRepository;

        public LeadsController(IRepository<Lead> leadRepository)
        {
            _leadRepository = leadRepository;
        }

        // GET: api/leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lead>>> GetLeads([FromServices] LeadRepository leadRepository)
        {
            var leads = await leadRepository.GetAllAsync();

            if (leads == null || !leads.Any())
            {
                var errorMessage = "No leads found.";
                var errorResponse = new ApiResponse<IEnumerable<Lead>>(false, errorMessage, null);
                return NotFound(errorResponse);
            }

            var successMessage = "Leads retrieved successfully.";
            var successResponse = new ApiResponse<IEnumerable<Lead>>(true, successMessage, leads);

            return Ok(successResponse);
        }

        // GET: api/leads/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Lead>>> GetLead([FromServices] LeadRepository leadRepository, int id)
        {
            var lead = await leadRepository.GetByIdAsync(id);

            if (lead == null)
            {
                var response = new ApiResponse<Lead>(false, "Lead not found", null);
                return NotFound(response);
            }

            return Ok(new ApiResponse<Lead>(true, "Lead retrieved successfully", lead));
        }

        // GET: api/leads/createdby/{createdById}
        [HttpGet("createdby/{createdById}")]
        public async Task<ActionResult<IEnumerable<Lead>>> GetLeadsByCreatedById([FromServices] LeadRepository leadRepository, int createdById)
        {
            var leads = await leadRepository.GetAllLeadsByCreatedById(createdById);

            if (leads == null || !leads.Any())
            {
                var errorMessage = "No leads found for the specified CreatedById.";
                var errorResponse = new ApiResponse<IEnumerable<Lead>>(false, errorMessage, null);
                return NotFound(errorResponse);
            }

            var successMessage = "Leads retrieved successfully for the specified CreatedById.";
            var successResponse = new ApiResponse<IEnumerable<Lead>>(true, successMessage, leads);

            return Ok(successResponse);
        }


        // POST: api/leads
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Lead>>> CreateLead([FromBody] Lead lead)
        {
            if (lead == null)
            {
                var errorMessage = "Invalid request data. Lead object is null.";
                var errorResponse = new ApiResponse<Lead>(false, errorMessage, null);
                return BadRequest(errorResponse);
            }

            var createdLead = await _leadRepository.CreateAsync(lead);

            if (createdLead == null)
            {
                var errorMessage = "Failed to create the lead.";
                var errorResponse = new ApiResponse<Lead>(false, errorMessage, null);
                return BadRequest(errorResponse);
            }

            var successMessage = "Lead created successfully";
            var successResponse = new ApiResponse<Lead>(true, successMessage, createdLead);

            return CreatedAtAction(nameof(GetLead), new { id = createdLead.Id }, successResponse);
        }

        // PUT: api/leads/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Lead>>> UpdateLead([FromServices] LeadRepository leadRepository, int id, [FromBody] Lead updatedLead)
        {
            var lead = await leadRepository.UpdateAsync(id, updatedLead);

            if (lead == null)
            {
                var errorMessage = "Lead not found or update failed.";
                var errorResponse = new ApiResponse<Lead>(false, errorMessage, null);
                return NotFound(errorResponse);
            }

            var successMessage = "Lead updated successfully.";
            var successResponse = new ApiResponse<Lead>(true, successMessage, lead);

            return Ok(successResponse);
        }

        
        // DELETE: api/leads/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Lead>>> DeleteLead(int id)
        {
            var result = await _leadRepository.DeleteAsync(id);

            if (!result)
            {
                var errorMessage = "Lead not found or deletion failed.";
                var errorResponse = new ApiResponse<bool>(false, errorMessage, false);
                return NotFound(errorResponse);
            }

            var successMessage = "Lead deleted successfully.";
            var successResponse = new ApiResponse<bool>(true, successMessage, true);

            return Ok(successResponse);
        }
    }
}
