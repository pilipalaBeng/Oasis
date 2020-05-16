using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TD
{
    public enum TurrerType
    {
        LaserTurret,
        MissileTurret,
        Standarturrret
    }
    [System.Serializable]
    public class TurretData 
    {
        public GameObject turretPrefab;
        public int cost;
        public GameObject turretUpgradeePrefab;
        public int costUpgraded;
        public TurrerType type;
    }
}