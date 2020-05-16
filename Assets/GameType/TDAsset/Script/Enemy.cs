using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TD
{
    public class Enemy : MonoBehaviour
    {
        public int hp = 150;
        private int totalHp = 0;
        public float speed = 1;
        private int index = 0;
        public GameObject explosionPrefab;
        public Slider slider;
        // Start is called before the first frame update
        void Start()
        {
            totalHp = hp;
            slider.value = totalHp;
            this.transform.position = EnemySpawner.Instance.targetPoint.startPos.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (isMove)
            {
                Move();
            }
        }
        bool isMove = true;
        private void Move()
        {
            if (index > EnemySpawner.Instance.targetPoint.targetPos.Length - 1)
            {
                isMove = false;
                return;
            }
            transform.Translate((EnemySpawner.Instance.targetPoint.targetPos[index].position - transform.position).normalized * Time.deltaTime * speed);
            if (Vector3.Distance(EnemySpawner.Instance.targetPoint.targetPos[index].position, transform.position) < 0.2f)
            {
                index++;
            }
            if (index > EnemySpawner.Instance.targetPoint.targetPos.Length - 1)
            {
                ReachDestination();
            }
        }

        private void ReachDestination()
        {
            GameObject.Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            EnemySpawner.CountEnemyAlitve--;
        }

        public void TakeDamage(int value)
        {
            if (hp <= 0)
            {
                return;
            }
            hp -= value;
            if (hp <= 0)
            {

                Die();
            }
            slider.value = (float)hp / totalHp;
        }

        private void Die()
        {
            var go = GameObject.Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(go.gameObject, 1.5f);
            Destroy(this.gameObject);
        }
    }
}