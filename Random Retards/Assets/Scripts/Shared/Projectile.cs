using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    [SerializeField] float damage;
    [SerializeField] Transform bulletHole;

    Vector3 destination;
    Collider collider;
    //private float _timer;
    //public float Range = 1;

    void Start() {
        Destroy(gameObject, timeToLive);
        //_timer = 0;
    }

    

    void Update() {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (destination != Vector3.zero)
            return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 5f))
            CheckDestructable(hit);

        if (isDestinationReached())
        {

            // Bullethole
            destination = hit.point + hit.normal * 0.001f;
            Transform hole = (Transform)Instantiate(bulletHole, destination, Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, 180f, 0));
            hole.SetParent(hit.transform);

            Destroy(gameObject);
            return;
        }
      
       

        

        /*
        _timer += Time.deltaTime;
        
	    // Create a vector at the center of our camera's viewport
        Vector3 lineOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
       
	    // Draw a line in the Scene View  from the point lineOrigin in the direction of fpsCam.transform.forward * weaponRange, using the color green
        Debug.DrawRay(lineOrigin, Camera.main.transform.forward * Range, Color.green);
        */
    }

    void CheckDestructable(RaycastHit hitinfo) {
      
        var destructable = hitinfo.transform.GetComponent<Destructable>();

        collider = hitinfo.transform.GetComponent<Collider>();
        destination = hitinfo.point;

        if (destructable == null)
            return;

        if (collider == null)
            return;

        destructable.TakeDamage(damage);
       
    }

    bool isDestinationReached()
    {
        /*if (destination == Vector3.zero)
            return false;

        Vector3 directionToDestination = destination - transform.position;   
        float dot = Vector3.Dot(directionToDestination, transform.forward);
        print(dot);
        if (dot == 0)           
            return true;

        return false;*/
       
        if (collider != null)
            return true;
        return false;
    }
}
