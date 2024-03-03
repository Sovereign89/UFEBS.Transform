using System;

namespace CBR.UfebsStream.Handlers
{
  public class DuplicateMacValueException : Exception
  {
    private byte[] previousMacValue;
    private byte[] currentMacValue;

    public byte[] PreviousMacValue => this.previousMacValue;

    public byte[] CurrentMacValue => this.currentMacValue;

    public DuplicateMacValueException(byte[] previousMacValue, byte[] currentMacValue)
      : base("Обнаружен повторый элемент MACValue")
    {
      this.previousMacValue = previousMacValue;
      this.currentMacValue = currentMacValue;
    }
  }
}
