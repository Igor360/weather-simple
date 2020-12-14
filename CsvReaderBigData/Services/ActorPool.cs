using System.Linq;
using System.Collections.Generic;
using System;

namespace CsvReaderBigData.Services
{
    public class ActorPool
    {
        private List<Actor> _actors;
        private Random _rand;

        public ActorPool(int actors = 1)
        {
            _rand = new Random();
            _actors = new List<Actor>();
            for (int i = 0; i < actors; i++)
            {
                _actors.Add(new Actor());
            }
        }

        public Actor GetActor()
        {
            var index = _rand.Next(0, _actors.Count);
            return _actors[index];
        }
    }
}