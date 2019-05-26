using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	[SerializeField]float rateOfFire;
    [SerializeField]Projectile projectile;
    [SerializeField] Transform hand;
    [SerializeField] AudioController audioReload;
    [SerializeField] AudioController audioFire;
    [SerializeField] AudioController audioEmptyFire;

    PlayerScript player;
    public Vector3 aimPoint;
    public Vector3 aimTargetOffset;

    private WeaponReloader reloader;
    private ParticleSystem muzzleFireParticleSystem;

    private WeaponRecoil m_weaponRecoil;
    private WeaponRecoil weaponRecoil
    {
        get
        {
            if (m_weaponRecoil == null)
                m_weaponRecoil = GetComponent<WeaponRecoil>();
            return m_weaponRecoil;
        }
    }
   
    [HideInInspector]
    Transform muzzle;

    float nextFireAllowed;
    public bool canFire;

    public void SetAimPoint(Vector3 target)
    {
        aimPoint = target;
    }

    void Awake() {
        muzzle = transform.Find("Muzzle");
        player = GetComponentInParent<PlayerScript>();
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
        if(player.isLocalPlayer)
            reloader.Reload();

        audioReload.PLay();
    } 

    public virtual void Fire() {
       
        canFire = false;

        if (Time.time < nextFireAllowed)
            return;

        //muzzle.LookAt(aimTarget);
        if(player.isLocalPlayer && reloader != null) // asdfgdsfgd      
        {
            if (reloader.IsReloading)
            {
                GameManager.Instance.LocalPlayer.playerState.weaponState = PlayerState.EWeaponState.IDLE;
                return;
            }

            if (reloader.RoundsRemainingInClip == 0) {
                audioEmptyFire.PLay();
                GameManager.Instance.LocalPlayer.playerState.weaponState = PlayerState.EWeaponState.IDLE;
                return;
            }

            reloader.TakeFromClip(1);
        }

        nextFireAllowed = Time.time + rateOfFire;


        muzzle.LookAt(aimPoint + aimTargetOffset);

        // instantiate the projectile
        Projectile newBullet = (Projectile)Instantiate(projectile, muzzle.position, muzzle.rotation);

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        Vector3 targetPosition = ray.GetPoint(100);

        if (Physics.Raycast(ray, out hit))
            targetPosition = hit.point;
        

        newBullet.transform.LookAt(targetPosition + aimTargetOffset);

        if (this.weaponRecoil)
            this.weaponRecoil.Activate();

        FireEffect();
        audioFire.PLay();
        canFire = true;
    }
}
