using System;
using System.Collections.Generic;
using d60.Cirqus.Extensions;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using Newtonsoft.Json;
using Oditel.Cirqus.Models;
using Oditel.Cirqus.Models.Events;
using Oditel.Models.RoomContext;

namespace Oditel.Cirqus.Views
{
    public class RoomView : IViewInstance<RoomView.Locator>,
        ISubscribeTo<AggregateRootCreatedEvent<Room>>,
        ISubscribeTo<AggregateRootDeletedEvent<Room>>,
        ISubscribeTo<RoomInfoUpdatedEvent>,
        ISubscribeTo<RoomBedAddedEvent>,
        ISubscribeTo<RoomBedRemovedEvent>
    {
        public Guid RoomId => Guid.Parse(Id);
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public string SerializedBathroom { get; set; }
        public bool SeperateToilet { get; set; }
        public bool HasTV { get; set; }
        public string SerializedBeds { get; set; }
        public Dimensions Dimensions { get; set; }

        public void Handle(IViewContext context, AggregateRootCreatedEvent<Room> domainEvent)
        {
            CreatedDate = domainEvent.CreatedDate;
        }

        public void Handle(IViewContext context, AggregateRootDeletedEvent<Room> domainEvent)
        {
            DeletedDate = domainEvent.DeletedDate;
        }

        public void Handle(IViewContext context, RoomBedAddedEvent domainEvent)
        {
            var list = GetBeds();
            list.Add(domainEvent.Bed);
            SetBeds(list);
        }

        public void Handle(IViewContext context, RoomBedRemovedEvent domainEvent)
        {
            var list = GetBeds();
            list.Remove(domainEvent.Bed);
            SetBeds(list);
        }

        public void Handle(IViewContext context, RoomInfoUpdatedEvent domainEvent)
        {
            HasTV = domainEvent.Tv;
            SeperateToilet = domainEvent.SeperateToilet;
            SerializedBathroom = JsonConvert.SerializeObject(domainEvent.Bathroom);
            Dimensions = domainEvent.Dimensions;
        }

        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }

        private void SetBeds(IEnumerable<Bed> beds)
        {
            SerializedBeds = JsonConvert.SerializeObject(beds);
        }

        private List<Bed> GetBeds()
            =>
                string.IsNullOrWhiteSpace(SerializedBeds)
                    ? new List<Bed>()
                    : JsonConvert.DeserializeObject<List<Bed>>(SerializedBeds);

        public IRoom GetRoomFromView()
        {
            var room = new Models.Room(Guid.Parse(Id))
            {
                CreatedDate = CreatedDate,
                DeletedDate = DeletedDate
            };

            room.UpdateInfo(HasTV, SeperateToilet,
                string.IsNullOrWhiteSpace(SerializedBathroom)
                    ? null
                    : JsonConvert.DeserializeObject<Bathroom>(SerializedBathroom),
                Dimensions);
            GetBeds().ForEach(room.AddBed);

            return room;
        }

        private class Locator : HandlerViewLocator,
            IGetViewIdsFor<AggregateRootCreatedEvent<Room>>,
            IGetViewIdsFor<AggregateRootDeletedEvent<Room>>,
            IGetViewIdsFor<RoomInfoUpdatedEvent>,
            IGetViewIdsFor<RoomBedAddedEvent>,
            IGetViewIdsFor<RoomBedRemovedEvent>
        {
            public IEnumerable<string> GetViewIds(IViewContext context, AggregateRootCreatedEvent<Room> e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, AggregateRootDeletedEvent<Room> e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, RoomBedAddedEvent e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, RoomBedRemovedEvent e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, RoomInfoUpdatedEvent e)
            {
                yield return e.GetAggregateRootId();
            }
        }
    }
}