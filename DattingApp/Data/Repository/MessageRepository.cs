using AutoMapper;
using AutoMapper.QueryableExtensions;
using DattingAppApi.DTOs;
using DattingAppApi.Entities;
using DattingAppApi.Helpers;
using DattingAppApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace DattingAppApi.Data.Repository
{
    public class MessageRepository(DataContext context, IMapper mapper) : IMessageRepository
    {
        public void AddGroup(Group group)
        {
            context.Groups.Add(group);
        }
        public void RemoveConnection(Connection connection)
        {
            context.Connections.Remove(connection);
        }

        public void AddMesage(Message message)
        {
            context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await context.Messages.FindAsync(id);
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = context.Messages.OrderByDescending(m => m.MessageSent).AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.SenderUsername == messageParams.Username && u.SenderDeleted == false),
                _ => query.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false && u.DateRead == null)
            };

            var messages = query.ProjectTo<MessageDto>(mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var querry =  context.Messages
                           .Where(m => m.Recipient.UserName == currentUsername && m.RecipientDeleted == false && m.Sender.UserName == recipientUsername
                                  || m.Recipient.UserName == recipientUsername && m.Sender.UserName == currentUsername && m.SenderDeleted == false)
                           .OrderBy(m => m.MessageSent)
                           .ProjectTo<MessageDto>(mapper.ConfigurationProvider)
                           .AsQueryable();

            var unreadMessages = querry.Where(m => m.DateRead == null && m.RecipientUsername == currentUsername).ToList();

            if (unreadMessages.Any())
            {
                unreadMessages.ForEach(x => x.DateRead = DateTime.UtcNow);
                await context.SaveChangesAsync();
            }

            return await querry.ProjectTo<MessageDto>(mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<Connection?> GetConnection(string connectionId)
        {
           return await context.Connections.FindAsync(connectionId);
        }

        public async Task<Group?> GetMessageGroup(string groupName)
        {
            return await context.Groups.Include(x => x.Connections).FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public async Task<Group?> GetGroupForConnection(string connectionId)
        {
           return await context.Groups
           .Include(x => x.Connections)
           .Where(x => x.Connections.Any(c => c.ConnectionId == connectionId))
           .FirstOrDefaultAsync();
        }
    }
}
