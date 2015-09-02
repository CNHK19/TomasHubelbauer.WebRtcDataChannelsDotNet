using Rhaeo.WebRtc.Ice;
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
      var typeMap = new Dictionary<IceCandidateType, string>()
      {
        [IceCandidateType.LocalHost] = "host",
        [IceCandidateType.ServerReflexive] = null,
        [IceCandidateType.Relay] = null,
        [IceCandidateType.PeerReflexive] = null
      };

      var type = typeMap[iceCandidate.Type];
      if (type == null)
      {
        throw new NotImplementedException($"Converting ICE of type {iceCandidate.Type} is not implemented.");
      }

      return $"a=candidate:{iceCandidate.Foundation} 1 udp {iceCandidate.Priority} {iceCandidate.Address} {iceCandidate.Port} typ {type} generation 0";
    }

    #endregion
  }
}
