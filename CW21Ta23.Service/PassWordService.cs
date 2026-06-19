using BookStore.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BookStore.Service
{
    public class PasswordService
    {
        public static string GetHashPassword(string password) 
        { 
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);

                return Convert.ToHexString(hash).ToLower();
            }
        }
        

        public static bool VerifyPassword(string hashPassword , string password )
        {
           var newPassword = GetHashPassword(password);

            return newPassword.Equals(hashPassword);

        }
    }
}
