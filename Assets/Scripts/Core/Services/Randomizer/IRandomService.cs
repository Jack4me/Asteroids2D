﻿using CodeBase.Infrastructure.Services;

namespace Core.Services.Randomizer {
    public interface IRandomService : IService {
        int Next(int minValue, int maxValue);
    }
}