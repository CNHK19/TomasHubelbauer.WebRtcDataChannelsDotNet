using Rhaeo.WebRtc.Ice.Declarations;

namespace Rhaeo.WebRtc.Ice.Implementations
{
  // TODO: Document.
  public abstract class IceCandidate
    : IIceCandidate
  {
    #region Constructors

    protected IceCandidate(int foundation, int component, int priority, string address, int port)
    {
      // TODO: Check against http://tools.ietf.org/html/rfc5245#section-2.4.
      Foundation = foundation;

      // TODO: Check against http://tools.ietf.org/html/rfc5245#section-4.1.2.
      Priority = priority;

      // TODO: Limit to IPv4 as the connection SDP line will have IPv4 hardcoded. Match both IPv4 and IPv6, but throw on IPv6.
      Address = address;

      // TODO: Check against http://tools.ietf.org/html/rfc5245#section-4.1.1.1.
      Port = port;
    }

    #endregion

    #region Properties

    public int Foundation { get; }

    public int Component { get; }

    public abstract IceCandidateTransport Transport { get; }

    public int Priority { get; }

    public string Address { get; }

    public int Port { get; }

    public abstract IceCandidateType Type { get; }

    #endregion
  }
}
