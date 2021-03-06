using System;
using Newtonsoft.Json.Linq;
using RMC.Address;
using RMC.Core.Binary;
using RMC.Core.Util;

namespace RMC.Core.Types
{
    public class AccountId : Hash160
    {
        private string _encoded;
        private string Encoded
        {
            get
            {
                return _encoded ?? (
                    _encoded = 
                    AddressCodec.EncodeData(Buffer, AddressCodec.AccountId));
            }
            set { _encoded = value; }
        }

        public AccountId(byte[] hash, string encoded) : base(hash)
        {
            Encoded = encoded;
        }

        public AccountId(string v) :
            this(AddressCodec.DecodeData(v, AddressCodec.AccountId), v) {}
        public AccountId(byte[] hash) :
            this(hash, AddressCodec.EncodeData(hash, AddressCodec.AccountId)) {}

        public static implicit operator AccountId(string value)
        {
            return new AccountId(value);
        }

        public static implicit operator AccountId(uint value)
        {
            byte[] empty = new byte[20];
            var sourceArray = Bits.GetBytes(value);
            Array.Copy(sourceArray, 0, empty, 16, 4);
            return new AccountId(empty);
        }
        public static implicit operator AccountId(JToken json)
        {
            return json == null ? null : FromJson(json);
        }

        public static implicit operator JToken(AccountId v)
        {
            return v.ToString();
        }

        public override string ToString()
        {
            return Encoded;
        }

        public new static AccountId FromJson(JToken json)
        {
            return json?.ToString();
        }

        public static readonly AccountId Zero = 0;
        public static readonly AccountId Neutral = 1;

        public new static AccountId FromParser(BinaryParser parser, int? hint=null)
        {
            return new AccountId(parser.Read(20));
        }
    }
}