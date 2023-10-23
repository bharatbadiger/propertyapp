namespace RealEstate.Controllers
{
    using global::RealEstate.Models;
    using global::RealEstate.Models.DTO;
    using global::RealEstate.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        //private readonly List<Property> _properties; // Replace with your data source (e.g., database)
        //private readonly ApplicationDbContext _context; // Replace with your DbContext
        //public PropertiesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        private readonly IRepository<Property> _propertyRepository;

        public PropertiesController(IRepository<Property> propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        //public PropertiesController()
        //{
        //    // Initialize your data source (e.g., from a database)
        //    _properties = new List<Property>
        //{
        //    new Property
        //    {
        //        Id = Guid.NewGuid(),
        //        Title = "Sample Property",
        //        Subtitle = "Beautiful home for sale",
        //        Price = 250000,
        //        NumberOfRooms = 4,
        //        BHK = 2,
        //        Location = "Sample Location",
        //        City = "Sample City",
        //        Images = new List<PropertyImage>(),
        //        Type = PropertyType.Property,
        //        Area = 1500,
        //        AreaUnit = AreaUnit.Sqft,
        //        Description = "This is a sample property description."
        //    }
        //    // Add more sample properties or load from your data source
        //};
        //}

        // GET: api/properties
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Property>>> GetProperties()
        public async Task<ActionResult<IEnumerable<Property>>> GetProperties()
        {
            var properties = await _propertyRepository.GetAllAsync();

            if (properties == null || !properties.Any())
            {
                var errorMessage = "No properties found.";
                var errorResponse = new ApiResponse<IEnumerable<Property>>(false, errorMessage, null);
                return NotFound(errorResponse);
            }

            var successMessage = "Properties retrieved successfully.";
            var successResponse = new ApiResponse<IEnumerable<Property>>(true, successMessage, properties);

            return Ok(successResponse);
        }

        // GET: api/properties/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Property>>> GetProperty(int id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);

            if (property == null)
            {
                var response = new ApiResponse<Property>(false, "Property not found", null);
                return NotFound(response);
            }
            return Ok(new ApiResponse<Property>(true, "Property retrieved successfully", property));
        }

        [HttpGet("city/{city}")]
        public async Task<ActionResult<ApiResponse<Property>>> GetPropertyByCity(string city)
        {
            PropertyRepository propertyRepository1 = (PropertyRepository)_propertyRepository;
            var property = await propertyRepository1.GetPropertiesByCityAsync(city);
            if (property.Count() >0)
            {
                return Ok(new ApiResponse<IEnumerable<Property>>(true, $"Properties for {city} retrieved successfully", property));

            }
            return NotFound(new ApiResponse<Property>(true, $"Properties for {city} not Found", null));
        }

        // POST: api/properties
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Property>>> CreateProperty([FromBody] Property property)
        {
            if (property == null)
            {
                var errorMessage = "Invalid request data. Property object is null.";
                var errorResponse = new ApiResponse<Property>(false, errorMessage, null);
                return BadRequest(errorResponse);
            }

            var createdProperty = await _propertyRepository.CreateAsync(property);

            if (createdProperty == null)
            {
                var errorMessage = "Failed to create the property.";
                var errorResponse = new ApiResponse<Property>(false, errorMessage, null);
                return BadRequest(errorResponse);
            }

            var successMessage = "Property created successfully";
            var successResponse = new ApiResponse<Property>(true, successMessage, createdProperty);

            return CreatedAtAction(nameof(GetProperty), new { id = createdProperty.Id }, successResponse);
        }

        // PUT: api/properties/{id}
        [HttpPut("{id}")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ApiResponse<Property>>> UpdateProperty(int id, [FromBody] Property  updatedProperty)
        {
            var property = await _propertyRepository.UpdateAsync(id, updatedProperty);

            if (property == null)
            {
                var errorMessage = "Property not found or update failed.";
                var errorResponse = new ApiResponse<Property>(false, errorMessage, null);
                return NotFound(errorResponse);
            }

            var successMessage = "Property updated successfully.";
            var successResponse = new ApiResponse<Property>(true, successMessage, property);

            return Ok(successResponse);
        }

        
        // DELETE: api/properties/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Property>>> DeleteProperty(int id)
        {
            var result = await _propertyRepository.DeleteAsync(id);

            if (!result)
            {
                var errorMessage = "Property not found or deletion failed.";
                var errorResponse = new ApiResponse<bool>(false, errorMessage, false);
                return NotFound(errorResponse);
            }

            var successMessage = "Property deleted successfully.";
            var successResponse = new ApiResponse<bool>(true, successMessage, true);

            return Ok(successResponse);
        }
    }
}
