using MediatR;

namespace RealTimeChat.Features.Commands.Chat
{
    public class CreateChat : IRequest<Guid>
    {
        public List<Guid> ListUsersId { get; set; }
        public List<string> Roles { get; set; }
        public bool IsGroup { get; set; }
        public string ChatName { get; set; }
        public CreateChat(List<Guid> listUsersId, List<string> roles ,bool isGroup, string chatName)
        {
            this.ListUsersId = listUsersId;
            this.Roles = roles;
            this.IsGroup = isGroup;
            this.ChatName = chatName;
        }
    }
}
