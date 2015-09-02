namespace Rhaeo.WebRtc.Ice
{
  public abstract class UdpIceCandidate
    : IceCandidate
  {
    #region Constructors

    protected UdpIceCandidate(int foundation, int component, int priority, string address, int port)
      : base(foundation, component, priority, address, port)
    {
    }

    #endregion

    #region Properties

    public override IceCandidateTransport Transport => IceCandidateTransport.Udp;

    #endregion
  }
}
