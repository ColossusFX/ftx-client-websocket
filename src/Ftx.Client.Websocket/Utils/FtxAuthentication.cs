using System.Security.Cryptography;
using System.Text;

namespace Ftx.Client.Websocket.Utils
{
    public class FtxAuthentication
    {
        public static long CreateAuthNonce(long? time = null)
        {
            return time ?? FtxTime.NowMs();
        }

        public static string CreateAuthPayload(long nonce)
        {
            return $"{nonce}websocket_login";
        }

        public static string CreateSignature(string secret, string message)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secret);
            var messageBytes = Encoding.UTF8.GetBytes(message);


            string ByteToString(byte[] buff)
            {
                var builder = new StringBuilder();

                for (var i = 0; i < buff.Length; i++)
                {
                    builder.Append(buff[i].ToString("X2")); // hex format
                }
                return builder.ToString();
            }

            using (var hmacsha256 = new HMACSHA256(keyBytes))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return ByteToString(hashmessage).ToLower();
            }
        }
    }
}
