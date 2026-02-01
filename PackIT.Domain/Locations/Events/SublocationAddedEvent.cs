using PackIT.Domain.Common;

namespace PackIT.Domain.Locations.Events
{
    internal class SublocationAddedEvent : IDomainEvent
    {
        public Location location;
        public Location sublocation;

        public SublocationAddedEvent(Location location, Location sublocation)
        {
            this.location = location;
            this.sublocation = sublocation;
        }
    }
}