using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster), typeof(CanvasScaler))]
    public abstract class UIBase : MonoBehaviour
    {
        private static int m_maxSortOrder = 0;
        private int mSortOrder = -1;
        // 显隐方式   active scale position layer
        [Header("跳转场景时是否销毁自身？")]
        public bool isJumpSceneDesSelf = false;
        private  enum DisplayTypeEnum
        {
            Active,
            Scale,
            Position,
            Layer,
        }
        [SerializeField] private DisplayTypeEnum displayerType = DisplayTypeEnum.Active;

        private Vector3 displayPos;
        private Vector3 hidePos;
        private Canvas mCanvas;
        private bool isInit = false;
        private void Init()
        {
            if (isInit)
            {
                return;
            }

            isInit = true;
            Vector3 displayPos = new Vector3(10000, 10000, 10000);
            Vector3 hidePos = Vector3.zero;

            m_maxSortOrder += 1;
            mSortOrder = m_maxSortOrder;

            mCanvas = this.GetComponent<Canvas>();
        }

        public virtual void OnDisplayer(params object[] objs)
        {
            switch (displayerType)
            {
                case DisplayTypeEnum.Active:
                    this.gameObject.SetActive(true);
                    break;
                case DisplayTypeEnum.Scale:
                    this.transform.localScale = Vector3.one;
                    break;
                case DisplayTypeEnum.Position:
                    this.transform.localPosition = displayPos;
                    break;
                case DisplayTypeEnum.Layer:
                    if (!mCanvas.overrideSorting)
                    {
                        mCanvas.overrideSorting = true;
                    }
                    mCanvas.sortingOrder = mSortOrder;
                    break;
            }
        }
        public virtual void OnHide()
        {
            switch (displayerType)
            {
                case DisplayTypeEnum.Active:
                    this.gameObject.SetActive(false);
                    break;
                case DisplayTypeEnum.Scale:
                    this.transform.localScale = Vector3.zero;
                    break;
                case DisplayTypeEnum.Position:
                    this.transform.localPosition = hidePos;
                    break;
                case DisplayTypeEnum.Layer:
                    if (!mCanvas.overrideSorting)
                    {
                        mCanvas.overrideSorting = true;
                    }
                    mCanvas.sortingOrder = mSortOrder - 100;
                    break;
            }
        }
    }
}
public class UIMgr : ManagerBase
{
    private Dictionary<string, UIBase> uiPop;

    private Dictionary<string, UIBase> UiPop
    {
        get
        {
            if (uiPop == null)
            {
                uiPop = new Dictionary<string, UIBase>();
            }
            return uiPop;
        }
    }

    public const string MESSAGE_BOX_NAME = "MessageBox";
    public const string CANVAS_NAME = "Canvas";
    public void ShowMessageBox(string contentStr, UnityAction yesCallback = null, UnityAction noCallback = null)
    {
        var mesBox = GetUI(MESSAGE_BOX_NAME);
        CheckParent(mesBox);
        SetAnchorScreenCenter(mesBox);
        mesBox.GetComponent<MessageBox>().OnDisplayer(contentStr, yesCallback, noCallback);
    }
    public void SetAnchorScreenCenter(UIBase ui)
    {
        ui.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        ui.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        ui.GetComponent<RectTransform>().anchorMin = Vector3.zero;
        ui.GetComponent<RectTransform>().anchorMax = Vector3.one;
    }
    private Transform canvas;
    private void CheckParent(UIBase ui)
    {
        if (ui.transform.parent != canvas)
        {
            ui.transform.SetParent(canvas);
            ui.transform.localPosition = Vector3.zero;
            ui.transform.localScale = Vector3.one;
        }
    }
    public UIBase GetUI(string name)
    {
        UiPop.TryGetValue(name, out UIBase ui);
        if (ui == null)
        {
            var memory = GameMgr.LoadMgr.ReseourceLoad<UIBase>(name);
            ui = Instantiate(memory);
            if (ui != null)
            {
                UiPop.Add(name, ui);
            }
            else
            {
                Debug.LogError($"name {name} ui is null!");
            }

        }
        return ui;
    }
    public override void Init()
    {
        base.Init();

        if (canvas == null)
        {
            //因为每个uipanel都是一个canvas，所以查找根canvas不实际，但是可以查eventSystem如果查到的话，它
            //对应的父级就是我们需要的根canvas，当然也可以给这个根canvas设置tag，通过findTag的方式查找
            //但是我就是喜欢这样查 emmmm
            //canvas = Transform.FindObjectOfType<Canvas>().transform;
            var eventSystem = Transform.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
            if (eventSystem == null)
            {
                Transform memory = GameMgr.LoadMgr.ReseourceLoad<Transform>(CANVAS_NAME);
                canvas = Instantiate(memory);
                canvas.gameObject.SetActive(true);
            }
            else
            {
                canvas = eventSystem.transform.GetComponentInParent<Canvas>().transform;
            }
        }
    }

}
