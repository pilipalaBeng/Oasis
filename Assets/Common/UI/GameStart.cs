using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace UI
{
    public class GameStart : UIBase
    {
        public const string GAME_SCENE_NAME = "GameScene1";
        public Button startBtn;
        private void Awake()
        {
            startBtn.onClick.AddListener(OnStartBtnClick);
        }

        private void OnStartBtnClick()
        {
            GameMgr.SceneMgr.LoadScene(GAME_SCENE_NAME);
        }
    }
}