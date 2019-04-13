namespace Jing
{
    public class Time2FloatValueTween
    {
        private float _time;
        private float _startValue;
        private float _endValue;

        /// <summary>
        /// 已通过时间
        /// </summary>
        private float _pastTime = 0f;
        public float nowValue;

        public Time2FloatValueTween(float time, float startValue, float endValue)
        {
            _time = time;
            _startValue = startValue;
            _endValue = endValue;
            _pastTime = 0f;
            nowValue = startValue;
        }

        public bool Step(float dt)
        {
            _pastTime += dt;
            float k = _pastTime / _time;
            if (k > 1)
            {
                k = 1;
            }
            nowValue = _startValue + ((_endValue - _startValue) * k);

            if (k == 1)
            {
                return true;
            }
            return false;
        }
    }

    public class Time2LongValueTween
    {
        private float _time;
        private long _startValue;
        private long _endValue;

        /// <summary>
        /// 已通过时间
        /// </summary>
        private float _pastTime = 0f;
        public long nowValue;

        public Time2LongValueTween(float time, long startValue, long endValue)
        {
            _time = time;
            _startValue = startValue;
            _endValue = endValue;
            _pastTime = 0f;
            nowValue = startValue;
        }

        public bool Step(float dt)
        {
            _pastTime += dt;
            float k = _pastTime / _time;
            if (k > 1)
            {
                k = 1;
            }
            double change = _endValue - _startValue;
            change = change * k;
            nowValue = _startValue + (long)change;
            //Debug.Log(nowValue);

            if (k == 1)
            {
                return true;
            }
            return false;
        }
    }

}
