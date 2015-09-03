using Rhaeo.WebRtc.Ice.Declarations;
using System;

namespace Rhaeo.WebRtc.Ice.Implementations
{
  public sealed class TurnIceServer
    : IceServer, ITurnIceServer
  {
    #region Constructors

    public TurnIceServer(Uri url, string userName, string password)
    {
      if (!string.Equals(url.Scheme, "turn", StringComparison.OrdinalIgnoreCase))
      {
        throw new ArgumentException("The TURN ICE server URL must have TURN protocol.", nameof(url));
      }

      Url = url;
      UserName = userName;
      Password = password;
    }

    #endregion

    #region Properties

    public override Uri Url { get; }

    public string UserName { get; }

    public string Password { get; }

    #endregion

    #region Methods

    public static bool TryCreate(Uri url, out ITurnIceServer turnIceServer)
    {
      try
      {
        turnIceServer = new TurnIceServer(url, null, null);
      }
      catch (Exception)
      {
        turnIceServer = null;
        return false;
      }

      return true;
    }

    #endregion
  }
}
