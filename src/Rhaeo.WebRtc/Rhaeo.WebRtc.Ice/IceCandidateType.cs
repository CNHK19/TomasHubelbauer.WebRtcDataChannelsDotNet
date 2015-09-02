namespace Rhaeo.WebRtc.Ice
{
  /// <summary>
  /// Specifies the type of an ICE candidate.
  /// </summary>
  public enum IceCandidateType
  {
    /// <summary>
    /// The local host candidate.
    /// </summary>
    LocalHost,

    /// <summary>
    /// The server reflexive candidate provided by STUN server (and in many cases by TURN server, since they usually implement both).
    /// </summary>
    ServerReflexive,

    /// <summary>
    /// The relay candidate provided by TURN server.
    /// </summary>
    Relay,

    /// <summary>
    /// The peer reflexive candidate.
    /// </summary>
    PeerReflexive
  }
}
