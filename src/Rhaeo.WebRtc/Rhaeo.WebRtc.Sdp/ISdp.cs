using Rhaeo.WebRtc.Ice.Declarations;
using System.Collections.Generic;

namespace Rhaeo.WebRtc.Sdp
{
  public interface ISdp
  {
    #region Properties

    IList<IUdpIceCandidate> UdpIceCandidates { get; }

    IIceCharString UserName { get; }

    IIceCharString Password { get; }

    byte[] Fingerprint { get; }

    #endregion
  }
}
