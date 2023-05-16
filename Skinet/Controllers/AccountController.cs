using API.Controllers;
using API.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> userManager;
    private readonly SignInManager<AppUser> signInManager;

    public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>>  login(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);    
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }
        var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password , false);
        if (result.Succeeded)
        {
            return Unauthorized(new ApiResponse(401));
        }
        return new UserDto
        {
            Email = loginDto.Email,
            Token = "here",
            DisplayName = user.DisplayName
        };  
    }
}
