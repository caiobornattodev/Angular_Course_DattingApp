using DattingAppApi.DTOs;
using DattingAppApi.Entities;
using DattingAppApi.Helpers;

namespace DattingAppApi.Interfaces.Repository
{
    public interface IUserRepository
    {
        void Update(AppUser user);

        Task<bool> SaveAllAsync();

        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<AppUser?> GetUserByIdAsync(int id);

        Task<AppUser?> GetUserByUsernameAsync(string username);

        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);

        Task<MemberDto> GetMemberAsync(string username);
    }
}
