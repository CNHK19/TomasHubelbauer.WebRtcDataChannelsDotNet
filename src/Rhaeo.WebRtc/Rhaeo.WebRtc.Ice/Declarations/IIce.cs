using System;

namespace Rhaeo.WebRtc.Ice.Declarations
{
  public interface IIce
  {
    #region Methods

    IObservable<IUdpIceCandidate> GetUdpIceCandidates();

    #endregion
  }
}
