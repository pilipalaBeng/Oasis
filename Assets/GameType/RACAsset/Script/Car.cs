using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RAC
{
    public class Car : MonoBehaviour
    {
        public WheelCollider qz, qy, hz, hy;
        public Transform qzTf, qyTf, hzTf, hyTf;
        public float maxMotorTorque;
        public float maxSteeringAngle;
        public float brakeTorqueValue;
        public Vector3 normalPos;
        public AudioSource audioSource;
        public AudioClip openClip, norClip, runClip, startRunClip, endRunClip;
        public TrailRenderer trailRenderHz, trailRenderHy;
        bool isOpenCar = false;
        private void Start()
        {
            audioSource.clip = openClip;
            audioSource.loop = false;

            PlayAudio(openClip, () => { isOpenCar = true; });
        }
        private void StopAudio()
        {
            audioSource.Stop();
            StopAllCoroutines();
        }
        private void PlayAudio(AudioClip clip, UnityAction callBack = null)
        {
            StartCoroutine(PlayAudioCor(clip, callBack));
        }
        IEnumerator PlayAudioCor(AudioClip clip, UnityAction callBack = null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            while (audioSource.isPlaying == true)
            {
                yield return new WaitForEndOfFrame();
            }
            callBack?.Invoke();
        }
        IEnumerator InvokeTime(float time, UnityAction callBack)
        {
            float curTime = 0;
            while (curTime < time)
            {
                curTime += Time.deltaTime;
                yield return null;
            }
            callBack?.Invoke();
        }
        float oldMotor = 0;
        bool isAudioLock = false;
        enum CurState
        {
            normal,
            run,
            startrun,
            endrun,
        }
        bool isCollision = false;
        //private CurState oldCurState = CurState.normal;
        //private CurState curState = CurState.normal;
        //private AudioClip curClip;
        private void SelectPlayAudio(AudioClip clip)
        {
            if (audioSource.clip != clip)
            {
                StopAudio();
                PlayAudio(clip);
            }
            else
            {
                if (audioSource.isPlaying == false)
                {
                    PlayAudio(clip);
                }
            }
        }
        public static bool isControl = true;
        private void Update()
        {
            if (isControl)
            {

                float motor = maxMotorTorque * Input.GetAxis("Vertical");
                float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
                qz.steerAngle = steering;
                qy.steerAngle = steering;

                SetPos(qz, qzTf);
                SetPos(qy, qyTf);
                SetPos(hz, hzTf);
                SetPos(hy, hyTf);
                if (Input.GetKey(KeyCode.Space))
                {
                    //qz.brakeTorque = brakeTorqueValue;
                    //qy.brakeTorque = brakeTorqueValue;
                    hz.brakeTorque = brakeTorqueValue;
                    hy.brakeTorque = brakeTorqueValue;

                    trailRenderHz.enabled = true;
                    trailRenderHy.enabled = true;
                    motor = 0;
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    trailRenderHz.enabled = false;
                    trailRenderHy.enabled = false;
                    //StartCoroutine(InvokeTime())
                    //qz.brakeTorque = 0;
                    //qy.brakeTorque = 0;
                    hz.brakeTorque = 0;
                    hy.brakeTorque = 0;
                }

                if (isOpenCar)
                {
                    if (motor == 0)
                    {
                        if (audioSource.clip == endRunClip)
                        {
                            if (isAudioLock == false)
                            {
                                isAudioLock = true;
                                PlayAudio(norClip, () => { isAudioLock = false; });
                            }
                            //if (isCollision)
                            //{
                            //    StopAudio();
                            //    isAudioLock = true;
                            //    PlayAudio(norClip, () => { isAudioLock = false; });
                            //}
                        }
                        else
                        {
                            if (audioSource.clip != norClip)
                            {
                                StopAudio();
                                isAudioLock = true;
                                PlayAudio(norClip, () => { isAudioLock = false; });
                            }
                            else
                            {
                                if (isAudioLock == false)
                                {
                                    isAudioLock = true;
                                    PlayAudio(norClip, () => { isAudioLock = false; });
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(motor) > Mathf.Abs(oldMotor))
                        {
                            //if (isCollision)
                            //{
                            //    StopAudio();
                            //    isAudioLock = true;
                            //    PlayAudio(norClip, () => { isAudioLock = false; });
                            //}
                            if (audioSource.clip != startRunClip)
                            {
                                StopAudio();
                                isAudioLock = true;
                                PlayAudio(startRunClip, () => { isAudioLock = false; });
                            }
                            else
                            {
                                if (isAudioLock == false)
                                {
                                    isAudioLock = true;
                                    PlayAudio(startRunClip, () => { isAudioLock = false; });
                                }
                            }
                        }
                        else if (Mathf.Abs(motor) == maxMotorTorque)
                        {
                            //if (isCollision)
                            //{
                            //    StopAudio();
                            //    isAudioLock = true;
                            //    PlayAudio(norClip, () => { isAudioLock = false; });
                            //}
                            if (audioSource.clip != runClip)
                            {
                                if (isAudioLock == false)
                                {
                                    StopAudio();
                                    isAudioLock = true;
                                    PlayAudio(runClip, () => { isAudioLock = false; });
                                }
                            }
                            else
                            {
                                if (isAudioLock == false)
                                {
                                    isAudioLock = true;
                                    PlayAudio(runClip, () => { isAudioLock = false; });
                                }
                            }
                        }
                        else if (Mathf.Abs(motor) < Mathf.Abs(oldMotor))
                        {
                            //if (isCollision)
                            //{
                            //    StopAudio();
                            //    isAudioLock = true;
                            //    PlayAudio(norClip, () => { isAudioLock = false; });
                            //}
                            if (audioSource.clip != endRunClip)
                            {
                                StopAudio();
                                isAudioLock = true;
                                PlayAudio(endRunClip, () => { isAudioLock = false; });
                            }
                            else
                            {
                                if (isAudioLock == false)
                                {
                                    isAudioLock = true;
                                    PlayAudio(endRunClip, () => { isAudioLock = false; });
                                }
                            }
                        }
                    }

                }
                oldMotor = motor;
                qz.motorTorque = motor;
                qy.motorTorque = motor;
                hz.motorTorque = motor;
                hy.motorTorque = motor;

                float tempAng = 0;
                if (transform.eulerAngles.z < 0)
                {
                    tempAng = transform.eulerAngles.z + 360;
                }
                else
                {
                    tempAng = transform.eulerAngles.z;
                }
                if (tempAng % 360 > 85)
                {
                    if (tempAng % 360 < 340)
                    {
                        GameMgr.RackGameMgr.Resurgence(this.transform);
                    }
                }
                //Debug.Log($"transform.eulerAngles.z {transform.eulerAngles.z }");
                //var v3= Vector3.Angle(transform.eulerAngles, transform.forward);
                //Debug.Log($"v3 {Mathf.Acos(v3)*Mathf.Rad2Deg}");
                //if (transform.eulerAngles.z > 70)
                //{
                //    GameMgr.RackGameMgr.Resurgence(this.transform);
                //}
            }
        }
        private void SetPos(WheelCollider wheel, Transform tf)
        {
            wheel.GetWorldPose(out Vector3 pos, out Quaternion quat);
            tf.position = pos;
            tf.rotation = quat;
        }
        public float stayTimer = 0;
        private void OnCollisionEnter(Collision collision)
        {
            isCollision = true;
            tempStayTimer = 0;
        }
        float tempStayTimer = 0;

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag(GameMgr.RackGameMgr.trackTag))
            {
                tempStayTimer += Time.deltaTime;
            }

            if (tempStayTimer >= stayTimer)
            {
                //GameMgr.RackGameMgr.SetCurTrack(collision.transform);
                //复位
                GameMgr.RackGameMgr.Resurgence(this.transform);
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            isCollision = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            GameMgr.RackGameMgr.SetCurCheckPos(other.transform);
            if (other.transform.name.Equals(GameMgr.RackGameMgr.startPosName))
            {
                GameMgr.RackGameMgr.AddStartPosNum();
            }
        }
    }
}