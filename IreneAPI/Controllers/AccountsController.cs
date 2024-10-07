// Presentation Layer (Controller): Handles HTTP requests, input validation, and returns responses. This layer should be lightweight and free of business logic.

using IreneAPI.Services;
using IreneAPI.DTOs;
using IreneAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IreneAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;


namespace IreneAPI.Controllers;
// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountsController(TokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }
        //Writing the actual code for when users registers, all users would be initially given the role User, then an Admin can choose to promote or demote a Users role

    // Registration endpoint
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest model)
    {
        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
           
                // Assign User role to all users by default after registration
                await _userManager.AddToRoleAsync(user, "User"); // All other users get the User role by default
                // I can now manually edit it myself and make a user an admin, then use the nre admin to assign more user roles
            
                
            return Ok("User registered successfully!");
        }
        return BadRequest(result.Errors);
    }

    // Endpoint to assign a specific role to a user by an admin
    [Authorize(Roles="Admin")]
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] RoleAssignmentRequest model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if(user == null)
        {
            return NotFound("User not found");
        }

        var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
        if(!roleExists)
        {
            return BadRequest("Role does not exist");
        }
        await _userManager.AddToRoleAsync(user, model.RoleName);
        return Ok($"Role {model.RoleName} assigned to {user.UserName}");
    }
    
    // Login endpointUsers can sign in with either email or password
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest model) //Ensure the login is from the request body
    {
        ApplicationUser user;

        // A Find User logic
        // Check if the input is a valid email or username
        if(ValidEmailHelper.IsValidEmail(model.UserNameOrEmail))
        {
            // Find user by email 
            user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);

        }
        else{
            // Find user by username
            user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
        }

        if(user == null)
        {
            return Unauthorized("Invalid username,email, or password");
        }

        // Validate the password; false; indicates whether to persist the sign-in accross browser sessions. This is typically false for APIs. lockoutOnFailure: false, Specifies whether the user account should be locked out after a certain number of failed attempts... Another great feature 
        var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);
        if(!result.Succeeded)

        {
            return Unauthorized("Invalid username, email, or password");
        }

        // Generate JWT token
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user.Id, roles.FirstOrDefault() ?? "User");

        return Ok(new { Token = token});
    }

    // Testing out the roles
    [Authorize(Roles="Admin,Developer")]
    [HttpGet("dev-or-admin-data")]
    public IActionResult FindDevOrAdminData()
    {
        return Ok("Only Admins or Developers can see this");
    }
    [Authorize(Roles="User")]
    [HttpGet("user-data")]
    public IActionResult FindUserData()
    {
        return Ok("Only Users can see this");
    }

    // Checking User Roles: The endpoint below retrieves all the roles assigned to a user using the GetRolesAsync() method. 
    // A great endpoint, it can be used for displaying user roles in an admin panel
    [Authorize(Roles = "Admin, Developer")]
    [HttpGet("get-user-roles/{userId}")]
    public async Task<IActionResult> FindUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user == null)
        {
            return NotFound("User Not Found");
        }
        var roles = await _userManager.GetRolesAsync(user);
        return Ok(roles);
    }

    // Another endpoint to really test out the ASP.NET Identity [Authorize] attribute
    [Authorize(Roles="Admin")]
    [HttpGet("admin-data")]
    public IActionResult GetAdminData()
    {
        return Ok("This data is only accessible by admins");
    }

}