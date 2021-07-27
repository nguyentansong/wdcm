using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCM_Api.Data;
using WDCM_Api.Entities;
using WDCM_Api.Repository.Interfaces;

namespace WDCM_Api.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(WDCMDbContext context) : base(context)
        {
        }

        public User AuthenticateByPhone(string phone, string password)
        {
            User user = null;
            user = _context.Users.SingleOrDefault(x => x.Phone == phone);


            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public async Task Create(User User)
        {
            if (!string.IsNullOrWhiteSpace(User.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(User.Password, out passwordHash, out passwordSalt);
                User.PasswordHash = passwordHash;
                User.PasswordSalt = passwordSalt;
            }
            await _context.Users.AddAsync(User);
            await _context.SaveChangesAsync();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            //if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            //if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public async Task<bool> IsExistPhone(string phone)
        {
           return  await _context.Users.AnyAsync(x => x.Phone == phone);
        }

        public async Task<bool> IsExistSerial(string serial)
        {
            return await _context.Users.AnyAsync(x => x.SerialDevice == serial);
        }
    }
}
