using Rhaeo.WebRtc.Ice.Declarations;
using System;

namespace Rhaeo.WebRtc.Ice.Implementations
{
  public abstract class IceServer
    : IIceServer
  {
    #region Properties

    public abstract Uri Url { get; }

    #endregion
  }
}
