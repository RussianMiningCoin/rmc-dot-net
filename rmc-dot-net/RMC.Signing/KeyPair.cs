using Org.BouncyCastle.Math;
using RMC.Signing.Utils;

namespace RMC.Signing
{
    using ECPrivateKeyParameters = Org.BouncyCastle.Crypto.Parameters.ECPrivateKeyParameters;
    using ECDSASigner = Org.BouncyCastle.Crypto.Signers.ECDsaSigner;
    using ECPoint = Org.BouncyCastle.Math.EC.ECPoint;
    using HMacDsaKCalculator = Org.BouncyCastle.Crypto.Signers.HMacDsaKCalculator;
    using Sha256Digest = Org.BouncyCastle.Crypto.Digests.Sha256Digest;

    public class KeyPair : VerifyingKey, IKeyPair
    {
        private readonly BigInteger _privKey;
        private ECDSASigner _signer;

        public KeyPair(BigInteger priv) : 
            this(priv, KeyGenerator.ComputePublicKey(priv))
        {
        }

        internal KeyPair(BigInteger priv, ECPoint pub) : base(pub)
        {
            _privKey = priv;
            InitSigner(priv);
        }

        private void InitSigner(BigInteger priv)
        {
            _signer = new ECDSASigner(new HMacDsaKCalculator(new Sha256Digest()));
            ECPrivateKeyParameters privKey = new ECPrivateKeyParameters(priv, Secp256K1.Parameters());
            _signer.Init(true, privKey);
        }

        public BigInteger Priv()
        {
            return _privKey;
        }

        public byte[] Sign(byte[] message)
        {
            return SignHash(Sha512.Half(message));
        }

        private byte[] SignHash(byte[] bytes)
        {
            return CreateEcdsaSignature(bytes).EncodeToDer();
        }

        private EcdsaSignature CreateEcdsaSignature(byte[] hash)
        {

            BigInteger[] sigs = _signer.GenerateSignature(hash);
            var r = sigs[0];
            var s = sigs[1];

            var otherS = Secp256K1.Order().Subtract(s);
            if (s.CompareTo(otherS) == 1)
            {
                s = otherS;
            }

            return new EcdsaSignature(r, s);
        }

        public string Id()
        {
            return Address.AddressCodec.EncodeAddress(this.PubKeyHash());
        }

        public override string ToString()
        {
            return Address.AddressCodec.EncodeKey(this.Priv().ToByteArrayUnsigned());
        }
    }

}