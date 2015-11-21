using System;
using Oditel.Models;

namespace Oditel.Services
{
    public interface IRoomService
    {
        Guid AddRoom(IRoom room);
    }
}