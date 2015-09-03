using Rhaeo.WebRtc.Ice.Declarations;
using System;

namespace Rhaeo.WebRtc.Ice.Implementations
{
  public sealed class StunIceServer
    : IceServer, IStunIceServer
  {
    #region Constructors

    public StunIceServer(Uri url)
    {
      if (!string.Equals(url.Scheme, "stun", StringComparison.OrdinalIgnoreCase))
      {
        throw new ArgumentException("The STUN ICE server URL must have STUN protocol.", nameof(url));
      }

      Url = url;
    }

    #endregion

    #region Properties

    public override Uri Url { get; }

    #endregion

    #region Methods

    public static bool TryCreate(Uri url, out IStunIceServer stunIceServer)
    {
      try
      {
        stunIceServer = new StunIceServer(url);
      }
      catch (Exception)
      {
        stunIceServer = null;
        return false;
      }

      return true;
    }

    #endregion
  }
}
