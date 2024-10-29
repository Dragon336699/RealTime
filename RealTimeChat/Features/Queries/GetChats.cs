using Domain.Entities;
using MediatR;

namespace RealTimeChat.Features.Queries
{
    public class GetChats : IRequest<IEnumerable<Chat>>
    {
        public int userId { get; set; }
        public GetChats(int UserId)
        {
            userId = UserId;
        }
    }
}
