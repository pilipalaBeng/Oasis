using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Camera camera;
    public Transform player1;
    public Transform player2;
  public   Vector3 offsetPos;                          //偏移   offset
    //float offset = 5;
    // Use this for initialization
    //void Start()
    //{
    //    offsetPos = transform.position - (player1.position + player2.position) / 2;
    //    //offset = 0;
    //}

    // Update is called once per frame
    void Update()
    {
        //transform.position = offsetPos + (player1.position + player2.position) / 2;
        transform.LookAt(offsetPos+(player1.position + player2.position) / 2);
        if (!(Vector3.Distance(player1.position, player2.position) / 2 < 3))
        {
            camera.orthographicSize = /*offset + */Vector3.Distance(player1.position, player2.position) / 2;
        }
    }
}
