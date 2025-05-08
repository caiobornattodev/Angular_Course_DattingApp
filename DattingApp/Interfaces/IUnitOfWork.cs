using System;
using DattingAppApi.Interfaces.Repository;

namespace DattingAppApi.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository {get;}
    IMessageRepository MessageRepository {get;}
    ILikesRepository LikesRepository {get;}

    Task<bool> Complete();

    bool HasChanges();
 }
