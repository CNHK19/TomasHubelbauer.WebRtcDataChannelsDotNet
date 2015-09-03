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

    private async Task<LocalHostIceCandidate> FindLocalHostUdpIceCandidate()
    {
      var ipAddress = NetworkInformation.GetHostNames()
        .Where(hn => hn.Type == HostNameType.Ipv4 && hn.IPInformation.NetworkAdapter.NetworkAdapterId == NetworkInformation.GetInternetConnectionProfile().NetworkAdapter.NetworkAdapterId)
        .Single()
        .RawName;

      // TODO: Remove async/await redundant layer once I am sure this synchronous code is the best way to find the local host unicast IP address.
      return await Task.FromResult(new LocalHostIceCandidate(0, 1, 0, ipAddress, 51000));
    }

    private Task<ServerReflexiveIceCandidate> FindServerReflexiveUdpIceCandidate(IStunIceServer stunIceServer)
    {
      if (!NetworkInterface.GetIsNetworkAvailable())
      {
        throw new InvalidOperationException("Cannot obtain the server reflexive UDP ICE candidate since the network interface has no network available.");
      }

      // TODO: This is a mock, implement correctly as per #6.
      return Task.FromResult(new ServerReflexiveIceCandidate(0, 1, 0, "74.125.194.127", 19302));
    }

    private Task<ServerReflexiveIceCandidate> FindServerReflexiveUdpIceCandidate(ITurnIceServer turnIceServer)
    {
      if (!NetworkInterface.GetIsNetworkAvailable())
      {
        throw new InvalidOperationException("Cannot obtain the server reflexive UDP ICE candidate since the network interface has no network available.");
      }

      return Task.FromException<ServerReflexiveIceCandidate>(new NotImplementedException());
    }

    private Task<RelayIceCandidate> FindRelayUdpIceCandidate(ITurnIceServer turnIceServer)
    {
      if (!NetworkInterface.GetIsNetworkAvailable())
      {
        throw new InvalidOperationException("Cannot obtain the relay UDP ICE candidate since the network interface has no network available.");
      }

      return Task.FromException<RelayIceCandidate>(new NotImplementedException());
    }

    private Task<PeerReflexiveIceCandidate> FindPeerReflexiveUdpIceCandidate()
    {
      if (!NetworkInterface.GetIsNetworkAvailable())
      {
        throw new InvalidOperationException("Cannot obtain the peer reflexive UDP ICE candidate since the network interface has no network available.");
      }

      return Task.FromException<PeerReflexiveIceCandidate>(new NotImplementedException());
    }

    public IObservable<IUdpIceCandidate> GetUdpIceCandidates()
    {
      return Observable.Merge(new IObservable<IUdpIceCandidate>[] { FindLocalHostUdpIceCandidate().ToObservable() }
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
