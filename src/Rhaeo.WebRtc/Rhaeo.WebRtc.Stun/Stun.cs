using Rhaeo.WebRtc.Stun.Attributes;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace Rhaeo.WebRtc.Stun
{
  public static class Stun
  {
    #region Methods

    public static async Task<Tuple<string, ushort>> Bind(HostName hostName, ushort port)
    {
      var taskCompletionSource = new TaskCompletionSource<Tuple<string, ushort>>();
      using (var datagramSocket = new DatagramSocket())
      {
        datagramSocket.MessageReceived += async (sender, e) =>
        {
          var buffer = await e.GetDataStream().ReadAsync(null, 100, InputStreamOptions.None).AsTask();
          var stunMessage = StunMessage.Parse(buffer.ToArray());
          var xorMappedAddressStunMessageAttribute = stunMessage.Attributes.OfType<XorMappedAddressStunMessageAttribute>().Single();
          taskCompletionSource.SetResult(Tuple.Create(xorMappedAddressStunMessageAttribute.IpAddress, xorMappedAddressStunMessageAttribute.Port));
        };

        using (var inMemoryRandomAccessStream = new InMemoryRandomAccessStream())
        {
          var stunMessageId = new StunMessageId(CryptographicBuffer.GenerateRandom(12).ToArray());
          var stunMessageType = StunMessageType.BindingRequest;
          var stunMessageAttributes = new StunMessageAttribute[] { };
          var stunMessage = new StunMessage(stunMessageType, stunMessageAttributes, stunMessageId);
          var bytes = stunMessage.ToLittleEndianByteArray();
          var outputStream = await datagramSocket.GetOutputStreamAsync(hostName, $"{port}");
          var written = await outputStream.WriteAsync(bytes.AsBuffer());
        }
      }

      return await taskCompletionSource.Task;
    }

    #endregion
  }
}
