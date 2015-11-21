using System;
using d60.Cirqus;
using Oditel.Cirqus.Models.Commands;
using Oditel.Cirqus.Models.Exceptions;
using Oditel.Models;
using Oditel.Services;

namespace Oditel.Cirqus
{
    public class RoomService : IRoomService
    {
        private readonly ICommandProcessor _processor;

        public RoomService(ICommandProcessor processor)
        {
            _processor = processor;
        }

        public Guid AddRoom(IRoom room)
        {
            var command = new AddRoomCommand(room);
            var result = _processor.ProcessCommand(command);
            if (result.EventsWereEmitted)
            {
                return command.CreatedGuid;
            }
            throw new CreationFailedException(command.CreatedGuid, typeof(IRoom));
        }
    }
}