using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class FireBall : MonoBehaviour
{
    float speed = 7;
    void Start()
    {
        Object.Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        speed = speed * 1.005f;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("interact") || other.gameObject.CompareTag("Ground")) Destroy(gameObject);
    }
}
