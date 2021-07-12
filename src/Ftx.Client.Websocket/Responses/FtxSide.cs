using System.Runtime.Serialization;

namespace Ftx.Client.Websocket.Responses
{
    public enum FtxSide
    {
        [DataMember(Name = "")]
        Undefined,

        [DataMember(Name = "buy")]
        Buy,

        [DataMember(Name = "sell")]
        Sell
    }
}
