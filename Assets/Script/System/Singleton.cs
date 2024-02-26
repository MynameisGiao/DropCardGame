using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class, new()
{
    private static T instance_ = new T();
    public static T instance
    {
        get
        {
            return instance_;
        }
        private set
        {
            //
        }
    }
}
public class BYSingletonMono<T>: MonoBehaviour where T: MonoBehaviour
{
    private static T singleton;
    public static T instance
    {
        private set
        {

        }
        get
        {
            if(BYSingletonMono<T>.singleton == null)
            {
                BYSingletonMono<T>.singleton= (T)FindObjectOfType(typeof(T));
                if (BYSingletonMono<T>.singleton == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "@[" + typeof(T).Name + "]";
                    BYSingletonMono<T>.singleton = obj.AddComponent<T>();
                }
            }
            return BYSingletonMono<T>.singleton;
        }
    }
    private void Reset()
    {
        gameObject.name = typeof(T).Name;
    }
}