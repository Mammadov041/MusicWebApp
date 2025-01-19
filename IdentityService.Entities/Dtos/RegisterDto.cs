using Microsoft.AspNetCore.Http;

namespace IdentityService.Entities.Dtos
{
    public class RegisterDto
    {
        public IFormFile? File {  get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
