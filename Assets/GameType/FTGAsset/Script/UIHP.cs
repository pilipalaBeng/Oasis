using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static FTG.Player;

namespace FTG
{
    public class UIHP : MonoBehaviour
    {
        public Text name;
        public Slider slider;
        private HP hp;
        public void SetName(playerType type,string goName)
        {
            switch (type)
            {
                case playerType.ONE_P:
                    name.text = $"1P {goName}";
                    break;
                case playerType.TWO_P:
                    name.text = $"2P {goName}";
                    break;
            }
        }
        public void SetPlayerHp(HP h)
        {
            hp = h;
            hp.onHpChange += HPChange;
        }

        private void HPChange(int hp)
        {
            this.slider.value = hp;
        }
        private void OnDisable()
        {
            if (hp != null)
            {
                hp.onHpChange -= HPChange;
            }
        }
    }
}
