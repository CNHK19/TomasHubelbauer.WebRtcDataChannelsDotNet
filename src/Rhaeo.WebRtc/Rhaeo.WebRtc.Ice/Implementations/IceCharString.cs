using Rhaeo.WebRtc.Ice.Declarations;

namespace Rhaeo.WebRtc.Ice.Implementations
{
  public class IceCharString
    : IIceCharString
  {
    #region Fields

    // https://tools.ietf.org/html/rfc5245#section-15.1 see `ice-char` in the grammar table.
    protected static readonly char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".ToCharArray();

    protected readonly string iceCharString;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="IceCharString"/> class.
    /// </summary>
    /// <param name="length">The length of the ICE-char string.</param>
    public IceCharString(string iceCharString)
    {
      this.iceCharString = iceCharString;
    }

    #endregion

    #region Operators

    public static implicit operator string (IceCharString iceCharString) => iceCharString.iceCharString;

    #endregion

    #region Methods

    public override string ToString() => iceCharString;

    #endregion
  }
}
