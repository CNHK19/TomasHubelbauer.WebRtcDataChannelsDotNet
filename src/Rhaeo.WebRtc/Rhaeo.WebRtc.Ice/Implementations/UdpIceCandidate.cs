using Rhaeo.WebRtc.Ice.Declarations;

namespace Rhaeo.WebRtc.Ice.Implementations
{
  public abstract class UdpIceCandidate
    : IceCandidate, IUdpIceCandidate
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
