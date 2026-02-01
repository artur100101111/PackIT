using PackIT.Domain.Common;

namespace PackIT.Domain.Locations.Events
{
    internal class SublocationRemovedEvent : IDomainEvent
    {
        public Location location;
        public Location sublocation;

        public SublocationRemovedEvent(Location location, Location sublocation)
        {
            this.location = location;
            this.sublocation = sublocation;
        }
    }
}