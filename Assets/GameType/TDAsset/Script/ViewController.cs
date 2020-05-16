using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TD
{
    public class ViewController : MonoBehaviour
    {
        public float speed = 1;
        public float mouseMoveSpeed = 600;//鼠标滑轮值很小，所以speed设置的大一些~
        void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float mouse = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(new Vector3(h * speed, -mouse * mouseMoveSpeed, v * speed) * Time.deltaTime, Space.World);
        }
    }
}