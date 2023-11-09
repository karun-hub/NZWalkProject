using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models.DTO;
using Demo.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        public readonly UserManager<IdentityUser> userManager ;
        public readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.tokenRepository = tokenRepository;
            this.userManager = userManager;
            
        }
        
        [HttpPost]
        [Route("Register")]
       public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
       {
        var identityUser= new IdentityUser
        {
            UserName = registerRequestDTO.Username,
            Email= registerRequestDTO.Username
        };
           var identityResult= await userManager.CreateAsync(identityUser, registerRequestDTO.Password);
           if( identityResult.Succeeded)
           {
                // add rols to this users
                if(registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult=  await userManager.AddToRolesAsync(identityUser,registerRequestDTO.Roles);
                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please Login");
                    }
                }

           }
            return BadRequest("Something Went Wrang");
       }
       [HttpPost]
       [Route("Login")]
       public async Task<IActionResult> Login([FromBody]LoginRequestDTO loginRequestDTO){
        var user =await userManager.FindByEmailAsync(loginRequestDTO.UserName);
        if(user != null)
        {
            var checkPasswordResult =await userManager.CheckPasswordAsync(user,loginRequestDTO.Password);
            if(checkPasswordResult)
            {
                //Get roles for this user
                 var roles= await userManager.GetRolesAsync(user);
                if(roles!= null)
                {
                     // Create token
                     var jwtToken =  tokenRepository.CreateJWTToken(user, roles.ToList());
                     var response = new LoginResponseDTO
                     {
                        JwtToken= jwtToken
                     };
                    return Ok(response);
                }
               
                
            }
        }
        return BadRequest("Username or password mismatch");

       }
    }
}