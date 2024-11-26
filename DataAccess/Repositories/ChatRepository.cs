using DataAccess.DbContext;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ChatRepository : GenericRepository<Chat> , IChatRepository
    {
        public ChatRepository(RealTimeDbContext context) : base(context)
        {

        }
    }
}
