using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

#pragma warning disable SA1134 // Attributes should not share line
namespace ClockingSystemReminder.Helpers
{
    //Useful class to get access to the clipboard outside of the MainThread
    public static class ClipboardHelper
    {
        [DllImport("user32.dll")] private static extern IntPtr SetClipboardData(ClipboardFormat format, IntPtr hMem);
        [DllImport("user32.dll")] private static extern bool IsClipboardFormatAvailable(ClipboardFormat format);
        [DllImport("user32.dll")] private static extern IntPtr GetClipboardData(ClipboardFormat format);
        [DllImport("user32.dll")] private static extern bool OpenClipboard(IntPtr hwndNewOwner);
        [DllImport("kernel32.dll")] private static extern IntPtr GlobalLock(IntPtr hMem);
        [DllImport("kernel32.dll")] private static extern bool GlobalUnlock(IntPtr hMem);
        [DllImport("gdi32.dll")] private static extern bool DeleteObject(IntPtr hObject);
        [DllImport("user32.dll")] private static extern bool EmptyClipboard();
        [DllImport("user32.dll")] private static extern bool CloseClipboard();

        public static string GetText()
        {
            if (!IsClipboardFormatAvailable(ClipboardFormat.CF_UNICODETEXT))
            {
                return null;
            }
            if (!OpenClipboard(IntPtr.Zero))
            {
                return null;
            }

            string result = null;
            var dataHandle = GetClipboardData(ClipboardFormat.CF_UNICODETEXT);
            if (dataHandle != IntPtr.Zero)
            {
                var dataPtr = GlobalLock(dataHandle);
                if (dataPtr != IntPtr.Zero)
                {
                    result = Marshal.PtrToStringUni(dataPtr);
                }
                GlobalUnlock(dataHandle);
            }
            CloseClipboard();
            return result;
        }

        public static bool SetText(string text)
        {
            if (!OpenClipboard(IntPtr.Zero))
            {
                return false;
            }

            var dataPtr = Marshal.StringToHGlobalUni(text);
            var dataHandle = SetClipboardData(ClipboardFormat.CF_UNICODETEXT, dataPtr); //NOTE: If it succeeds, we no longer own "dataPtr"
            if (dataHandle == IntPtr.Zero)
            {
                Marshal.FreeHGlobal(dataPtr);
                CloseClipboard();
                return false;
            }
            CloseClipboard();
            return true;
        }

        public static Bitmap GetImage()
        {
            if (!IsClipboardFormatAvailable(ClipboardFormat.CF_BITMAP))
            {
                return null;
            }
            if (!OpenClipboard(IntPtr.Zero))
            {
                return null;
            }

            Bitmap result = null;
            var bitmapHandle = GetClipboardData(ClipboardFormat.CF_BITMAP);
            if (bitmapHandle != IntPtr.Zero)
            {
                result = Image.FromHbitmap(bitmapHandle);
            }

            CloseClipboard();
            return result;
        }

        public static bool SetImage(Bitmap image)
        {
            if (!OpenClipboard(IntPtr.Zero))
            {
                return false;
            }

            EmptyClipboard();

            var bitmapHandle = image.GetHbitmap();
            var paletteHandle = GetPalleteHandle(image);

            SetClipboardData(ClipboardFormat.CF_BITMAP, bitmapHandle);
            if (paletteHandle != IntPtr.Zero)
            {
                SetClipboardData(ClipboardFormat.CF_PALETTE, paletteHandle);
            }

            CloseClipboard();

            // Free the resources
            DeleteObject(bitmapHandle);
            if (paletteHandle != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(paletteHandle);
            }

            return true;
        }

        private static IntPtr GetPalleteHandle(Bitmap image)
        {
            if (image.Palette.Entries.Length == 0)
            {
                return IntPtr.Zero;
            }

            // Create a copy of the palette to pass to the clipboard.
            var paletteHandle = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ColorPalette)));
            Marshal.StructureToPtr(image.Palette, paletteHandle, false);
            return paletteHandle;
        }
    }

    //https://www.pinvoke.net/default.aspx/ole32.CLIPFORMAT
    public enum ClipboardFormat : uint
    {
        CF_TEXT = 1,
        CF_BITMAP = 2,
        CF_METAFILEPICT = 3,
        CF_SYLK = 4,
        CF_DIF = 5,
        CF_TIFF = 6,
        CF_OEMTEXT = 7,
        CF_DIB = 8,
        CF_PALETTE = 9,
        CF_PENDATA = 10,
        CF_RIFF = 11,
        CF_WAVE = 12,
        CF_UNICODETEXT = 13,
        CF_ENHMETAFILE = 14,
        CF_HDROP = 15,
        CF_LOCALE = 16,
        CF_MAX = 17,
        CF_OWNERDISPLAY = 0x80,
        CF_DSPTEXT = 0x81,
        CF_DSPBITMAP = 0x82,
        CF_DSPMETAFILEPICT = 0x83,
        CF_DSPENHMETAFILE = 0x8E,
    }
}
#pragma warning restore SA1134 // Attributes should not share line
