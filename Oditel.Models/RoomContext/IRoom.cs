using System;
using System.Collections.Generic;

namespace Oditel.Models.RoomContext
{
    public interface IRoom
    {
        Guid? RoomId { get; }
        IEnumerable<Bed> Beds { get; }
        bool SeperateToilet { get; }
        Bathroom Bathroom { get; }
        bool HasTV { get; }
        DateTimeOffset? CreatedDate { get; set; }
        DateTimeOffset? DeletedDate { get; set; }
        void AddBed(Bed bed);
        void RemoveBed(Bed bed);
        void UpdateInfo(bool tv, bool seperateToilet, Bathroom bathroom);
    }
}