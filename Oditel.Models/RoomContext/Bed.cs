using System;

namespace Oditel.Models.RoomContext
{
    public sealed class Bed : IEquatable<Bed>, IEquatable<object>
    {
        public enum Size
        {
            King,
            Queen,
            Full,
            Single
        }

        public Bed(Size bedSize)
        {
            BedSize = bedSize;
        }

        public Bed()
        {
        }

        public Size BedSize { get; set; }

        public bool Equals(Bed other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return BedSize == other.BedSize;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Bed && Equals((Bed) obj);
        }

        public override int GetHashCode() => (int) BedSize;
    }
}