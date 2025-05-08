using DattingAppApi.DTOs;
using DattingAppApi.Entities;
using DattingAppApi.Helpers;

namespace DattingAppApi.Interfaces.Repository
{
    public interface ILikesRepository
    {
        Task<UserLike?> GetUserLike(int sourceUserId, int targetUserId);
        
        Task<PagedList<MemberDto>> GetUserLikes(LikesParams likesParams);

        //Task<IEnumerable<MemberDto>> GetUserLikes(string predicate, int userId);

        Task<IEnumerable<int>> GetCurrentUserLikeIds(int currentUserId);

        void DeleteLike(UserLike like);

        void AddLike(UserLike like);
    }
}
