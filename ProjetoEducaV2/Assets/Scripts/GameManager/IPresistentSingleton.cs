using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPresistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _uniqueInstance = null;
    private static bool _isApplicationQuiting = false;
    public static T Instance
    {
        get
        {
            if (_uniqueInstance == null && !_isApplicationQuiting)
            {
                _uniqueInstance = FindObjectOfType<T>();
                if (_uniqueInstance == null)
                {
                    GameObject GFToolsPrefab = Resources.Load<GameObject>(typeof(T).Name);
                    if (GFToolsPrefab)
                    {
                        GameObject GFToolsObject = Instantiate<GameObject>(GFToolsPrefab);
                        if (GFToolsObject != null)
                            _uniqueInstance = GFToolsObject.GetComponent<T>();
                    }
                    if (_uniqueInstance == null)
                        _uniqueInstance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
            }
            return _uniqueInstance;
        }
        private set
        {
            if (null == _uniqueInstance)
            {
                _uniqueInstance = value;
                DontDestroyOnLoad(_uniqueInstance.gameObject);
            }
            else if (_uniqueInstance != value)
            {
#if UNITY_EDITOR && !UNITY_WEBGL
                Debug.LogError("[" + typeof(T).Name + "] Tentou instanciar uma segunda instancia da classe IPresistentSingleton.");
#endif
                DestroyImmediate(value.gameObject);

            }
        }
    }

    public static bool IsInitialized()
    {
        return _uniqueInstance != null;
    }

    // Awake is called when the script instance is being loaded
    protected virtual void Awake() => Instance = this as T;

    // This function is called when the MonoBehaviour will be destroyed
    protected virtual void OnDestroy()
    {
        if (_uniqueInstance == this)
            _uniqueInstance = null;
    }

    private void OnApplicationQuit()
    {
        _isApplicationQuiting = true;
    }
}