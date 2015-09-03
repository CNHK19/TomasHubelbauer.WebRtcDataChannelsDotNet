namespace Rhaeo.WebRtc.Ice.Declarations
{
  /// <summary>
  /// Defines a contract for <see cref="IceCandidate"/> implementations.
  /// </summary>
  public interface IIceCandidate
  {
    #region Properties

    /// <summary>
    /// Gets the ICE candidate foundation.
    /// </summary>
    /// <remarks>ICE candidate foundation syntax is defined in https://tools.ietf.org/html/rfc5245#section-15.1.</remarks>
    int Foundation { get; }

    /// <summary>
    /// Gets the ICE candidate component.
    /// </summary>
    /// <remarks>ICE candidate component is defined in https://tools.ietf.org/html/rfc5245#section-15.1.</remarks>
    int Component { get; }

    /// <summary>
    /// Gets the ICE candidate transport.
    /// </summary>
    /// <remarks>ICE candidate transport is defined in https://tools.ietf.org/html/rfc5245#section-15.1.</remarks>
    IceCandidateTransport Transport { get; }

    int Priority { get; }

    string Address { get; }

    int Port { get; }

    IceCandidateType Type { get; }

    #endregion
  }
}
