using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMgr : ManagerBase
{
    public T ReseourceLoad<T>(string name) where T:Object
    {
        return Resources.Load<T>(name);
    }
}
