//--------------------------------------------------------------------------------
// Olive Mobile - CopyRight 2014 - 2014.08
//--------------------------------------------------------------------------------

using UnityEngine;

using System.Collections;
using System.Collections.Generic;


public class SingletonControl<T> : MonoBehaviour where T : class, new()
{
    public static T SingleInstance { get ; set ; }

	public virtual void Init ()
	{
	}

    public static T Instance
    {
		get 
		{
            if (SingleInstance == null )
            {
                SingleInstance = GameObject.FindObjectOfType(typeof(T)) as T;

                if ( SingleInstance == null )
                {
                    GameObject Container = new GameObject();

                    SingleInstance = Container.AddComponent(typeof(T)) as T;

                    Container.name = SingleInstance.ToString();
                }
            }

            return SingleInstance;
		}
	}
}
