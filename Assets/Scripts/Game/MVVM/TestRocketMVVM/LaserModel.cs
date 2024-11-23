using System;

namespace UI.Test.TestRocketMVVM {
    public  class LaserModel
    {
        private int _rocketCount;

        public int RocketCount => _rocketCount;

        public LaserModel(int initialCount)
        {
            _rocketCount = initialCount;
        }

        public void AddRocket(int count)
        {
            _rocketCount += count;
        }

        public void UseRocket()
        {
            if (_rocketCount > 0)
            {
                _rocketCount--;
            }
            else
            {
                throw new InvalidOperationException("No rockets left!");
            }
        }
    }
}