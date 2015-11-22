using System;
using System.Collections.Generic;
using Oditel.Models.RoomContext;

namespace Oditel.Cirqus.Views.Models
{
    public class Room : IRoom
    {
        private readonly List<Bed> _beds;

        public Room(Guid id)
        {
            RoomId = id;
        }

        public Guid? RoomId { get; }

        public IEnumerable<Bed> Beds => _beds;

        public bool SeperateToilet { get; private set; }
        public Bathroom Bathroom { get; private set; }
        public bool HasTV { get; private set; }
        public Dimensions Dimensions { get; private set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }

        public void AddBed(Bed bed)
        {
            _beds.Add(bed);
        }

        public void RemoveBed(Bed bed)
        {
            _beds.Remove(bed);
        }

        public void UpdateInfo(bool tv, bool seperateToilet, Bathroom bathroom, Dimensions dimensions)
        {
            HasTV = tv;
            SeperateToilet = seperateToilet;
            Bathroom = bathroom;
            Dimensions = dimensions;
        }
    }
}