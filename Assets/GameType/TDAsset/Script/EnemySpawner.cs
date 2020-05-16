using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TD
{
    public class EnemySpawner : MonoBehaviour
    {
        public static int CountEnemyAlitve = 0;
        public List<Wave> waves;
        public TargetPoint targetPoint;
        public float waveRate = 0.2f;
        private static EnemySpawner instance;

        public static EnemySpawner Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = Transform.FindObjectOfType<EnemySpawner>();
                }
                return instance;
            }
        }

        private void Start()
        {
            StartCoroutine(SpawmEnemy());
        }
        IEnumerator SpawmEnemy()
        {
            foreach (var item in waves)
            {
                for (int i = 0; i < item.count; i++)
                {
                    GameObject.Instantiate(item.enemyPrefab, targetPoint.startPos.position, Quaternion.identity);
                    CountEnemyAlitve++;
                    if (i != item.count - 1)
                    {
                        yield return new WaitForSeconds(item.rate);
                    }
                }
                while (CountEnemyAlitve > 0)
                {
                    yield return 0;
                }
                yield return new WaitForSeconds(waveRate);
            }
        }
    }
}