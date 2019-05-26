using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

    [SerializeField] float speed;

    public Transform Reticle;
    Transform crossTop;
    Transform crossBottom;
    Transform crossLeft;
    Transform crossRight;

    float reticleStartPoint;

    private void Start()
    {
        if (!GetComponentInParent<PlayerScript>().isLocalPlayer)
        {
            Destroy(this.gameObject);
            return;
        }
        
        Reticle = GameObject.Find("Canvas/Reticle").transform;
        
        crossTop = Reticle.Find("Cross/Top").transform;
        crossBottom = Reticle.Find("Cross/Bottom").transform;
        crossLeft = Reticle.Find("Cross/Left").transform;
        crossRight = Reticle.Find("Cross/Right").transform;

        reticleStartPoint = crossTop.localPosition.y;
    }

    void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Reticle.transform.position = Vector3.Lerp(Reticle.transform.position, screenPosition, speed * Time.deltaTime);     
    }

    public void ApplayScale(float scale)
    {
        crossTop.localPosition = new Vector3(0, reticleStartPoint + scale, 0);
        crossBottom.localPosition = new Vector3(0,-reticleStartPoint - scale, 0);
        crossLeft.localPosition = new Vector3(-reticleStartPoint - scale, 0, 0);
        crossRight.localPosition = new Vector3(reticleStartPoint + scale, 0, 0);
    }

    /*[SerializeField] Texture2D image;
    [SerializeField] int size;

    private void OnGUI()
    {
        if (GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMING
            || GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMEDFIRING)
        {      
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
     
        }
    }*/

}
