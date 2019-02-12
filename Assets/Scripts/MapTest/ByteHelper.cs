using System;

public static class ByteHelper
{
    public static int ToInt(this byte[] bytes,
        ref long offset, bool littleEndian = true)
    {
        if (bytes.Length <= offset + 4) return 0;

        int val = 0;
        for (int i = 0; i < 4; i++)
        {
            val += bytes[offset + i] << (i * 8);
        }
        offset += 4;
        return val;
    }

    public static byte[] GetRange(this byte[] input,
        ref long offset, int length)
    {
        byte[] output = new byte[length];
        Array.Copy(input, offset, output, 0, length);
        offset += length;
        return output;
    }
}