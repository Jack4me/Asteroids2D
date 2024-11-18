using UnityEngine;

namespace Infrastructure {
    public class JsonConfigLoader<T>
    {
        public static T Load(string path)
        {
            var json = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
    }
}

