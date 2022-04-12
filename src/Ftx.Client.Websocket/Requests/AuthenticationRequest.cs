using System.Runtime.Serialization;
using Ftx.Client.Websocket.Messages;
using Ftx.Client.Websocket.Utils;
using Ftx.Client.Websocket.Validations;
using Newtonsoft.Json;

namespace Ftx.Client.Websocket.Requests
{
    public class AuthenticationRequest : RequestBase
    {
        private readonly string _apiKey;
        private readonly string _authSig;
        private readonly long _authNonce;
        private readonly string _subAccount;

        public AuthenticationRequest(string apiKey, string apiSecret, string subAccount=null)
        {
            FtxValidations.ValidateInput(apiKey, nameof(apiKey));
            FtxValidations.ValidateInput(apiSecret, nameof(apiSecret));

            _apiKey = apiKey;
            _authNonce = FtxAuthentication.CreateAuthNonce();
            _subAccount = subAccount;

            if (string.IsNullOrWhiteSpace(subAccount))
                _subAccount = null;
            
            var authPayload = FtxAuthentication.CreateAuthPayload(_authNonce);
            
            _authSig = FtxAuthentication.CreateSignature(apiSecret, authPayload);
        }

        [IgnoreDataMember]
        public override MessageType Operation => MessageType.Login;

        public Args Args => new Args
        {
            Key = _apiKey,
            Sign = _authSig,
            Time = _authNonce,
            SubAccount = _subAccount
        };
    }
    
    public class Args
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
        
        [JsonProperty("subaccount",NullValueHandling = NullValueHandling.Ignore)]
        public string SubAccount { get; set; }

        
    }
}
