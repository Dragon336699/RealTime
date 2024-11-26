using Dapper;
using DataAccess.DbContext;
using Domain.Entities;
using MediatR;
using RealTimeChat.Features.Commands.Chat;

namespace RealTimeChat.Handler.CommandsHandler.ChatHandler
{
    public class CreateChatHandler : IRequestHandler<CreateChat, Guid>
    {
        private readonly RealTimeDapperContext _dapperContext;
        public CreateChatHandler(RealTimeDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<Guid> Handle(CreateChat request, CancellationToken cancellationToken)
        {
            var queryGetChat = @"
            SELECT ""ChatId""
            FROM ""ChatUser""
            WHERE ""UserId"" = ANY(@UsersId)
            GROUP BY ""ChatId""
            HAVING COUNT(DISTINCT ""UserId"") = @NumberOfUsersId;";

            var queryCreateChat = @"
            WITH new_chat AS (
            INSERT INTO ""Chat"" (""ChatName"", ""IsGroup"", ""CreatedAt"")
            VALUES (@ChatName, @IsGroup, NOW())
            RETURNING ""Id"")
            INSERT INTO ""ChatUser"" (""ChatId"", ""UserId"", ""NickName"", ""Role"", ""JoinedAt"")
            VALUES {0}
            RETURNING (SELECT ""Id"" FROM new_chat)";

            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("UsersId", request.ListUsersId.ToArray());
                    param.Add("NumberOfUsersId", request.ListUsersId.Count());
                    var chatId = await connection.QueryFirstOrDefaultAsync<Guid>(queryGetChat, param);
                    if (chatId != Guid.Empty) {
                        return Guid.Empty;
                    }

                    var chatValue = string.Join(",", request.ListUsersId.Select((userId, index) => $@"
                    ((SELECT ""Id"" FROM new_chat),
                    '{userId}',
                    (SELECT ""FirstName"" FROM ""User"" WHERE ""Id"" = '{userId}'),
                    '{request.Roles[index]}',
                    NOW()
                    )"));
                    var queryPrivateChat = string.Format(queryCreateChat, chatValue);

                    param = new DynamicParameters();
                    param.Add("ChatName", request.ChatName);
                    param.Add("IsGroup", request.IsGroup);

                    var newChatId = await connection.QueryFirstOrDefaultAsync<Guid>(queryPrivateChat, param);
                    return newChatId;
                }
                catch (Exception ex) { return Guid.Empty; }
            } 
        }
    }
}
