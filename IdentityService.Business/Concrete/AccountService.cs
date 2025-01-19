using IdentityService.Business.Abstract;
using IdentityService.Entities.Dtos;
using IdentityService.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Business.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IS3Service _awsService;


        public AccountService(IConfiguration configuration, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IS3Service awsService)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
            _awsService = awsService;
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
                var token = GetToken(authClaims);
                return new JwtSecurityTokenHandler().WriteToken(token).ToString();
            }
            return string.Empty;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
           
                var fileUrl = await _awsService.UploadFileAsync(dto.File.FileName, dto.File.OpenReadStream(), dto.File.ContentType);

            // Create the user with the provided details
            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email,
                ImageUrl = fileUrl // Save the relative path or default image URL
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            return result.Succeeded;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(8),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }
    }
}
