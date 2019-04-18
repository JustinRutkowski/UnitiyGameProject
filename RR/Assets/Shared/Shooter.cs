using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	[SerializeField]float rateOfFire;
    [SerializeField]Projectile projectile;
    [SerializeField] Transform hand;
    [SerializeField] AudioController audioReload;
    [SerializeField] AudioController audioFire;

    private WeaponReloader reloader;
    private ParticleSystem muzzleFireParticleSystem;
   
    [HideInInspector]
    Transform muzzle;

    float nextFireAllowed;
    public bool canFire;

    void Awake() {
        muzzle = transform.Find("Muzzle");
        reloader = GetComponent<WeaponReloader>();
        transform.SetParent(hand);
        muzzleFireParticleSystem = muzzle.GetComponent<ParticleSystem>();
    }

    void FireEffect()
    {
        if (muzzleFireParticleSystem == null)
            return;
        muzzleFireParticleSystem.Play();
    }   

    public void Reload()
    {
        if (reloader == null)     
            return;        
        reloader.Reload();
        audioReload.PLay();
    }

    public virtual void Fire() {
       
        canFire = false;

        if (Time.time < nextFireAllowed)
            return;

        if(reloader != null)
        {
            if (reloader.IsReloading)
            {
                return;
            }

            if (reloader.RoundsRemainingInClip == 0) { 
                return;
            }

            reloader.TakeFromClip(1);
        }

        nextFireAllowed = Time.time + rateOfFire;

        FireEffect();


        // instantiate the projectile
        Instantiate(projectile, muzzle.position, muzzle.rotation);
        audioFire.PLay();
        canFire = true;
    }
}
