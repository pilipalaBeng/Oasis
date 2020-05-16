using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace UI
{
    public class MessageBox : UIBase
    {
        public Button blockerBtn;
        public Text contentTex;
        public Button yesBtn;
        public Button noBtn;
        [SerializeField] RectTransform self;
        private UnityAction onYesDel;
        private UnityAction onNoDel;

        private void Awake()
        {
            blockerBtn.onClick.AddListener(OnHide);
            yesBtn.onClick.AddListener(OnYesBtnClick);
            noBtn.onClick.AddListener(OnNoBtnClick);
            if (isJumpSceneDesSelf)
            {
                DontDestroyOnLoad(self.gameObject);
            }
        }

        private void OnNoBtnClick()
        {
            //执行完回调后顺便关掉组件
            onNoDel?.Invoke();
            OnHide();
            //self.localScale = Vector3.zero;
        }

        private void OnYesBtnClick()
        {
            //执行完回调后顺便关掉组件
            onYesDel?.Invoke();
            OnHide();
            //self.localScale = Vector3.zero;
        }
        public override void OnDisplayer(params object[] objs)
        {
            base.OnDisplayer(objs);
            contentTex.text = objs[0] as string;
            onYesDel = objs[1] as UnityAction;
            onNoDel = objs[2] as UnityAction;
        }
       
    }
}
