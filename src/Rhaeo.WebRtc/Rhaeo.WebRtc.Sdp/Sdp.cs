using Rhaeo.WebRtc.Ice.Declarations;
using Rhaeo.WebRtc.Ice.Implementations;
using Rhaeo.WebRtc.Sdp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Windows.Security.Cryptography;

namespace Rhaeo.WebRtc.Sdp
{
  public sealed class Sdp
    : ISdp
  {
    #region Constructors

    public Sdp(IList<IUdpIceCandidate> udpIceCandidates)
    {
      UdpIceCandidates = udpIceCandidates;

      // Generate a cryptographically secure random user name with minimum of 24 bits of randomness which is 4 characters given the 6 bits of randomness per ICE char (alpha, digit, + and / alphabet).
      UserName = new RandomIceCharString(4);

      // Generate a cryptographically secure random password with minimum of 128 bits of randomness which is 22 characters given the 6 bits of randomness per ICE char (alpha, digit, + and / alphabet).
      Password = new RandomIceCharString(22);

      var fingerprint = new byte[] { };
      CryptographicBuffer.CopyToByteArray(CryptographicBuffer.GenerateRandom(96), out fingerprint);

      // TODO: Make a class for this that holds the algorithm enum and bytes of the hash. sha-256 18:B6:57:DA:92:EE:45:81:65:EB…
      Fingerprint = new byte[96];

      Setup = SdpSetup.Passive;
    }

    #endregion

    #region Properties

    public IList<IUdpIceCandidate> UdpIceCandidates { get; }

    public IIceCharString UserName { get; }

    public IIceCharString Password { get; }

    public byte[] Fingerprint { get; }

    public SdpSetup Setup { get; }

    #endregion

    #region Methods

    public override string ToString()
    {
      // https://tools.ietf.org/html/rfc4145#section-4
      var setupMap = new Dictionary<SdpSetup, string>()
      {
        [SdpSetup.Active] = null, // active
        [SdpSetup.Passive] = "passive",
        [SdpSetup.ActivePassive] = null, // actpass
        [SdpSetup.HoldConnection] = null, // holdconn
      };

      var setup = setupMap[Setup];
      if (setup == null)
      {
        throw new NotImplementedException("Active, active-passive and hold-connection session setup is not supported.");
      }

      return $@"
v=0
o=- 33718892146891840 2 IN IP4 127.0.0.1
s=-
t=0 0
a=msid-semantic: WMS
m=application 54267 DTLS/SCTP 5000
c=IN IP4 214.25.96.199
{string.Join(Environment.NewLine, UdpIceCandidates.Select(udpIceCandidate => udpIceCandidate.ToSdpACandidateLineString()))}
a=ice-ufrag:{UserName}
a=ice-pwd:{Password}
a=fingerprint:{Fingerprint}
a=setup:{setup}
a=mid:data
a=sctpmap:5000 webrtc-datachannel 1024
".Trim();
    }

    #endregion
  }
}
