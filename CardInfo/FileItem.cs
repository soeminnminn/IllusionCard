using System;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CardInfo
{
    public class FileItem : INotifyPropertyChanged
    {
        #region Win32 declarations
        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_LARGEICON = 0x0;
        private const uint SHGFI_SMALLICON = 0x1;

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyIcon(IntPtr hIcon);
        #endregion

        #region Variables
        public event PropertyChangedEventHandler PropertyChanged;

        private Image mIconSmall;

        private Image mIconLarge;
        #endregion

        #region Properties
        public DirectoryInfo Directory { get; }

        public FileInfo File { get; }

        public string Name
        {
            get
            {
                if (File != null) return File.Name;
                else if (Directory != null) return Directory.Name;
                return "";
            }
        }

        public Image Icon { get => mIconSmall; }

        public string Size
        {
            get => File == null ? "" : FormatFileSize(File.Length);
        }

        public string Type
        {
            get
            {
                if (File != null)
                {
                    return File.Extension;
                }
                else if (Directory != null)
                {
                    return "File folder";
                }
                return "";
            }
        }

        public string Date
        {
            get => File == null ? FormatFileTime(Directory.LastAccessTime) : FormatFileTime(File.LastAccessTime);
        }
        
        public CardTypes CardType { get; set; }

        public Image Image
        {
            get
            {
                if (File != null)
                {
                    return Image.FromFile(File.FullName);
                }
                return null;
            }
        }
        #endregion

        #region Constructor
        public FileItem()
        { }

        public FileItem(DirectoryInfo dirInfo)
        {
            this.Directory = dirInfo;
            if (dirInfo.Exists)
            {
                this.ExtractIcon(dirInfo.FullName);
            }
        }

        public FileItem(FileInfo fileInfo)
        {
            this.File = fileInfo;
            if (fileInfo.Exists)
            {
                this.ExtractIcon(fileInfo.FullName);
            }
        }
        #endregion

        #region Methods
        private string FormatFileSize(long size)
        {
            double bytes = size;
            string[] s = new string[] { "bytes", "KB", "MB", "GB", "TB", "PB", "EB" };
            int pos;
            for (pos = 0; bytes >= 1000; pos++) bytes /= 1024;
            double d = Math.Round(bytes * 10);
            return pos > 0 ? $"{Math.Floor(d / 10)}.{d % 10} {s[pos]}" : $"{size} bytes";
        }

        private string FormatFileTime(DateTime date)
        {
            if (date.Date == DateTime.Now.Date)
            {
                return "Today " + date.ToString("h:mm tt");
            }
            return date.ToString("MMM d, yyyy, h:mm tt");
        }

        private void ExtractIcon(string filePath)
        {
            try
            {
                SHFILEINFO shinfo = new SHFILEINFO();

                SHGetFileInfo(filePath, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);
                mIconSmall = Bitmap.FromHicon(shinfo.hIcon);
                DestroyIcon(shinfo.hIcon);

                SHGetFileInfo(filePath, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
                mIconLarge = Bitmap.FromHicon(shinfo.hIcon);
                DestroyIcon(shinfo.hIcon);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(string.Format("File \"{0}\" doesn not exist!", filePath), ex);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Nested Types
        #endregion
    }
}
