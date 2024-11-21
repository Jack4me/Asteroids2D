using UnityEngine;
using Zenject;

namespace UI.Test {
    public class PlayerDebug : MonoBehaviour {
        [Inject] public MoneyStorage MoneyStorage;
    }
}
