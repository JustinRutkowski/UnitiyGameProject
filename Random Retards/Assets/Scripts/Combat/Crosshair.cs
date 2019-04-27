using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

    [SerializeField] Texture2D image;
    [SerializeField] int size;

    private void OnGUI()
    {
        if (GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMING
            || GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMEDFIRING)
        {      
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        GUI.DrawTexture(new Rect(screenPosition.x - size/2, screenPosition.y - size/2, size, size), image);
        }
    }

}
