using Rhaeo.WebRtc.Ice;
using Rhaeo.WebRtc.Sdp.Extensions;
using System;
using System.Linq;

namespace Rhaeo.WebRtc.Sdp
{
  public sealed class Sdp
    : ISdp
  {
    #region Constructors

    public Sdp(IIce ice)
    {
      Ice = ice;
    }

    #endregion

    #region Properties

    public IIce Ice { get; }

    #endregion

    #region Methods

    public override string ToString() => $@"
v=0
o=- 33718892146891840 2 IN IP4 127.0.0.1
s=-
t=0 0
a=msid-semantic: WMS
m=application 54267 DTLS/SCTP 5000
c=IN IP4 214.25.96.199
{string.Join(Environment.NewLine, Ice.GetUdpIceCandidates().Select(udpIceCandidate => udpIceCandidate.ToSdpACandidateLineString()))}
a=ice-ufrag:CBaeAkCWA6iaPjbU
a=ice-pwd:uh2p6v2YAx71S5BTLySxUVCa
a=fingerprint:sha-256 18:B6:57:DA:92:EE:45:81:65:EB…
a=setup:actpass
a=mid:data
a=sctpmap:5000 webrtc-datachannel 1024
";

    #endregion
  }
}
