using Snowflake.Core;

namespace Crx.vNext.Common.Helper
{
    public class SnowflakeID
    {
        private static IdWorker _idWorker;

        public SnowflakeID(IdWorker idWorker)
        {
            _idWorker = idWorker;
        }

        public static long NextId() {
            return _idWorker.NextId();
        }
    }
}
