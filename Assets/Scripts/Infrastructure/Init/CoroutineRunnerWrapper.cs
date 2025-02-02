using System.Collections;
using Core;
using UnityEngine;

namespace Infrastructure.Bootstrap
{
    public class CoroutineRunnerWrapper : ICoroutineRunner 
    {
        private readonly MonoBehaviour _monoBehaviour;

        public CoroutineRunnerWrapper(MonoBehaviour monoBehaviour) {
            _monoBehaviour = monoBehaviour;
        }

        public Coroutine StartCoroutine(IEnumerator coroutine) {
            return _monoBehaviour.StartCoroutine(coroutine);
        }

        public void StopCoroutine(Coroutine coroutine) {
            _monoBehaviour.StopCoroutine(coroutine);
        }
    }
}
