using System.Collections;
using System.Collections.Generic;
using RAC;
using UnityEngine;

public abstract class ManagerBase : MonoBehaviour
{
    public virtual void Init()
    {

    }
}

public class GameMgr : ManagerBase
{
    private static GameMgr instance;

    private static GameMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Transform.FindObjectOfType<GameMgr>();
                if (instance == null)
                {
                    GameObject go = new GameObject("GameMgr");
                    instance = go.AddComponent<GameMgr>();
                    go.SetActive(true);
                }
            }
            return instance;
        }
    }

    public static UIMgr UiMgr
    {
        get
        {
            if (Instance.uiMgr == null)
            {
                GameObject obj = new GameObject("UIMgr");
                obj.transform.SetParent(Instance.transform);
                Instance.uiMgr = obj.AddComponent<UIMgr>();
                Instance.uiMgr.Init();
            }
            return Instance.uiMgr;
        }
    }

    public static SceneMgr SceneMgr
    {
        get
        {
            if (Instance.sceneMgr == null)
            {
                GameObject obj = new GameObject("SceneMgr");
                obj.transform.SetParent(Instance.transform);
                Instance.sceneMgr = obj.AddComponent<SceneMgr>();
                Instance.sceneMgr.Init();
            }
            return Instance.sceneMgr;
        }
    }

    public static LoadMgr LoadMgr
    {
        get
        {
            if (Instance.loadMgr == null)
            {
                GameObject obj = new GameObject("LoadMgr");
                obj.transform.SetParent(Instance.transform);
                Instance.loadMgr = obj.AddComponent<LoadMgr>();
                Instance.loadMgr.Init();
            }
            return Instance.loadMgr;
        }
    }

    public static AudioMgr AudioMgr
    {
        get
        {
            if (Instance.audioMgr == null)
            {
                GameObject obj = new GameObject("AudioMgr");
                obj.transform.SetParent(Instance.transform);
                Instance.audioMgr = obj.AddComponent<AudioMgr>();
                Instance.audioMgr.Init();
            }
            return Instance.audioMgr;
        }
    }

    public static RacGameMgr RackGameMgr
    {
        get
        {
            if (Instance.rackGameMgr == null)
            {
                Instance.rackGameMgr = Transform.FindObjectOfType<RacGameMgr>();
            }
            return Instance.rackGameMgr;
        }
    }

    private UIMgr uiMgr;
    private SceneMgr sceneMgr;
    private LoadMgr loadMgr;
    private AudioMgr audioMgr;
    private RAC.RacGameMgr rackGameMgr;
    private void Awake()
    {
        DontDestroyOnLoad(Instance.gameObject);
    }
}
