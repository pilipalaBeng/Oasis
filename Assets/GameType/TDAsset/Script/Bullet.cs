using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TD
{
    public class Bullet : MonoBehaviour
    {
        public int damage = 50;
        public float speed = 20;
        private Transform target;
        public GameObject explosionEffectPrefab;
        public void SetTarget(Transform target)
        {
            this.target = target;
        }
        private void Update()
        {
            if (target == null)
            {
                var eff = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                Destroy(eff.gameObject, 1);
                Destroy(this.gameObject);
                return;
            }
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage(damage);
                var eff = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                Destroy(eff.gameObject, 1);
                Destroy(this.gameObject);
            }
        }
    }
}