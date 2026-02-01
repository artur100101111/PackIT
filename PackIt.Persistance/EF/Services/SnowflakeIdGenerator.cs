using IdGen;
using PackIt.Application.Services;

namespace PackIt.Persistance.EF.Services
{
    public class SnowflakeIdGenerator : ISnowflakeIdGenerator
    {
        private IdGenerator _longs;

        public SnowflakeIdGenerator(IdGenerator longs)
        {
            _longs = longs;
        }
        public long CreateId()
        {
            return _longs.CreateId();
        }
    }
}
