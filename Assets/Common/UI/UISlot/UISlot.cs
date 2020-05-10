using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image slotIma;
    [SerializeField] private Text slotNum;
    [SerializeField] private Text slotName;
    [SerializeField] private Button btn;

    public UnityAction onBtnClick;
    private void Awake()
    {
        btn.onClick.AddListener(OnBtnClick);
    }

    private void OnBtnClick()
    {
        onBtnClick?.Invoke();
    }

    public void SetData(Sprite spr, string number, string name, UnityAction btnClick)
    {
        if (slotIma != null)
        {
            slotIma.sprite = spr;
        }
        if (slotNum != null)
        {
            slotNum.text = number;
        }
        if (name != null)
        {
            slotName.text = name;
        }
        onBtnClick = btnClick;
    }
}
