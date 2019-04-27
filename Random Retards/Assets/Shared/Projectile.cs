using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    [SerializeField] float Speed;
    [SerializeField] float timeToLive;
    [SerializeField] float damage;

    void Start() {
        Destroy(gameObject, timeToLive);
    }

    void Update() {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
      
        var destructable = other.transform.GetComponent<Destrucable>();
        if (destructable == null)
            return;

        destructable.TakeDamage(damage);
    }
}
