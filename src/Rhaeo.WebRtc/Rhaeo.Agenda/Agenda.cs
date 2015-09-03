using Rhaeo.WebRtc;
using Rhaeo.WebRtc.Ice.Declarations;
using Rhaeo.WebRtc.Ice.Implementations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rhaeo.Agenda
{
  public static class Agenda
  {
    #region Properties

    public static PeerConnection PeerConnection { get; }

    #endregion

    #region Constructors

    static Agenda()
    {
      // https://gist.github.com/yetithefoot/7592580
      var iceServers = new List<IIceServer>()
      {
        new StunIceServer(new Uri("stun:stun01.sipphone.com")),
        new StunIceServer(new Uri("stun:stun.ekiga.net")),
        new StunIceServer(new Uri("stun:stun.fwdnet.net")),
        new StunIceServer(new Uri("stun:stun.ideasip.com")),
        new StunIceServer(new Uri("stun:stun.iptel.org")),
        new StunIceServer(new Uri("stun:stun.rixtelecom.se")),
        new StunIceServer(new Uri("stun:stun.schlund.de")),
        new StunIceServer(new Uri("stun:stun.l.google.com:19302")),
        new StunIceServer(new Uri("stun:stun1.l.google.com:19302")),
        new StunIceServer(new Uri("stun:stun2.l.google.com:19302")),
        new StunIceServer(new Uri("stun:stun3.l.google.com:19302")),
        new StunIceServer(new Uri("stun:stun4.l.google.com:19302")),
        new StunIceServer(new Uri("stun:stunserver.org")),
        new StunIceServer(new Uri("stun:stun.softjoys.com")),
        new StunIceServer(new Uri("stun:stun.voiparound.com")),
        new StunIceServer(new Uri("stun:stun.voipbuster.com")),
        new StunIceServer(new Uri("stun:stun.voipstunt.com")),
        new StunIceServer(new Uri("stun:stun.voxgratia.org")),
        new StunIceServer(new Uri("stun:stun.xten.com")),
        new TurnIceServer(new Uri("turn:numb.viagenie.ca"), "webrtc@live.com", "muazkh"),
        new TurnIceServer(new Uri("turn:192.158.29.39:3478?transport=udp"), "28224511:1379330808", "JZEOEt2V3Qb0y27GRntt2u2PAYA="),
        new TurnIceServer(new Uri("turn:192.158.29.39:3478?transport=tcp"), "28224511:1379330808", "JZEOEt2V3Qb0y27GRntt2u2PAYA=")
      };

      PeerConnection = new PeerConnection(new Uri("stun:stun.l.google.com:19302"));
    }

    #endregion

    #region Methods

    public static async Task Run()
    {
      // Evaluate the lazy to cache it.
      var localDescription = await PeerConnection.LocalDescription;
    }

    #endregion
  }
}
