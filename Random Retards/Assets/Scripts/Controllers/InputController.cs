using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    [System.Serializable]
    public class InputState
    {
        public float Vertical;
        public float Horizontal;
        public float AimAngle;
        public bool Fire1;
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
        public bool canJump;
    }

    public float Vertical { get { return State.Vertical; } }
    public float Horizontal { get { return State.Horizontal; } }
    public float AimAngle { get { return State.AimAngle; } }
    public bool Fire1 { get { return State.Fire1; } }
    public bool Fire2 { get { return State.Fire2; } }
    public bool IsWalking { get { return State.IsWalking; } }
    public bool IsSprinting { get { return State.IsSprinting; } }
    public bool IsCrouched { get { return State.IsCrouched; } }
    public bool IsReloading { get { return State.IsReloading; } }
    public bool IsJumping { get { return State.IsJumping; } }
    public bool IsProneing { get { return State.IsProneing; } }
    public bool IsAiming { get { return State.IsAiming; } }
    public bool IsLeaningRight { get { return State.IsLeaningRight; } }
    public bool IsLeaningLeft { get { return State.IsLeaningLeft; } }
    public bool IsFiring { get { return State.IsFiring; } }
   
    public Vector2 MouseInput;
    public InputState State;

    void Start()
    {
        State = new InputState();
    }

    void Update() {

         

        State.Vertical = Input.GetAxis("Vertical");
        State.Horizontal = Input.GetAxis("Horizontal");
        State.AimAngle = GameManager.Instance.LocalPlayer.playerAim.GetAngle();
		MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        State.Fire1 = Input.GetButton("Fire1");
        State.Fire2 = Input.GetButton("Fire2");
        State.IsAiming = Input.GetButton("Fire2");
        State.IsFiring = Input.GetButton("Fire1") || Input.GetButton("Fire1") && Input.GetButton("Fire2");
        //IsFiring = GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.FIRING || GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMEDFIRING;
        State.IsWalking = Input.GetKey(KeyCode.LeftAlt);
        State.IsSprinting = Input.GetKey(KeyCode.LeftShift);
        State.IsCrouched = Input.GetKey(KeyCode.C);
        State.IsReloading = Input.GetKey(KeyCode.R);
        State.IsJumping = Input.GetKey(KeyCode.Space);
        State.IsProneing = Input.GetKey(KeyCode.X);
        State.IsLeaningRight = Input.GetKey(KeyCode.E);
        State.IsLeaningLeft = Input.GetKey(KeyCode.Q);
       

    }  

}
