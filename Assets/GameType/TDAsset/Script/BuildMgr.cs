using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TD
{
    public class BuildMgr : MonoBehaviour
    {
        public TurretData laserTurretData;
        public TurretData missileTurret;
        public TurretData standarTurrret;

        //当前选择的炮台
        public TurretData selectTurretData;
        private void Awake()
        {
            //默认第一个炮台被设置
            OnLaserSelectd(true);
        }
        private int money = 1000;
        public Text moneyTex;
        void MoneyChange(int change = 0)
        {
            money += change;
            moneyTex.text = $"${money}";
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("mapCube"));
                    if (isCollider)
                    {
                        MapCube cube = hit.collider.gameObject.GetComponent<MapCube>();//得到cube
                        if (cube.turretGo == null)
                        {
                            //创建
                            if (money > selectTurretData.cost)
                            {
                                MoneyChange(-selectTurretData.cost);
                                cube.BuildTurret(selectTurretData.turretPrefab);
                            }
                            else
                            {
                                //钱不够了
                                GameMgr.UiMgr.ShowMessageBox("钱不足了");
                            }
                        }
                        else
                        {
                            //升级
                        }
                    }
                }
            }
        }

        public void OnLaserSelectd(bool isOn)
        {
            if (isOn)
            {
                selectTurretData = laserTurretData;
            }
        }
        public void OnmissileSelectd(bool isOn)
        {
            if (isOn)
            {
                selectTurretData = missileTurret;
            }
        }
        public void OnstandarSelectd(bool isOn)
        {
            if (isOn)
            {
                selectTurretData = standarTurrret;
            }
        }
    }
}