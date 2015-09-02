namespace Rhaeo.WebRtc.Ice
{
  public sealed class LocalHostIceCandidate
    : IceCandidate
  {
    #region Constructors

    public LocalHostIceCandidate(int foundation, int priority, string address, int port)
      : base(foundation, priority, address, port)
    {
    }

    #endregion

    #region Properties

    public override IceCandidateType Type => IceCandidateType.LocalHost;

    #endregion
  }
}
