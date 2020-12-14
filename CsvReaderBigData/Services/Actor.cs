using System;
using System.Threading.Tasks;

namespace CsvReaderBigData.Services
{
    public sealed class Actor
    {
        private readonly TaskFactory _taskFactory = new TaskFactory(new ActorTaskScheduler());

        public Task Enqueue(Action work)
        {
            return _taskFactory.StartNew(work);
        }

        public Task<T> Enqueue<T>(Func<T> work)
        {
            return _taskFactory.StartNew(work);
        }
    }
}