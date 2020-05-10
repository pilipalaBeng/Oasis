using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FTG
{
    public class HP : MonoBehaviour
    {
        public UnityAction<int> onHpChange;
        public int hp = 100;//血量
        public void TakeBeating(int hp)
        {
            this.hp -= hp;
            if (this.hp <= 0)
            {
                this.gameObject.SetActive(false);
                Debug.Log($"full");
                switch (this.GetComponent<Player>().playerTypeEnum)
                {
                    case Player.playerType.ONE_P:
                        GameMgr.UiMgr.ShowMessageBox("1P输了");
                        break;
                    case Player.playerType.TWO_P:
                        GameMgr.UiMgr.ShowMessageBox("2P输了");
                        break;
                    case Player.playerType.AI:
                        GameMgr.UiMgr.ShowMessageBox("AI输了");
                        break;
                }
              
            }
            onHpChange?.Invoke(this.hp);
        }
    }
}