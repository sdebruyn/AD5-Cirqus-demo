using System.Collections.Generic;
using System.Linq;
using Oditel.Models;

namespace Oditel.Cirqus.Models.Commands
{
    public class AddRoomCommand : CreateCommand<Room>
    {
        private readonly Bathroom _bathroom;
        private readonly List<Bed> _beds;
        private readonly bool _seperateToilet;
        private readonly bool _tv;

        public AddRoomCommand(bool tv, bool seperateToilet, Bathroom bathroom, params Bed[] beds)
        {
            _tv = tv;
            _seperateToilet = seperateToilet;
            _bathroom = bathroom;
            _beds = new List<Bed>(beds);
        }

        public AddRoomCommand(IRoom room) : this(room.HasTV, room.SeperateToilet, room.Bathroom, room.Beds.ToArray())
        {
        }

        protected override void Update(Room instance)
        {
            instance.UpdateInfo(_tv, _seperateToilet, _bathroom);
            _beds.ForEach(instance.AddBed);
        }
    }
}