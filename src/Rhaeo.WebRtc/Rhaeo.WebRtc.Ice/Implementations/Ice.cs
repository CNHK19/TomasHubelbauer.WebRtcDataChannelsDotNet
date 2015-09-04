using Rhaeo.WebRtc.Ice.Declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Connectivity;

namespace Rhaeo.WebRtc.Ice.Implementations
{
  // TODO: Implement foundation and priority computation and assign ports in coordination with the (soon to exist) UDP endpoint component.
  public sealed class Ice
    : IIce
  {
    #region Constructors

    public Ice(IEnumerable<Uri> iceServers)
    {
      IceServers = iceServers;
    }

    #endregion

    #region Properties

    public IEnumerable<Uri> IceServers { get; }

    #endregion

    #region Methods

    private LocalHostIceCandidate FindLocalHostUdpIceCandidate()
    {
      var ipAddress = NetworkInformation.GetHostNames()
        .Where(hn => hn.Type == HostNameType.Ipv4 && hn.IPInformation.NetworkAdapter.NetworkAdapterId == NetworkInformation.GetInternetConnectionProfile().NetworkAdapter.NetworkAdapterId)
        .Single()
        .RawName;

      return new LocalHostIceCandidate(0, 1, 0, ipAddress, 51000);
    }

    private async Task<ServerReflexiveIceCandidate> FindServerReflexiveUdpIceCandidate(IStunIceServer stunIceServer)
    {
      var hostNameAndPort = await Stun.Stun.Bind(new HostName("stun.l.google.com"), 19302);
      return new ServerReflexiveIceCandidate(0, 1, 0, hostNameAndPort.Item1, hostNameAndPort.Item2);
    }

    private Task<ServerReflexiveIceCandidate> FindServerReflexiveUdpIceCandidate(ITurnIceServer turnIceServer)
    {
      return Task.FromException<ServerReflexiveIceCandidate>(new NotImplementedException());
    }

    private Task<RelayIceCandidate> FindRelayUdpIceCandidate(ITurnIceServer turnIceServer)
    {
      return Task.FromException<RelayIceCandidate>(new NotImplementedException());
    }

    private Task<PeerReflexiveIceCandidate> FindPeerReflexiveUdpIceCandidate()
    {
      return Task.FromException<PeerReflexiveIceCandidate>(new NotImplementedException());
    }

    public IObservable<IUdpIceCandidate> GetUdpIceCandidates()
    {
      if (!NetworkInterface.GetIsNetworkAvailable())
      {
        throw new InvalidOperationException("Cannot obtain the server reflexive UDP ICE candidate since the network interface has no network available.");
      }

      return Observable.Merge(new IObservable<IUdpIceCandidate>[] { Observable.Return(FindLocalHostUdpIceCandidate()) }
        .Concat(IceServers.SelectMany(iceServer =>
        {
          IStunIceServer stunIceServer;
          if (StunIceServer.TryCreate(iceServer, out stunIceServer))
          {
            return new[] { FindServerReflexiveUdpIceCandidate(stunIceServer).ToObservable() };
          }

          ITurnIceServer turnIceServer;
          if (TurnIceServer.TryCreate(iceServer, out turnIceServer))
          {
            return new IObservable<IUdpIceCandidate>[]
            {
              FindServerReflexiveUdpIceCandidate(turnIceServer).ToObservable(),
              FindRelayUdpIceCandidate(turnIceServer).ToObservable()
            };
          }

          return Enumerable.Empty<IObservable<IUdpIceCandidate>>();
        })));
    }

    #endregion
  }
}
