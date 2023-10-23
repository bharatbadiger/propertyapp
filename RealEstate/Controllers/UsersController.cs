namespace RealEstate.Controllers
{
    namespace RealEstate.Controllers
    {
        using global::RealEstate.Models;
        using global::RealEstate.Models.DTO;
        using global::RealEstate.Repositories;
        using Microsoft.AspNetCore.Mvc;
        using System.Collections.Generic;

        [Route("api/[controller]")]
        [ApiController]
        public class UsersController : ControllerBase
        {

            private readonly IRepository<User> _genericRepository;

            public UsersController(IRepository<User> userRepository)
            {
                _genericRepository = userRepository;
            }

            
            // GET: api/users
            [HttpGet]
            public async Task<ActionResult<IEnumerable<User>>> GetUsers()
            {
                var users = await _genericRepository.GetAllAsync();

                if (users == null || !users.Any())
                {
                    var errorMessage = "No Users found.";
                    var errorResponse = new ApiResponse<IEnumerable<User>>(false, errorMessage, null);
                    return NotFound(errorResponse);
                }

                var successMessage = "Users retrieved successfully.";
                var successResponse = new ApiResponse<IEnumerable<User>>(true, successMessage, users);

                return Ok(successResponse);
            }

            // GET: api/users/{id}
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<ApiResponse<User>>> GetUser(int id)
            {
                    var user = await _genericRepository.GetByIdAsync(id);

                if (user == null)
                {
                    var response = new ApiResponse<User>(false, "User not found", null);
                    return NotFound(response);
                }

                return Ok(new ApiResponse<User>(true, "User retrieved successfully", user));
            }

            // GET: api/users/phone/{phoneNumber}
            [HttpGet("mobileNo/{mobileNo}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<ApiResponse<User>>> GetUserByPhone([FromServices] UserRepository _userRepository,string mobileNo)
            {
                var user = await _userRepository.GetUserByMobileNoAsync(mobileNo);

                if (user == null)
                {
                    var response = new ApiResponse<User>(false, "User not found", null);
                    return NotFound(response);
                }

                return Ok(new ApiResponse<User>(true, "User retrieved successfully", user));
            }


            // GET: api/users/email/{email}
            [HttpGet("email")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<ApiResponse<User>>> GetUserphoneByEmail([FromServices] UserRepository _userRepository, [FromQuery] string email)
            {
                var user = await _userRepository.GetUserByEmailAsync(email);

                if (user == null)
                {
                    var response = new ApiResponse<User>(false, "User not found", null);
                    return NotFound(response);
                }

                return Ok(new ApiResponse<User>(true, "User retrieved successfully", user));
            }


            // POST: api/users
            [HttpPost]
            public async Task<ActionResult<ApiResponse<User>>> CreateUser([FromBody] User user)
            {
                if (user == null)
                {
                    var errorMessage = "Invalid request data. User object is null.";
                    var errorResponse = new ApiResponse<User>(false, errorMessage, null);
                    return BadRequest(errorResponse);
                }

                var createdUser = await _genericRepository.CreateAsync(user);

                if (createdUser == null)
                {
                    var errorMessage = "Failed to create the user.";
                    var errorResponse = new ApiResponse<User>(false, errorMessage, null);
                    return BadRequest(errorResponse);
                }

                var successMessage = "User created successfully";
                var successResponse = new ApiResponse<User>(true, successMessage, createdUser);

                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, successResponse);
            }

            // PUT: api/users/{id}
            [HttpPut("{id}")]
            public async Task<ActionResult<ApiResponse<User>>> UpdateUser(int id, [FromBody] User updatedUser)
            {
                var user = await _genericRepository.UpdateAsync(id, updatedUser);

                if (user == null)
                {
                    var errorMessage = "User not found or update failed.";
                    var errorResponse = new ApiResponse<User>(false, errorMessage, null);
                    return NotFound(errorResponse);
                }

                var successMessage = "User updated successfully.";
                var successResponse = new ApiResponse<User>(true, successMessage, user);

                return Ok(successResponse);
            }


            // DELETE: api/users/{id}
            [HttpDelete("{id}")]
            public async Task<ActionResult<ApiResponse<User>>> DeleteUser(int id)
            {
                var result = await _genericRepository.DeleteAsync(id);

                if (!result)
                {
                    var errorMessage = "User not found or deletion failed.";
                    var errorResponse = new ApiResponse<bool>(false, errorMessage, false);
                    return NotFound(errorResponse);
                }

                var successMessage = "User deleted successfully.";
                var successResponse = new ApiResponse<bool>(true, successMessage, true);

                return Ok(successResponse);
            }
        }
    }

}
