using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CsvReaderBigData.Services
{
    internal sealed class ActorTaskScheduler : TaskScheduler
    {
        private readonly Queue<Task> _taskQueue = new Queue<Task>();
        private readonly object _syncObject = new object();
        private bool _isActive = false;

        public override int MaximumConcurrencyLevel => 1;

        protected override IEnumerable<Task> GetScheduledTasks() { throw new NotSupportedException(); }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) => false;

        protected override void QueueTask(Task task)
        {
            lock (_syncObject)
            {
                _taskQueue.Enqueue(task);
                if (!_isActive)
                {
                    _isActive = true;
                    ThreadPool.QueueUserWorkItem(
                        _ =>
                        {
                            Task nextTask = null;
                            while ((nextTask = TryGetNextTask()) != null)
                            {
                                TryExecuteTask(nextTask);
                            }
                        });
                }
            }
        }

        private Task TryGetNextTask()
        {
            lock (_syncObject)
            {
                if (_taskQueue.Count > 0)
                {
                    return _taskQueue.Dequeue();
                }
                else
                {
                    _isActive = false;
                    return null;
                }
            }
        }
    }
}