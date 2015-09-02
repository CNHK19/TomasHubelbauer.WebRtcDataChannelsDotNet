using System.Collections.Generic;

namespace Rhaeo.WebRtc.Ice
{
  public interface IIce
  {
    #region Methods

    // TODO: Rewrite to observable pattern.
    IEnumerable<IIceCandidate> GetUdpIceCandidates();

    #endregion
  }
}
