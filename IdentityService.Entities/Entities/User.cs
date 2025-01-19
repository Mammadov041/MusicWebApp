using Microsoft.AspNetCore.Identity;
using MusicWebApp.Core.Abstract;

namespace IdentityService.Entities.Entities
{
    public class User : IdentityUser,IEntity
    {
        public string? ImageUrl { get; set; }
    }
}
