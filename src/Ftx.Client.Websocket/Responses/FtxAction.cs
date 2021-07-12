using System.Runtime.Serialization;

namespace Ftx.Client.Websocket.Responses
{
    public enum FtxAction
    {
        Undefined,

        [DataMember(Name = "partial")]
        Partial,

        [DataMember(Name = "insert")]
        Insert,

        [DataMember(Name = "update")]
        Update,

        [DataMember(Name = "delete")]
        Delete
    }
}
