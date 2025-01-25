using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Model;


namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public UserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create a new user
        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] AddInformationRequest request)
        {
            try
            {
                var Users = new Entities.Users
                {
                    user_Name = request.user_Name,
                    Email = request.Email,
                    Password = request.Password,
                    //CreatedDate = DateTime.UtcNow
                };

                _dbContext.Users.Add(Users);
                await _dbContext.SaveChangesAsync();

                return Ok(new AddInformationResponse
                {
                    IsSuccess = true,
                    Message = "User successfully added."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { IsSuccess = false, Message = $"An error occurred: {ex.Message}" });
            }
        }

        // Get all users
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _dbContext.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }
        }

        // login api
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                // Find the user by email
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

                if (user == null)
                {
                    return Unauthorized(new { IsSuccess = false, Message = "User not found." });
                }

                // Validate password (assuming passwords are stored in plaintext; use hashing in production)
                if (user.Password != request.Password)
                {
                    return Unauthorized(new { IsSuccess = false, Message = "Invalid password." });
                }

                // Return success response
                return Ok(new { IsSuccess = true, Message = "Login successful.", UserName = user.user_Name, userID= user.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { IsSuccess = false, Message = $"An error occurred: {ex.Message}" });
            }
        }

        // Update a user
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] AddInformationRequest request)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found." });
                }

                user.user_Name = request.user_Name;
                user.Email = request.Email;
                user.Password = request.Password;

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                return Ok(new { Message = "User successfully updated." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }
        }

        // Delete a user
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found." });
                }

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                return Ok(new { Message = "User successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}

