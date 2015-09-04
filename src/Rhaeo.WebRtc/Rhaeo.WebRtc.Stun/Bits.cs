using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhaeo.WebRtc.Stun
{
  public sealed class Bits
  {
    #region Fields

    private readonly List<bool> list;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Bits"/> class.
    /// </summary>
    /// <param name="length">Length in bytes.</param>
    public Bits(int length)
    {
      list = new List<bool>(length * 8);
    }

    public Bits(bool[] bits)
    {
      list = new List<bool>(bits);
    }

    public Bits(byte[] bytes)
    {
      list = new List<bool>(bytes.Length * 8);
      for (var index = 0; index < bytes.Length * 8; index++)
      {
        var byteIndex = index / 8;
        var bitIndex = index % 8;
        var mask = (byte)(1 << bitIndex);
        list.Add((bytes[byteIndex] & mask) != 0);
      }
    }

    #endregion

    #region Properties

    public int Count => list.Count;

    #endregion

    #region Methods

    public void AddOnBit()
    {
      list.Add(true);
    }

    public void AddOffBit()
    {
      list.Add(false);
    }

    public void AddBit(bool bit)
    {
      list.Add(bit);
    }

    public void AddBits(params bool[] bits)
    {
      list.AddRange(bits);
    }

    public void AddBits(Bits bits)
    {
      list.AddRange(bits.list);
    }

    public void AddByteLittleEndian(byte value)
    {
      for (var index = 0; index < 8; index++)
      {
        var bit = (value & (1 << (index - 1))) != 0;
        list.Add(bit);
      }
    }

    public void AddBytesLittleEndian(params byte[] bytes)
    {
      foreach (var @byte in bytes)
      {
        AddByteLittleEndian(@byte);
      }
    }

    public void AddUInt16LittleEndian(ushort value)
    {
      var bytes = BitConverter.GetBytes(value);
      if (!BitConverter.IsLittleEndian)
      {
        bytes = bytes.Reverse().ToArray();
      }

      AddBytesLittleEndian(bytes);
    }

    public void AddUInt32LittleEndian(uint value)
    {
      var bytes = BitConverter.GetBytes(value);
      if (!BitConverter.IsLittleEndian)
      {
        bytes = bytes.Reverse().ToArray();
      }

      AddBytesLittleEndian(bytes);
    }

    public bool Pop()
    {
      var bit = list[0];
      list.RemoveAt(0);
      return bit;
    }

    public Bits PopBits(int count)
    {
      var bits = new bool[count];
      list.CopyTo(0, bits, 0, count);
      list.RemoveRange(0, count);
      return new Bits(bits);
    }

    public byte[] PopLittleEndianBytes(int count)
    {
      var byteArray = new byte[count / 8];
      for (int index = 0; index < count * 8; index++)
      {
        var byteIndex = index / 8;
        var bitIndex = index % 8;
        byte mask = (byte)(1 << bitIndex);
        byteArray[byteIndex] = (byte)(list[index] ? (byteArray[byteIndex] | mask) : (byteArray[byteIndex] & ~mask));
      }

      list.RemoveRange(0, count * 8);
      return byteArray;
    }

    public bool[] ToBitArray()
    {
      return list.ToArray();
    }

    public byte[] ToLittleEndianByteArray()
    {
      var byteArray = new byte[list.Count / 8];
      for (int index = 0; index < list.Count; index++)
      {
        var byteIndex = index / 8;
        var bitIndex = index % 8;
        byte mask = (byte)(1 << bitIndex);
        byteArray[byteIndex] = (byte)(list[index] ? (byteArray[byteIndex] | mask) : (byteArray[byteIndex] & ~mask));
      }

      return byteArray;
    }

    public override string ToString() => $"{string.Join(null, list.Select((b, i) => (i % 2 == 0 ? " " : "") + (b ? "1" : "0")))}";

    #endregion
  }
}
