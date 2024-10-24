using Microsoft.AspNetCore.Mvc;
using UsermanagementApplication.Models;
using UsermanagementApplication.Data;
using System.Linq;
using System;

namespace UsermanagementApplication.Controllers{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase{
        private readonly UserContext _context;
        
    // constructor
        public UserController(UserContext context){
            _context = context;
        }

    // các phương thức xử lý API cho User sẽ được viết ở dưới đây
    [HttpPost("register")]


    public IActionResult Register([FromBody] RegisterRequest request) {
    var userExists = _context.Users.Any(u => u.Username == request.Username);
    if (userExists) return BadRequest("Username already taken");

    var user = new User {
        Username = request.Username,
        UserPassword = request.UserPassword, // Không mã hóa mật khẩu
        Email = request.Email
    };

    _context.Users.Add(user);
    _context.SaveChanges();

    return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request) {
    var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
    if (user == null || user.UserPassword != request.UserPassword)
        return Unauthorized("Invalid credentials");

    return Ok("Login successful");
    }

  [HttpPost("changepassword")]
    public IActionResult ChangePassword([FromBody] ChangePasswordRequest request) {
    var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
    if (user == null || user.UserPassword != request.OldPassword)
        return Unauthorized("Invalid credentials");

    user.UserPassword = request.NewPassword; // Không mã hóa mật khẩu
    _context.SaveChanges();

    return Ok("Password changed successfully");
    }

    public class RegisterRequest{
        public required string Username { get; set; }
        public required string UserPassword  { get; set; }
        public required string Email { get; set; }
    }

    public class LoginRequest{
        public string Username { get; set; }
        public string UserPassword  { get; set; }
    }

    public class ChangePasswordRequest {
    public required string Username { get; set; }
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
    }

    }
}