using Rhaeo.Agenda.Views;
using Rhaeo.WebRtc.Ice.Declarations;
using Rhaeo.WebRtc.Ice.Implementations;
using Rhaeo.WebRtc.Qr.Extensions;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Rhaeo.WebRtc.App
{
  sealed partial class App
    : Application
  {
    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="e">Details about the launch request and process.</param>
    protected override async void OnLaunched(LaunchActivatedEventArgs e)
    {
      var rootFrame = Window.Current.Content as Frame;
      if (rootFrame == null)
      {
        rootFrame = new Frame();
        Window.Current.Content = rootFrame;
      }

      if (rootFrame.Content == null)
      {
        try
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

          var peerConnection = new PeerConnection(new Uri("stun:stun.l.google.com:19302"));
          var localDescription = await peerConnection.LocalDescription;

          var dataUri = await localDescription.ToQrDataUri();
          rootFrame.Content = new OfferView() { DataContext = new { ImageSource = localDescription.ToQrImageSource(), AgendaWebUrl = new Uri($"http://localhost:60289/?capturedQrDataUri={dataUri}") } };
        }
        catch (Exception exception)
        {
          rootFrame.Content = exception.ToString();
        }
      }

      Window.Current.Activate();
    }
  }
}
