using System.Linq;
using Windows.Security.Cryptography;

namespace Rhaeo.WebRtc.Ice.Implementations
{
  /// <summary>
  /// A random string that is restricted to the ICE char alphabet consisting of alphanumerical characters, plus sign and forward slash sign.
  /// </summary>
  public sealed class RandomIceCharString
    : IceCharString
  {
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="RandomIceCharString"/> class using the given length.
    /// </summary>
    /// <param name="length">The length of the random ICE-char string.</param>
    public RandomIceCharString(int length)
      : base(GetRandomIceCharString(length))
    {
    }

    #endregion

    #region Methods

    private static string GetRandomIceCharString(int length)
    {
      // Cryptographically secure random numbers on UWP: http://stackoverflow.com/a/14582462
      return new string(Enumerable.Range(0, length).Select(index => chars[CryptographicBuffer.GenerateRandomNumber() % chars.Length]).ToArray());
    }

    #endregion
  }
}
