namespace Rhaeo.WebRtc.Ice
{
  public sealed class RelayIceCandidate
    : UdpIceCandidate
  {
    #region Constructors

    public RelayIceCandidate(int foundation, int component, int priority, string address, int port)
      : base(foundation, component, priority, address, port)
    {
    }

    #endregion

    #region Properties

    public override IceCandidateType Type => IceCandidateType.Relay;

    #endregion
  }
}
