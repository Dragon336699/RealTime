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
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(RealTimeDbContext context) : base(context)
        {
            
        }

        public void Add (Message message)
        {
            try
            {
                _context.Set<Message>().Add(message);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
