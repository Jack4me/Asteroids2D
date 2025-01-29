using Core.Services.Input;
using UnityEngine;

namespace Core.Intrerfaces.Services.Input {
    public class MobileInputService : InputService {
        public override Vector2 Axis => SimpleInputAxis();
    }
}