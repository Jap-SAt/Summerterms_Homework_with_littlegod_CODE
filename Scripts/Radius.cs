using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour
{
    public float radius = 5f; // กำหนดรัศมีของ Gizmos
    public Color gizmoColor = Color.green; // กำหนดสีของ Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.gameObject.name} เข้ามาในรัศมี");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{other.gameObject.name} ออกจากรัศมี");
    }

    private void Start()
    {
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = radius;
    }
}
