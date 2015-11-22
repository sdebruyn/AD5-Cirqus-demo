using System;
using System.Collections.Generic;
using System.Linq;
using d60.Cirqus;
using Oditel.Cirqus.Models.Commands;
using Oditel.Cirqus.Models.Exceptions;
using Oditel.Cirqus.Views;
using Oditel.Models.RoomContext;
using Oditel.Services;

namespace Oditel.Cirqus.Services
{
    public class RoomService : IRoomService
    {
        private readonly ICommandProcessor _processor;
        private readonly IQueryable<RoomView> _rooms;

        public RoomService(ICommandProcessor processor, IQueryable<RoomView> rooms)
        {
            _rooms = rooms;
            _processor = processor;
        }

        private IQueryable<RoomView> Rooms => _rooms.Where(r => r.DeletedDate == null && r.CreatedDate != null);

        public Guid AddRoom(IRoom room)
        {
            var command = new AddRoomCommand(room);
            var result = _processor.ProcessCommand(command);
            if (result.EventsWereEmitted)
            {
                return command.CreatedGuid;
            }
            throw new CreationFailedException(command.CreatedGuid, typeof (IRoom));
        }

        public Guid AddRoom(bool tv, bool seperateToilet, Bathroom bathroom, Dimensions dimensions, params Bed[] beds)
        {
            var command = new AddRoomCommand(tv, seperateToilet, bathroom, dimensions, beds);
            var result = _processor.ProcessCommand(command);
            if (result.EventsWereEmitted)
            {
                return command.CreatedGuid;
            }
            throw new CreationFailedException(command.CreatedGuid, typeof(IRoom));
        }

        public IEnumerable<IRoom> GetAllRooms() => Rooms.Select(r => r.GetRoomFromView()).ToList();

        public IRoom GetRoomById(Guid roomId)
        {
            try
            {
                return Rooms.Single(r => r.RoomId == roomId).GetRoomFromView();
            }
            catch (Exception)
            {
                throw new LoadingFailedException(roomId, typeof (IRoom));
            }
        }
    }
}