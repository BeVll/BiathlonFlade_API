using MB_API.Data.Entities.Identity;

namespace MB_API.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(UserEntity user);
    }
}
