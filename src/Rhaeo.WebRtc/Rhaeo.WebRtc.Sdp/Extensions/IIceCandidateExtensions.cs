using Rhaeo.WebRtc.Ice;
using Rhaeo.WebRtc.Ice.Declarations;
using System;
using System.Collections.Generic;

namespace Rhaeo.WebRtc.Sdp.Extensions
{
  public static class IIceCandidateExtensions
  {
    #region Methods

    /// <summary>
    /// Produces a string that contains SDP <c>a=candidate</c> line.
    /// </summary>
    /// <param name="iceCandidate">The ICE candidate to convert to the SDP <c>a=candidate</c> line string.</param>
    /// <returns>A string containing the SDP <c>a=candidate</c> line.</returns>
    public static string ToSdpACandidateLineString(this IIceCandidate iceCandidate)
    {
      // Candidate transports as per https://tools.ietf.org/html/rfc5245#section-15.1.
      var transportMap = new Dictionary<IceCandidateTransport, string>()
      {
        [IceCandidateTransport.Udp] = "udp",
        [IceCandidateTransport.Tcp] = null, // TCP is not supported for Data Channel.
        [IceCandidateTransport.Extension] = null // I don't currently support transport extensions.
      };

      var transport = transportMap[iceCandidate.Transport];
      if (transport == null)
      {
        throw new ArgumentException("Non-UDP ICE candidate transports are not supported.", nameof(iceCandidate));
      }

      // Candidate types as per https://tools.ietf.org/html/rfc5245#section-15.1.
      var typeMap = new Dictionary<IceCandidateType, string>()
      {
        [IceCandidateType.LocalHost] = "host",
        [IceCandidateType.ServerReflexive] = "srflx",
        [IceCandidateType.Relay] = null, // relay
        [IceCandidateType.PeerReflexive] = null // prflx
      };

      var type = typeMap[iceCandidate.Type];
      if (type == null)
      {
        throw new NotImplementedException($"Converting ICE of type {iceCandidate.Type} is not implemented.");
      }

      return $"a=candidate:{iceCandidate.Foundation} {iceCandidate.Component} {transport} {iceCandidate.Priority} {iceCandidate.Address} {iceCandidate.Port} typ {type} generation 0";
    }

    #endregion
  }
}
