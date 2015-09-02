namespace Rhaeo.WebRtc.Sdp
{
  /// <summary>
  /// https://tools.ietf.org/html/rfc4145#section-4
  /// </summary>
  public enum SdpSetup
  {
    /// <summary>
    /// The endpoint will initiate an outgoing connection.
    /// </summary>
    Active,

    /// <summary>
    /// The endpoint will accept an incoming connection.
    /// </summary>
    Passive,

    /// <summary>
    /// The endpoint is willing to accept an incoming connection or to initiate an outgoing connection.
    /// </summary>
    ActivePassive,

    /// <summary>
    /// The endpoint does not want the connection to be established for the time being.
    /// </summary>
    HoldConnection
  }
}
