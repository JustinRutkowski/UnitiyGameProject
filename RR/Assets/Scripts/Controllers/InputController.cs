using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	public float Vertical;
	public float Horizontal;
	public Vector2 MouseInput;
    public bool Fire1;
    public bool Reload;
    public bool IsWalking;
    public bool IsSprinting;
    public bool IsCrouched;
    public bool IsReloading;
    public bool IsJumping;
    public bool IsProneing;
    public bool Fire2;
    public bool IsAiming;
    public bool IsLeaningRight;
    public bool IsLeaningLeft;
    public bool IsFiring;


    

    void Update() {
        
		Vertical = Input.GetAxis ("Vertical");
		Horizontal = Input.GetAxis ("Horizontal");
		MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        Fire2 = Input.GetButton("Fire2");
        IsAiming = Input.GetButton("Fire2");
        IsFiring = GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.FIRING || GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMEDFIRING;
        Reload = Input.GetKey(KeyCode.R);
        IsWalking = Input.GetKey(KeyCode.LeftAlt);
        IsSprinting = Input.GetKey(KeyCode.LeftShift);
        IsCrouched = Input.GetKey(KeyCode.C);
        IsReloading = Input.GetKey(KeyCode.R);

        IsJumping = Input.GetKey(KeyCode.Space);
        

        
        IsProneing = Input.GetKey(KeyCode.X);
        IsLeaningRight = Input.GetKey(KeyCode.E);
        IsLeaningLeft = Input.GetKey(KeyCode.Q);
    }   
}
