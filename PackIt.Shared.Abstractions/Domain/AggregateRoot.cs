namespace PackIT.Domain.Common
{
    public abstract class AggregateRoot<T>
    {
        protected AggregateRoot()
        {
    
        }
        public  T Id { get; protected set; }
        private readonly List<IDomainEvent> _events = new() ;

        public IEnumerable<IDomainEvent> Events => _events;

        public int Version { get; protected set; } = 0;


        private bool _versionIncremented = false;

        protected void IncrementVersion()
        {
            if (_versionIncremented)
            {
                return;
            }
            Version ++;
            _versionIncremented = true;
        }
        /// <summary>
        /// Domain Event
        /// </summary>
        /// <param name="event"></param>
        protected void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any() && !_versionIncremented)
            {
                Version ++;
                _versionIncremented = true;
            }
            _events.Add(@event);
        }
        public void ClearEvents() => _events.Clear();

    }
}
