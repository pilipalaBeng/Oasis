using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RAC
{
    public class RacGameMgr : MonoBehaviour
    {
        public Track track;
        public string startPosName = "start";
        public string trackTag = "track";
        public void SetCurTrack(Transform tf)
        {
            //track.SetCurTrack(tf);
        }
        public void SetCurCheckPos(Transform tf)
        {
            track.SetCurCheckPos(tf);
        }
        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="car"></param>
        public void Resurgence(Transform car)
        {
            track.Resurgence(car);
        }
        public void AddStartPosNum()
        {
            track.AddStartPosNum();
        }
        public void Win()
        {
            GameMgr.UiMgr.ShowMessageBox("胜利");
            Car.isControl = false;
        }
    }
}
