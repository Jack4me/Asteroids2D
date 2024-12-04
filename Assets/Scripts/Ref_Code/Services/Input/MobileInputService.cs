using Ref_Code.Services.Input;
using UnityEngine;

namespace CodeBase.Services.Input {
    public class MobileInputService : InputService {
        public override Vector2 Axis => SimpleInputAxis();
    }
}