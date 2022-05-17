using System;
using System.IO;
using System.Linq;

namespace CardInfo
{
    public static class Extensions
    {
        #region Methods
        /*
         * Correct but slow.
         */
        public static long FindSequence(this Stream stream, byte[] byteSequence)
        {
            if (byteSequence.Length > stream.Length)
                return -1;

            int padLeftSequence(byte[] bytes, byte[] seqBytes)
            {
                int i = 1;
                while (i < bytes.Length)
                {
                    int n = bytes.Length - i;
                    byte[] aux1 = new byte[n];
                    byte[] aux2 = new byte[n];
                    Buffer.BlockCopy(bytes, i, aux1, 0, n);
                    Buffer.BlockCopy(seqBytes, 0, aux2, 0, n);
                    if (aux1.SequenceEqual(aux2))
                        return i;
                    i++;
                }
                return i;
            };

            byte[] buffer = new byte[byteSequence.Length];

            BufferedStream bufStream = new BufferedStream(stream, byteSequence.Length);
            while (bufStream.Read(buffer, 0, byteSequence.Length) == byteSequence.Length)
            {
                if (byteSequence.SequenceEqual(buffer))
                    return bufStream.Position - byteSequence.Length;
                else
                    bufStream.Position -= byteSequence.Length - padLeftSequence(buffer, byteSequence);
            }

            return -1;
        }

        public static long IndexOf(this byte[] haystack, byte[] needle, long startOffset = 0)
        {
            unsafe
            {
                fixed (byte* h = haystack) fixed (byte* n = needle)
                {
                    for (byte* hNext = h + startOffset, hEnd = h + haystack.LongLength + 1 - needle.LongLength, nEnd = n + needle.LongLength; hNext < hEnd; hNext++)
                        for (byte* hInc = hNext, nInc = n; *nInc == *hInc; hInc++)
                            if (++nInc == nEnd)
                                return hNext - h;
                    return -1;
                }
            }
        }

        public static long Seek(this BinaryReader reader, long offset, SeekOrigin origin) => reader.BaseStream.Seek(offset, origin);

        public static long Position(this BinaryReader reader) => reader.BaseStream.Position;

        public static long Length(this BinaryReader reader) => reader.BaseStream.Length;

        public static bool IsEOF(this BinaryReader reader) => (reader.BaseStream.Position >= reader.BaseStream.Length);

        public static byte[] ReadToEnd(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        #endregion
    }
}
