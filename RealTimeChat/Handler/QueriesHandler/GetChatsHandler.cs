using DataAccess.DbContext;
using Domain.Entities;
using MediatR;
using RealTimeChat.Features.Queries;
using Dapper;

namespace RealTimeChat.Handler.QueriesHandler
{
    public class GetChatsHandler : IRequestHandler<GetChats, IEnumerable<Chat>>
    {
        private readonly RealTimeDapperContext _dapperContext;
        public GetChatsHandler(RealTimeDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Chat>> Handle(GetChats request, CancellationToken cancellationToken)
        {
            var query = "SELECT c.* FROM Chats c JOIN ChatUser cu ON c.Id = cu.ChatId WHERE cu.UserId = @UserId AND c.Id = cu.ChatId;";
            using (var connection = _dapperContext.CreateConnection())
            {
                var chat = await connection.QueryAsync<Chat>(query, new { request.UserId });
                if (chat == null) { return null; }
                return chat;
            }
        }
    }
}
