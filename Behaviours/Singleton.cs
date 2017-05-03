using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // http://answers.unity3d.com/questions/408518/dontdestroyonload-duplicate-object-in-a-singleton.html

    private static T _instance;

    public bool IsPersistant;

    /**
       Returns the _instance of this singleton.
    */
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) +
                                   " is needed in the scene, but there is none.");
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (IsPersistant)
        {
            if (!_instance)
            {
                _instance = this as T;
            }
            else {
                DestroyObject(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        else {
            _instance = this as T;
        }
    }
}
