﻿using jwtStore.core.Context.SharedContext.ValueObjects;
using JwtStore.Core;
using System.Security.Cryptography;

namespace jwtStore.core.Context.AccountContext.ValueObjects
{
    public class Password : ValueObject
    {
        private const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const string Special = "!@#$%ˆ&*(){}[];";


        public string Hash { get; } = string.Empty;
        public string ResetCode { get; } = Guid.NewGuid().ToString("N")[..8].ToLower();

        protected Password()
        {
        }

        public Password(string? text = null)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                text = Generate();

            Hash = Hashing(text);
        }

        public bool Challenge(string plainTextPassword) => Verify(Hash, plainTextPassword);

        private static string Generate(short lenght = 16, bool includeSpecialChars = true, bool uppCase = false)
        {
            var chars = includeSpecialChars ? Valid + Special : Valid;
            var startRandom = uppCase ? 26 : 0;
            var index = 0;
            var result = new char[lenght];
            var random = new Random();

            while (index < lenght)
                result[index++] = chars[random.Next(startRandom, chars.Length)];


            return new string(result);
        }


        private static string Hashing(
        string password,
        short saltSize = 16,
        short keySize = 32,
        int iterations = 10000,
        char splitChar = '.')
        {
            if (string.IsNullOrEmpty(password))
                throw new Exception("Password should not be null or empty");

            password += Configuration.Secrets.PasswordSaltKey;

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                saltSize,
                iterations,
                HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{iterations}{splitChar}{salt}{splitChar}{key}";
        }


        private static bool Verify(
          string hash,
          string password,
          short keySize = 32,
          int iterations = 10000,
          char splitChar = '.')
        {
            password += Configuration.Secrets.PasswordSaltKey;

            var parts = hash.Split(splitChar, 3);
            if (parts.Length != 3)
                return false;

            var hashIterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            if (hashIterations != iterations)
                return false;

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256);
            var keyToCheck = algorithm.GetBytes(keySize);

            return keyToCheck.SequenceEqual(key);
        }


    }
}