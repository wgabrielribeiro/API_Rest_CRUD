using Microsoft.EntityFrameworkCore;
using RestWithAspNET.API.Data.VO;
using RestWithAspNET.API.Model;
using RestWithAspNET.API.Model.Context;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestWithAspNET.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());

            return _context.Users.FirstOrDefault(u => u.UserName.Equals(user.UserName) && u.Password.Equals(pass));
        }
        public User ValidateCredentials(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }
        public User RefreshUserInfo(User user)
        {
            try
            {
                if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

                var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));

                if (result != null)
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();

                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool RevokeToken(string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == username);

            if (user is null) return false;

            user.RefreshToken = null;

            _context.SaveChanges();

            return true;
        }
        private string ComputeHash(string input, SHA256CryptoServiceProvider algorith)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorith.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

    }
}
