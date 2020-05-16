using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TD
{
    public class Turret : MonoBehaviour
    {
        public List<GameObject> enemys = new List<GameObject>();
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("enemy"))
            {
                enemys.Add(other.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("enemy"))
            {
                enemys.Remove(other.gameObject);
            }
        }

        public int attackRateTime = 1;
        public GameObject bulletPrefab;//子弹
        public Transform firePosition;
        public float timer = 0;
        public Transform head;
        private void Start()
        {
            timer = attackRateTime;
        }
        private void Update()
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer > attackRateTime)
            {
                timer = 0;
                Attack();
            }
            if (enemys.Count>0 && enemys[0]!=null)
            {
                Vector3 targetpos = enemys[0].transform.position;
                targetpos.y = head.position.y;
                head.LookAt(targetpos);
            }
        }

        private void Attack()
        {
            if (enemys[0] == null)
            {
                UpdateEnemys();
            }
            if (enemys.Count>0)
            {
                GameObject bullett = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
                bullett.GetComponent<Bullet>().SetTarget(enemys[0].transform);
            }
            else
            {
                timer = attackRateTime;
            }
          
        }
        void UpdateEnemys()
        {
            List<int> temp = new List<int>();
            for (int i = 0; i < enemys.Count; i++)
            {
                if (enemys[i]==null)
                {
                    temp.Add(i);
                }
            }
            for (int i = 0; i < temp.Count; i++)
            {
                enemys.RemoveAt(temp[i]-i);
            }
        }
    }
}