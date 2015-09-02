namespace Rhaeo.WebRtc.Ice
{
  public sealed class PeerReflexiveIceCandidate
    : IceCandidate
  {
    #region Constructors

    public PeerReflexiveIceCandidate(int foundation, int priority, string address, int port)
      : base(foundation, priority, address, port)
    {
    }

    #endregion

    #region Properties

    public override IceCandidateType Type => IceCandidateType.PeerReflexive;

    #endregion
  }
}
