using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth2.Entities;
using Auth2.Models;
using Auth2.Models.Result;
using Auth2.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AccountController(
            ApplicationContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet]
        public ResultDto Ok()
        {
            return new ResultDto
            {
                IsSuccessful = true
            };
        }

        [HttpPost("Register")]
        public async Task<ResultDto> Register([FromBody] RegisterDto model)
        {
            User user = new User() 
            { 
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email
            };
            await _userManager.CreateAsync(user, model.Password);
            UserAdditionalInfo ui = new UserAdditionalInfo()
            {
                Id = user.Id,
                Age = model.Age,
                FullName = model.FullName,
                Image = model.Image
            };
            await _context.UserAdditionalInfos.AddAsync(ui);
            await _context.SaveChangesAsync();

            return new ResultDto
            {
                IsSuccessful = true
            };

        }

        [HttpPost("login")]
        public async Task<ResultDto> Login(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded)
            {
                return new ResultDto
                {
                    IsSuccessful = false
                };
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            await _signInManager.SignInAsync(user, isPersistent: false);

            return new ResultLoginDto
            {
                IsSuccessful = true,
                Token = _jwtTokenService.CreateToken(user)
            };

        }
    }
}