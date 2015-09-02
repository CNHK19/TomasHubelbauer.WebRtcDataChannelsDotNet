namespace Rhaeo.WebRtc.Ice
{
  public sealed class PeerReflexiveIceCandidate
    : UdpIceCandidate
  {
    #region Constructors

    public PeerReflexiveIceCandidate(int foundation, int component, int priority, string address, int port)
      : base(foundation, component, priority, address, port)
    {
    }

    #endregion

    #region Properties

    public override IceCandidateType Type => IceCandidateType.PeerReflexive;

    #endregion
  }
}
