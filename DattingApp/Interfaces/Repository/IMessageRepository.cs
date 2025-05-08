using DattingAppApi.DTOs;
using DattingAppApi.Entities;
using DattingAppApi.Helpers;

namespace DattingAppApi.Interfaces.Repository
{
    public interface IMessageRepository
    {
        void AddMesage(Message message);

        void DeleteMessage(Message message);

        Task<Message> GetMessage(int id);

        Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);

        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);

        void AddGroup(Group group);

        void RemoveConnection(Connection connection);

        Task<Connection?> GetConnection(string connectionId);

        Task<Group?> GetMessageGroup(string groupName);

        Task<Group?> GetGroupForConnection(string connectionId);
    }
}
