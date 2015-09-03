using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Rhaeo.WebRtc
{
  public sealed class PeerConnection
  {
    #region Constructors

    public PeerConnection(params Uri[] iceServers)
    {
      IceServers = iceServers;
    }

    #endregion

    #region Properties

    public IEnumerable<Uri> IceServers { get; }

    public AsyncLazy<Sdp.Sdp> LocalDescription => new AsyncLazy<Sdp.Sdp>(async () => new Sdp.Sdp(await new Ice.Implementations.Ice(IceServers).GetUdpIceCandidates().ToList()));

    #endregion

    #region Methods

    #endregion
  }
}
