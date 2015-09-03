using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Rhaeo.WebRtc.Sdp;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Media;
using ZXing.QrCode;

namespace Rhaeo.WebRtc.Qr.Extensions
{
  public static class ISdpExtensions
  {
    #region Methods

    public async static Task<Uri> ToQrDataUri(this ISdp sdp)
    {
      var height = 500;
      var width = 500;

      var qrCodeWriter = new QRCodeWriter();
      var bitMatrix = qrCodeWriter.encode(sdp.ToString(), ZXing.BarcodeFormat.QR_CODE, width, height);

      using (var canvasRenderTarget = new CanvasRenderTarget(CanvasDevice.GetSharedDevice(), 500, 500, 96))
      {
        using (var drawingSession = canvasRenderTarget.CreateDrawingSession())
        {
          for (var y = 0; y < height; y++)
          {
            for (var x = 0; x < width; x++)
            {
              drawingSession.DrawRectangle(x, y, 1, 1, bitMatrix.get(x, y) ? Color.FromArgb(0, 0, 0, 0) : Color.FromArgb(255, 255, 255, 255));
            }
          }
        }

        using (var inMemoryRandomAccessStream = new InMemoryRandomAccessStream())
        {
          await canvasRenderTarget.SaveAsync(inMemoryRandomAccessStream, CanvasBitmapFileFormat.Png);
          inMemoryRandomAccessStream.Seek(0);
          var buffer = new byte[inMemoryRandomAccessStream.Size];
          await inMemoryRandomAccessStream.ReadAsync(buffer.AsBuffer(), (uint)inMemoryRandomAccessStream.Size, InputStreamOptions.None);
          return new Uri($"data:image/png;base64,{Convert.ToBase64String(buffer)}");
        }
      }

      //return $@"{sdp.UserName}|{sdp.Password}|{Convert.ToBase64String(sdp.Fingerprint)}|{string.Join("|", sdp.UdpIceCandidates.Select(udpIceCandidate => $"{udpIceCandidate.Address}:{udpIceCandidate.Port}"))}".Trim();
    }

    public static ImageSource ToQrImageSource(this ISdp sdp)
    {
      var height = 500;
      var width = 500;

      var qrCodeWriter = new QRCodeWriter();
      var bitMatrix = qrCodeWriter.encode(sdp.ToString(), ZXing.BarcodeFormat.QR_CODE, width, height);

      var canvasImageSource = new CanvasImageSource(CanvasDevice.GetSharedDevice(), width, height, 96);
      using (var drawingSession = canvasImageSource.CreateDrawingSession(Color.FromArgb(255, 255, 255, 255)))
      {
        for (var y = 0; y < height; y++)
        {
          for (var x = 0; x < width; x++)
          {
            if (bitMatrix.get(x, y))
            {
              drawingSession.DrawRectangle(x, y, 1, 1, Color.FromArgb(255, 0, 0, 0));
            }
          }
        }
      }

      return canvasImageSource;
    }

    #endregion
  }
}
