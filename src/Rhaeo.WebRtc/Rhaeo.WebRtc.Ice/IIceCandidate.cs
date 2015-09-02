namespace Rhaeo.WebRtc.Ice
{
  /// <summary>
  /// Defines a contract for <see cref="IceCandidate"/> implementations.
  /// </summary>
  public interface IIceCandidate
  {
    #region Properties

    int Foundation { get; }

    int Priority { get; }

    string Address { get; }

    int Port { get; }

    IceCandidateType Type { get; }

    #endregion
  }
}
