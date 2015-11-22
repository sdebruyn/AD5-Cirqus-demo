using System;
using System.Collections.Generic;
using Oditel.Models.RoomContext;

namespace Oditel.Services
{
    public interface IRoomService
    {
        Guid AddRoom(IRoom room);
        IEnumerable<IRoom> GetAllRooms();
        IRoom GetRoomById(Guid roomId);
    }
}