using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance = null;

    public static bool IsAwake
    { get { return (_instance != null); } }

    public static T I
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    string goName = typeof(T).ToString();

                    GameObject gameobject = GameObject.Find(goName);
                    if (gameobject == null)
                    {
                        gameobject = new GameObject();
                        gameobject.name = goName;
                    }

                    _instance = gameobject.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    public virtual void OnApplicationQuit()
    {
        _instance = null;
    }

    protected void SetParent(string name)
    {
        if (name != null)
        {
            GameObject parentObj = GameObject.Find(name);
            if (parentObj == null)
            {
                parentObj = new GameObject();
                parentObj.name = name;
            }
            this.transform.parent = parentObj.transform;
        }
    }
}