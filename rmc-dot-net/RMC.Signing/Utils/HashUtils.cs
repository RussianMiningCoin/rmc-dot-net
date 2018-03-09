﻿using System.Security.Cryptography;

namespace RMC.Signing.Utils
{
    internal class HashUtils
    {
        public static byte[] PublicKeyHash(byte[] bytes)
        {
            var hash = SHA256.Create();
            var riper = RIPEMD160.Create();
            bytes = hash.ComputeHash(bytes, 0, bytes.Length);
            return riper.ComputeHash(bytes, 0, bytes.Length);
        }
    }
}