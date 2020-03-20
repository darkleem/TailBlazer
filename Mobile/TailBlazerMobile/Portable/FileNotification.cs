using System;
using System.IO;

namespace TailBlazerMobile.Portable
{
    [Flags]
    public enum FileNotificationType
    {
        None,
        CreatedOrOpened,
        Changed,
        Missing,
        Error
    }

    public class FileNotification : IEquatable<FileNotification>
    {
        private FileInfo _info;
        public long Size { get; }
        public bool Exists { get; }
        public Exception Error { get; }
        public string FullName => _info.FullName;
        public FileNotificationType NotificationType { get; }

        public FileNotification(FileInfo file)
        {
            _info = file;
            Exists = file.Exists;

            if (Exists)
            {
                NotificationType = FileNotificationType.CreatedOrOpened;
                Size = file.Length;
            }
            else
            {
                NotificationType = FileNotificationType.Missing;
            }
        }

        public FileNotification(FileInfo info, Exception ex) : this(info)
        {
            _info = info;
            Error = ex;
            Exists = false;
            NotificationType = FileNotificationType.Error;
        }

        public FileNotification(FileNotification prev)
        {
            prev._info.Refresh();
            _info = prev._info;
            Exists = _info.Exists;

            if (Exists)
            {
                Size = _info.Length;

                if (!prev.Exists)
                {
                    NotificationType = FileNotificationType.CreatedOrOpened;
                }
                else if (Size > prev.Size) // Tail에 추가된 경우
                {
                    NotificationType = FileNotificationType.Changed;
                }
                else if (Size < prev.Size)
                {
                    throw new Exception("tail만 지원됩니다. 크기가 줄어들었습니다.");
                }

            }
            else
            {
                NotificationType = FileNotificationType.Missing;
            }
        }

        #region Equality

        public static explicit operator FileInfo(FileNotification source)
        {
            return source._info;
        }

        public bool Equals(FileNotification other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(FullName, other.FullName)
                   && Exists == other.Exists
                   && Size == other.Size
                   && NotificationType == other.NotificationType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FileNotification)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FullName?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ Exists.GetHashCode();
                hashCode = (hashCode * 397) ^ Size.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)NotificationType;
                return hashCode;
            }
        }

        public static bool operator ==(FileNotification left, FileNotification right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FileNotification left, FileNotification right)
        {
            return !Equals(left, right);
        }

        #endregion

        public override string ToString()
        {
            return $"{_info?.Name}  Size: {Size}, Type: {NotificationType}";
        }
    }
}