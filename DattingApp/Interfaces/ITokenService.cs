using DattingAppApi.Entities;

namespace DattingAppApi.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}
