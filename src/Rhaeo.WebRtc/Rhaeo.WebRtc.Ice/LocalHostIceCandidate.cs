namespace Rhaeo.WebRtc.Ice
{
  public sealed class LocalHostIceCandidate
    : UdpIceCandidate
  {
    #region Constructors

    public LocalHostIceCandidate(int foundation, int component, int priority, string address, int port)
      : base(foundation, component, priority, address, port)
    {
    }

    #endregion

    #region Properties

    public override IceCandidateType Type => IceCandidateType.LocalHost;

    #endregion
  }
}
