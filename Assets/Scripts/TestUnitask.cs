using Cysharp.Threading.Tasks;

using UnityEngine;

public class TestUnitask : MonoBehaviour
{
    async void Start()
    {
        Debug.Log("UniTask Test Started");

        // Используем UniTask для асинхронной задержки
        await UniTask.Delay(1000);

        Debug.Log("1 second passed with UniTask!");
    }
}