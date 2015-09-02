namespace Rhaeo.WebRtc.Ice
{
  /// <summary>
  /// Specifies the ICE candidate transport.
  /// </summary>
  /// <remarks>ICE candidate transport is defined in https://tools.ietf.org/html/rfc5245#section-15.1.</remarks>
  public enum IceCandidateTransport
  {
    /// <summary>
    /// UDP transport.
    /// </summary>
    /// <remarks>UDP transport is always used with Data Channel.</remarks>
    Udp,

    /// <summary>
    /// TCP transport.
    /// </summary>
    /// <remarks>TCP transport is never used with Data Channel.</remarks>
    Tcp,

    /// <summary>
    /// Transport extension - currently not supported.
    /// </summary>
    Extension
  }
}
