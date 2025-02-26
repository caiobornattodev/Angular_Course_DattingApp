﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using DattingAppApi.DTOs;
using DattingAppApi.Entities;
using DattingAppApi.Helpers;
using DattingAppApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DattingAppApi.Data.Repository
{
    public class LikesRepository(DataContext context, IMapper mapper) : ILikesRepository
    {
        public void AddLike(UserLike like)
        {
            context.Likes.Add(like);
        }

        public void DeleteLike(UserLike like)
        {
            context.Likes.Remove(like);
        }

        public async Task<IEnumerable<int>> GetCurrentUserLikeIds(int currentUserId)
        {
            return await context.Likes
                .Where(x => x.SourceUserId == currentUserId)
                .Select(x => x.TargetUserId)
                .ToListAsync();
        }

        public async Task<UserLike?> GetUserLike(int sourceUserId, int targetUserId)
        {
            return await context.Likes.FindAsync(sourceUserId, targetUserId);
        }

        //public async Task<IEnumerable<MemberDto>> GetUserLikes(string predicate, int userId)
        //{
        //    var likes = context.Likes.AsQueryable();
        //    //IQueryable<MemberDto> query;

        //    switch (predicate)
        //    {
        //        case "liked":

        //            return await likes
        //                .Where(x => x.SourceUserId == userId)
        //                .Select(x => x.TargetUser)
        //                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
        //                .ToListAsync();

        //        //break;
        //        case "likedBy":

        //            return await likes
        //              .Where(x => x.TargetUserId == userId)
        //              .Select(x => x.SourceUser)
        //              .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
        //              .ToListAsync();


        //        //break;
        //        default:
        //            var likeIds = await GetCurrentUserLikeIds(userId);

        //            return await likes
        //            .Where(x => x.TargetUserId == userId && likeIds.Contains(x.SourceUserId))
        //            .Select(x => x.SourceUser)
        //            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
        //            .ToListAsync();
        //    }
        //}

        public async Task<PagedList<MemberDto>> GetUserLikes(LikesParams likesParams)
        {
            var likes = context.Likes.AsQueryable();
            IQueryable<MemberDto> query;

            switch (likesParams.Predicate)
            {
                case "liked":
                    query = likes
                        .Where(x => x.SourceUserId == likesParams.UserId)
                        .Select(x => x.TargetUser)
                        .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                    break;
                case "likedBy":
                    query = likes
                        .Where(x => x.TargetUserId == likesParams.UserId)
                        .Select(x => x.SourceUser)
                        .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                    break;
                default:
                    var likeIds = await GetCurrentUserLikeIds(likesParams.UserId);

                    query = likes
                        .Where(x => x.TargetUserId == likesParams.UserId && likeIds.Contains(x.SourceUserId))
                        .Select(x => x.SourceUser)
                        .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                    break;
            }

            return await PagedList<MemberDto>.CreateAsync(query, likesParams.PageNumber, likesParams.PageSize);
        }


        public async Task<bool> SaveChanges()
        {
          return await context.SaveChangesAsync() > 0;  
        }
    }
}
