using DattingAppApi.Entities;

namespace DattingAppApi.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(AppUser user);
    }
}
