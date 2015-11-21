using System;
using System.Collections.Generic;
using d60.Cirqus.Events;
using Oditel.Cirqus.Models.Events;
using Oditel.Models;
using Oditel.Models.RoomContext;

namespace Oditel.Cirqus.Models
{
    public class Room : AggregateRootBase, IRoom,
        IEmit<AggregateRootCreatedEvent<Room>>,
        IEmit<AggregateRootDeletedEvent<Room>>,
        IEmit<RoomInfoUpdatedEvent>,
        IEmit<RoomBedAddedEvent>,
        IEmit<RoomBedRemovedEvent>
    {
        private readonly IList<Bed> _beds;
        private DateTimeOffset? _createdDate;
        private DateTimeOffset? _deletedDate;

        public Room()
        {
            _beds = new List<Bed>();
        }

        public void Apply(AggregateRootCreatedEvent<Room> e)
        {
            _createdDate = e.CreatedDate;
        }

        public void Apply(AggregateRootDeletedEvent<Room> e)
        {
            _deletedDate = e.DeletedDate;
        }

        public void Apply(RoomBedAddedEvent e)
        {
            ThrowIfDeleted();
            _beds.Add(e.Bed);
        }

        public void Apply(RoomBedRemovedEvent e)
        {
            ThrowIfDeleted();
            _beds.Remove(e.Bed);
        }

        public void Apply(RoomInfoUpdatedEvent e)
        {
            ThrowIfDeleted();

            HasTV = e.Tv;
            SeperateToilet = e.SeperateToilet;
            Bathroom = e.Bathroom;
        }

        public override DateTimeOffset? CreatedDate
        {
            get { return _createdDate; }
            set { Emit(new AggregateRootDeletedEvent<Room>(value)); }
        }

        public override DateTimeOffset? DeletedDate
        {
            get { return _deletedDate; }
            set { Emit(new AggregateRootCreatedEvent<Room>(value)); }
        }

        public Guid? RoomId => ConvertIdToGuid();

        public IEnumerable<Bed> Beds => _beds;

        public bool SeperateToilet { get; private set; }
        public Bathroom Bathroom { get; private set; }
        public bool HasTV { get; private set; }

        public void AddBed(Bed bed)
        {
            ThrowIfDeleted();
            Emit(new RoomBedAddedEvent(bed));
        }

        public void RemoveBed(Bed bed)
        {
            ThrowIfDeleted();
            Emit(new RoomBedRemovedEvent(bed));
        }

        public void UpdateInfo(bool tv, bool seperateToilet, Bathroom bathroom)
        {
            ThrowIfDeleted();
            Emit(new RoomInfoUpdatedEvent(tv, seperateToilet, bathroom));
        }
    }
}