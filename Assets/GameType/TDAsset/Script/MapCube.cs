using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TD
{
    public class MapCube : MonoBehaviour
    {
        [HideInInspector] public GameObject turretGo;
        public GameObject buildEffect;
        public void BuildTurret(GameObject turretPrefab)
        {
            turretGo = GameObject.Instantiate(turretPrefab, new Vector3(transform.position.x,1,transform.position.z), Quaternion.identity);
            GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1);
        }
        private Renderer render;
        private void Start()
        {
            render = this.GetComponent<Renderer>();
        }
        private void OnMouseEnter()
        {
            if (turretGo == null&&!EventSystem.current.IsPointerOverGameObject())
            {
                render.material.color = Color.red;
            }
        }
        private void OnMouseExit()
        {
            render.material.color = Color.white;
        }
    }
}