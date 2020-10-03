using UnityEngine;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static class Persister
    {
        const string PERSISTENT_OBJECT_ROOT = "Singletons";

        private static GameObject CreateRoot(string rootName)
        {
            GameObject go = new GameObject(rootName);
            Object.DontDestroyOnLoad(go);
            return go;
        }

        private static GameObject FindRoot(string rootName)
        {
            return GameObject.Find(rootName);
        }

        public static void MakePersistent(GameObject o, System.Type singletonType)
        {
            string rootName = PERSISTENT_OBJECT_ROOT;
            SingletonTagAttribute[] attributes = (SingletonTagAttribute[])singletonType.GetCustomAttributes(typeof(SingletonTagAttribute), false);
            if(attributes.Length > 0)
            {             
                if(!string.IsNullOrEmpty(attributes[0].TagRoot))
                {
                    rootName = attributes[0].TagRoot;
                }
            }
            GameObject perst_root = FindRoot(rootName);
            if (perst_root == null)
            {
                perst_root = CreateRoot(rootName);
            }

            if (o.transform.root != perst_root)
            {
                o.transform.SetParent(perst_root.transform);
            }
        }
    }

    private static T _instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).ToString();

                        Persister.MakePersistent(singleton, typeof(T));
                        //DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so '" + singleton +
                            "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " +
                            _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }
    public virtual void Awake()
    {
        DontDestroyOnLoad(this);
        Persister.MakePersistent(gameObject, typeof(T));
    }

    private static bool applicationIsQuitting = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestroy()
    {
        //applicationIsQuitting = true;
    }
}