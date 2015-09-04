namespace Rhaeo.WebRtc.Stun.Attributes
{
  public sealed class XorMappedAddressStunMessageAttribute
    : StunMessageAttribute
  {
    #region Constructors

    public XorMappedAddressStunMessageAttribute(string ipAddress, ushort port)
    {
      IpAddress = ipAddress;
      Port = port;
    }

    #endregion

    #region Properties

    public override StunMessageAttributeType Type => StunMessageAttributeType.XorMappedAddress;

    public string IpAddress { get; }

    public ushort Port { get; }

    #endregion
  }
}
