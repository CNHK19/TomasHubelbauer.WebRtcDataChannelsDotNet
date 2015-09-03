namespace Rhaeo.WebRtc.Ice.Declarations
{
  public interface ITurnIceServer
    : IIceServer
  {
    #region Properties

    string UserName { get; }

    string Password { get; }

    #endregion
  }
}
