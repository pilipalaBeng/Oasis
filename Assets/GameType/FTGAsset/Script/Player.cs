using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FTG
{
    public class Player : MonoBehaviour
    {
        public enum playerType
        {
            ONE_P,
            TWO_P,
            AI,
        }
        enum direction
        {
            L,
            R,
        }
        enum State
        {
            None,
            Run,
            Back,
            Atk,
        }
        private State state = State.None;

        public UIHP ui;
        public playerType playerTypeEnum = playerType.ONE_P;
        private Animator anim;
        public List<TrailRenderer> trails;

        public Player target;
        public float distance = 1;//攻击距离
        public int atkValue = 10;    //伤害值

        [Range(0, 60)]
        public float angle = 30;//判定扇形攻击区域角度
        private void Awake()
        {
            anim = this.GetComponent<Animator>();
            SetTrails(false);
            ui.SetName(playerTypeEnum, this.gameObject.name);
            ui.SetPlayerHp(this.GetComponent<HP>());
        }
        public bool desLooAt = true;
        private bool isLookAt = true;

        private State State1
        {
            get
            {
                return state;
            }
        }
        float timer = 1;
        float time = 0;
        private void Update()
        {
            if (target.GetComponent<HP>().hp<=0)
            {
                return;
            }
            switch (playerTypeEnum)
            {
                case playerType.ONE_P:
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        Atk();
                    }
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        Move(true, direction.L, true);
                    }
                    else if (Input.GetKeyUp(KeyCode.A))
                    {
                        Move(false, direction.L, false);
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        Move(true, direction.R, true);
                    }
                    else if (Input.GetKeyUp(KeyCode.D))
                    {
                        Move(false, direction.R, false);
                    }
                    break;
                case playerType.TWO_P:
                    if (Input.GetKeyDown(KeyCode.Keypad1))
                    {
                        Atk();
                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        Move(true, direction.L, true);
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftArrow))
                    {
                        Move(false, direction.L, false);
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Move(true, direction.R, true);
                    }
                    else if (Input.GetKeyUp(KeyCode.RightArrow))
                    {
                        Move(false, direction.R, false);
                    }
                    break;
                case playerType.AI:
                    isLookAt = true;
                    time += Time.deltaTime;
                    if (time > timer)
                    {
                        time = 0;
                        bool isAtk = false;
                        var dis = Vector3.Distance(this.transform.position, target.transform.position);

                        Vector3 norVec = transform.rotation * Vector3.forward;
                        Vector3 temVec = target.transform.position - transform.position;

                        var tempAng = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;
                        if (dis < distance)
                        {
                            if (tempAng <= angle)
                            {
                                isAtk = true;
                            }
                        }
                        if (isAtk)
                        {
                            Atk();
                        }
                        else
                        {
                            //if (state == State.Run)
                            //{
                            //    return;
                            //}
                            var acos = Mathf.Acos(Vector3.Dot(this.transform.forward, Vector3.forward)) * Mathf.Rad2Deg;
                            switch (target.state)
                            {
                                case State.None:
                                    if (acos > 90)
                                    {
                                        Move(true, direction.R, true);
                                    }
                                    else
                                    {
                                        Move(true, direction.L, true);
                                    }
                                    break;
                                case State.Run:
                                    if (acos > 90)
                                    {
                                        Move(true, direction.R, true);
                                    }
                                    else
                                    {
                                        Move(true, direction.L, true);
                                    }
                                    break;
                                case State.Back:
                                    if (acos > 90)
                                    {
                                        Move(true, direction.L, true);
                                    }
                                    else
                                    {
                                        Move(true, direction.R, true);
                                    }
                                    break;
                                case State.Atk:
                                    if (acos > 90)
                                    {
                                        Move(false, direction.R, true);
                                    }
                                    else
                                    {
                                        Move(false, direction.L, true);
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            }


            if (desLooAt)
            {
                if (isLookAt)
                {
                    this.transform.LookAt(target.transform);
                }
            }

        }
        private void SetTrails(bool isActive)
        {
            int sentry = 0;
            while (sentry < trails.Count)
            {
                trails[sentry].enabled = isActive;
                sentry++;
            }
        }
        private void Move(bool isMove, direction dir, bool isDown)
        {
            if (isMove == false)
            {
                isLookAt = true;
            }
            else
            {
                isLookAt = false;
            }
            if (Vector3.Distance(this.transform.position, target.transform.position) > 20)
            {
                return;
            }
            var acos = Mathf.Acos(Vector3.Dot(this.transform.forward, Vector3.forward)) * Mathf.Rad2Deg;
            switch (playerTypeEnum)
            {
                case playerType.ONE_P:
                    switch (dir)
                    {
                        case direction.L:
                            if (acos < 90)
                            {
                                anim.SetBool("Moving", isMove);
                                state = State.Run;
                            }
                            else
                            {
                                if (isDown == true)
                                {
                                    this.transform.position -= this.transform.forward;
                                    state = State.Back;
                                }
                            }
                            break;
                        case direction.R:
                            if (acos > 90)
                            {
                                anim.SetBool("Moving", isMove);
                                state = State.Run;
                            }
                            else
                            {
                                if (isDown == true)
                                {
                                    this.transform.position -= this.transform.forward;
                                    state = State.Back;
                                }
                            }
                            break;
                    }
                    break;
                case playerType.TWO_P:
                    switch (dir)
                    {
                        case direction.L:
                            if (acos < 90)
                            {
                                anim.SetBool("Moving", isMove);
                                state = State.Run;
                            }
                            else
                            {
                                if (isDown == true)
                                {
                                    this.transform.position -= this.transform.forward;
                                    state = State.Back;
                                }
                            }
                            break;
                        case direction.R:
                            if (acos > 90)
                            {
                                anim.SetBool("Moving", isMove);
                                state = State.Run;
                            }
                            else
                            {
                                if (isDown == true)
                                {
                                    this.transform.position -= this.transform.forward;
                                    state = State.Back;
                                }
                            }
                            break;
                    }
                    break;
                case playerType.AI:
                    Debug.Log($"acos {acos}");
                    Debug.Log($"dir {dir}");
                    Debug.Log($"isMove {isMove}");
                    switch (dir)
                    {
                        case direction.L:
                            if (acos < 90)
                            {
                                anim.SetBool("Moving", isMove);
                                state = State.Run;
                            }
                            else
                            {
                                if (isDown == true)
                                {
                                    this.transform.position -= this.transform.forward;
                                    state = State.Back;
                                }
                            }
                            break;
                        case direction.R:
                            if (acos > 90)
                            {
                                anim.SetBool("Moving", isMove);
                                state = State.Run;
                            }
                            else
                            {
                                if (isDown == true)
                                {
                                    this.transform.position -= this.transform.forward;
                                    state = State.Back;
                                }
                            }
                            break;
                    }
                    break;
            }
        }
        private void Atk()
        {
            state = State.Atk;
            SetTrails(true);
            anim.SetTrigger("Attack1Trigger");
            isLookAt = false;
        }
        public void Hit()
        {
            var dis = Vector3.Distance(this.transform.position, target.transform.position);

            Vector3 norVec = transform.rotation * Vector3.forward;
            Vector3 temVec = target.transform.position - transform.position;

            var tempAng = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;
            if (dis < distance)
            {
                if (tempAng <= angle)
                {
                    if (target.GetComponent<HP>() != null)
                    {
                        target.GetComponent<HP>().TakeBeating(atkValue);
                    }
                }
            }
        }
        public void StopTrails()
        {
            state = State.None;
            SetTrails(false);
            isLookAt = true;
        }
    }
}