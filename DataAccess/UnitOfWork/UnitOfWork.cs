using DataAccess.DbContext;
using DataAccess.Repositories;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RealTimeDbContext _context;
        public IUserRepository Users {  get; private set; }
        public IMessageRepository Messages { get; private set; }
        public UnitOfWork(RealTimeDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Messages = new MessageRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
