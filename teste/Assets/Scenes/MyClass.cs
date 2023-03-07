using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClass
{
    private static MyClass _uniqueIstance;
    private MyClass()
    {



    }
    public static MyClass GetInstaciate()
    {
        if (_uniqueIstance == null)
        {
            _uniqueIstance = new MyClass();

        }
        return _uniqueIstance;
    }
}
public class IPersitentSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _uniqueInstance;
    private static bool _isAplicationQuiting = false;

    public static T Instance
    {
        get
        {
            if (_uniqueInstance == null && !_isAplicationQuiting )
            {
                _uniqueInstance = FindObjectOfType<T>();
                if (_uniqueInstance == null)
                {
                    GameObject prefab = Resources.Load<GameObject>(typeof(T).Name);
                    if (prefab != null)
                    {
                        GameObject instaceObject = Instantiate<GameObject>(prefab);
                        if (instaceObject != null)
                        {
                            _uniqueInstance = instaceObject.GetComponent<T>();
                        }

                    }
                }
            }
            return _uniqueInstance;
        }
        private set 
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = value;
                DontDestroyOnLoad(_uniqueInstance);
            }
            else
            {
                DestroyImmediate(value.gameObject);
            }
        
        }
    }
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        Instance = this as T;
    }


    protected virtual void OnDestroy()
    {
        
        if (_uniqueInstance == this)
        {
            _uniqueInstance = null;
        }
    }

    private void OnApplicationQuit()
    {
        _isAplicationQuiting = true;
    }
}


public class GarbageTest : MonoBehaviour
{

    private void Awake()
    {
       
    }
    void Start()
    {
        MyClass.GetInstaciate();
    }

    private void Update()
    {

    }

    public void print()
    {
        Debug.Log("My class");

    }
}
