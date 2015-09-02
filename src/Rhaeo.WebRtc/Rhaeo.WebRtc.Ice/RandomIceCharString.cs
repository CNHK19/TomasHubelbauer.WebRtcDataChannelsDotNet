using System.Linq;
using Windows.Security.Cryptography;

namespace Rhaeo.WebRtc.Ice
{
  /// <summary>
  /// A random string that is restricted to the ICE char alphabet consisting of alphanumerical characters, plus sign and forward slash sign.
  /// </summary>
  public sealed class RandomIceCharString
  {
    #region Fields

    // https://tools.ietf.org/html/rfc5245#section-15.1 see `ice-char` in the grammar table.
    private readonly char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".ToCharArray();

    private readonly string iceCharString;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="RandomIceCharString"/> using the given length.
    /// </summary>
    /// <param name="length">The length of the random ICE-char string.</param>
    public RandomIceCharString(int length)
    {
      // Cryptographically secure random numbers on UWP: http://stackoverflow.com/a/14582462
      iceCharString = new string(Enumerable.Range(0, length).Select(index => chars[CryptographicBuffer.GenerateRandomNumber() % chars.Length]).ToArray());
    }

    #endregion

    #region Operators

    public static implicit operator string(RandomIceCharString randomIceCharString) => randomIceCharString.iceCharString;

    #endregion

    #region Methods

    public override string ToString() => iceCharString;

    #endregion
  }
}
