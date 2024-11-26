using Domain.Entities;
using MediatR;

namespace RealTimeChat.Features.Queries
{
    public class GetChats : IRequest<IEnumerable<Chat>>
    {
        public Guid UserId { get; set; }
        public GetChats(Guid UserId)
        {
            this.UserId = UserId;
        }
    }
}
