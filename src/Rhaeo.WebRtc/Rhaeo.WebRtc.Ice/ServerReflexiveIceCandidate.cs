namespace Rhaeo.WebRtc.Ice
{
  public sealed class ServerReflexiveIceCandidate
    : UdpIceCandidate
  {
    #region Constructors

    public ServerReflexiveIceCandidate(int foundation, int component, int priority, string address, int port)
      : base(foundation, component, priority, address, port)
    {
    }

    #endregion

    #region Properties

    public override IceCandidateType Type => IceCandidateType.ServerReflexive;

    #endregion
  }
}
