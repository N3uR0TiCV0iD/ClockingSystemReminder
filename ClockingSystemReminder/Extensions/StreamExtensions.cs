using System;
using System.IO;
using System.Text;

namespace ClockingSystemReminder.Extensions
{
    public static class StreamExtensions
    {
        public static void WriteString(this Stream stream, string text)
        {
            var bytes = Encoding.ASCII.GetBytes(text);
            stream.Write(bytes, 0, bytes.Length);
        }

        public static string ReadToEnd(this Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
