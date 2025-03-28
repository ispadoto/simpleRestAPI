﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRestAPI.Shared.Utils
{
    public static class PasswordUtils
    {
        public static string CalculateHash(string input)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(input, 10);

            return passwordHash;
        }

        public static bool VerifyPassword(string input, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(input, passwordHash);
        }

        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-#$@&?_()[]^~";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }

}
