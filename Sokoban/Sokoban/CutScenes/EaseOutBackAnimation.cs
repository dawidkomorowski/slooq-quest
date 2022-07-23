using System;

namespace Sokoban.CutScenes
{
    internal sealed class EaseOutBackAnimation
    {
        private readonly TimeSpan _duration;
        private readonly bool _reverse;
        private TimeSpan _timer = TimeSpan.Zero;

        public EaseOutBackAnimation(TimeSpan duration, bool reverse = false)
        {
            _duration = duration;
            _reverse = reverse;
        }

        public double Value { get; private set; }

        public bool Update(TimeSpan deltaTime)
        {
            _timer += deltaTime;

            if (_timer > _duration)
            {
                _timer = _duration;
            }

            Value = _reverse ? EaseOutBack(1 - _timer / _duration) : EaseOutBack(_timer / _duration);

            return _timer == _duration;
        }

        private static double EaseOutBack(double x)
        {
            const double c1 = 1.70158;
            const double c3 = c1 + 1;

            return 1 + c3 * Math.Pow(x - 1, 3) + c1 * Math.Pow(x - 1, 2);
        }
    }
}