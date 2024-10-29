using DataAccess.DbContext;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User> ,IUserRepository
    {
        public UserRepository(RealTimeDbContext context) : base(context)
        {
            
        }

        public User GetUserByToken(string token)
        {
            var userMail = GetUserMail(token);
            return _context.Set<User>().FirstOrDefault(u => u.Email == userMail);
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> expression)
        {
            return _context.Set<User>().Where(expression).ToList();
        }

        private string GetUserMail(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            return emailClaim?.Value;
        }
    }
}
