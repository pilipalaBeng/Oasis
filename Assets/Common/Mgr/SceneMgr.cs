using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : ManagerBase
{
   public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadAysnScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
