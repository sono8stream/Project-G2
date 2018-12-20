using System;

public class ByteHelper
{
    public static int IntFromBytes(
        byte[] data, long offset, bool littleEndian = true)
    {
        if (data.Length <= offset + 4) return 0;

        int val = 0;
        for (int i = 0; i < 4; i++)
        {
            val += data[offset + i] << (i * 8);
        }
        return val;
    }

    public static byte[] GetBytesRange(byte[] input, int offset, int length)
    {
        byte[] output = new byte[length];
        Array.Copy(input, offset, output, 0, length);
        return output;
    }
}