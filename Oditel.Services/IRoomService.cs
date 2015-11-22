using System;
using System.Collections.Generic;
using Oditel.Models.RoomContext;

namespace Oditel.Services
{
    public interface IRoomService
    {
        Guid AddRoom(IRoom room);
        Guid AddRoom(bool tv, bool seperateToilet, Bathroom bathroom, Dimensions dimensions, params Bed[] beds);
        ICollection<IRoom> GetAllRooms();
        IRoom GetRoomById(Guid roomId);
    }
}