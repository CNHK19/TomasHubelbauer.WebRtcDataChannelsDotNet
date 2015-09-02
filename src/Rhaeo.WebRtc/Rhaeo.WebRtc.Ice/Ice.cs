using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Connectivity;

namespace Rhaeo.WebRtc.Ice
{
  // TODO: Implement foundation and priority computation and assign ports in coordination with the (soon to exist) UDP endpoint component.
  public sealed class Ice
    : IIce
  {
    #region Methods

    private async Task<LocalHostIceCandidate> FindLocalHostUdpIceCandidate()
    {
      ////#if WINDOWS_UWP

      ////#elif NETFX_CORE

      ////#endif
      // Passing in an empty string is a convenience way of specifying localhost.
      ////var ipHostEntry = await Task<IPHostEntry>.Factory.FromAsync(Dns.BeginGetHostEntry, Dns.EndGetHostEntry, string.Empty, null);
      ////var address = ipHostEntry
      ////  .AddressList
      ////  .Where(ipAddress => ipAddress.AddressFamily == AddressFamily.InterNetwork)
      ////  .Single()
      ////  .ToString();
      ////return new IceLocalHostCandidate(0, 0, address, 51000);

      var unicastIpAddress = NetworkInformation.GetHostNames()
        .Where(hn => hn.Type == HostNameType.Ipv4 && hn.IPInformation.NetworkAdapter.NetworkAdapterId == NetworkInformation.GetInternetConnectionProfile().NetworkAdapter.NetworkAdapterId)
        .Single()
        .RawName;

      // TODO: Remove async/await redundant layer once I am sure this synchronous code is the best way to find the local host unicast IP address.
      return await Task.FromResult(new LocalHostIceCandidate(0, 1, 0, unicastIpAddress, 51000));
    }

    private Task<ServerReflexiveIceCandidate> FindServerReflexiveUdpIceCandidate()
    {
      if (!NetworkInterface.GetIsNetworkAvailable())
      {
        throw new InvalidOperationException("Cannot obtain the server reflexive UDP ICE candidate since the network interface has no network available.");
      }

      return Task.FromException<ServerReflexiveIceCandidate>(new NotImplementedException());
    }

    private Task<RelayIceCandidate> FindRelayUdpIceCandidate()
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

    public IEnumerable<IIceCandidate> GetUdpIceCandidates()
    {
      // TODO: Yield candidates from all the implementations by implementing IObservable and searching for them in parallel. 
      // Implemeting provider: https://msdn.microsoft.com/en-us/library/ff506345(v=vs.110).aspx
      // Implementing observer: https://msdn.microsoft.com/en-us/library/ff506346(v=vs.110).aspx
      // Await with IObservable http://stackoverflow.com/a/10290943 & http://log.paulbetts.org/rx-and-await-some-notes/.
      yield return FindLocalHostUdpIceCandidate().Result;
    }

#endregion
  }
}
