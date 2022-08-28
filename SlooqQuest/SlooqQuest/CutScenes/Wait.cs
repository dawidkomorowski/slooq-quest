using System;

namespace SlooqQuest.CutScenes
{
    internal sealed class Wait
    {
        private TimeSpan _timeSpan;

        public Wait(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
        }

        public bool Update(TimeSpan deltaTime)
        {
            _timeSpan -= deltaTime;
            return _timeSpan < TimeSpan.Zero;
        }
    }
}