using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RAC
{
    public class Track : MonoBehaviour
    {
        public Transform[] tracks;
        public Transform[] checkPos;
        public Transform[] resurgencePos;
        public Transform curCheckPos;
        public Transform curTrack;
        public Transform startPos;
        private int num = -1;
        public int winNum = 3;
        public void AddStartPosNum()
        {
            num++;
            if (num == winNum)
            {
                GameMgr.RackGameMgr.Win();
            }
        }
        public void SetCurTrack(Transform tf)
        {
            curTrack = tf;
        }
        public void SetCurCheckPos(Transform tf)
        {
            for (int i = 0; i < checkPos.Length; i++)
            {
                if (checkPos[i] == tf)
                {
                    curCheckPos = tf;
                }
            }
        }
        public void Resurgence(Transform car)
        {
            if (curCheckPos == null)
            {
                car.GetComponent<Rigidbody>().isKinematic = true;
                car.position = startPos.position;
                car.rotation = startPos.rotation;
                car.GetComponent<Rigidbody>().isKinematic = false;
                return;
            }
            else
            {
                for (int i = 0; i < checkPos.Length; i++)
                {
                    if (curCheckPos == checkPos[i])
                    {
                        car.GetComponent<Rigidbody>().isKinematic = true;
                        car.position = resurgencePos[i].position;
                        car.rotation = resurgencePos[i].rotation;
                        car.GetComponent<Rigidbody>().isKinematic = false;
                        return;
                    }
                }
            }
        }
    }
}
