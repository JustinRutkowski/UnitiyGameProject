using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    Animator animator;
    
    public float Vertical;
    public float Horizontal;
    public bool isWalking;
    public bool isSprinting;
    public bool isCrouched;
    public bool isReloading;
    public bool isAiming;
    public bool IsLeaningRight;
    public bool IsLeaningLeft;
    public bool isJumping;
    public bool isFiring;
    public float AimAngle;

    private PlayerAim m_PlayerAim;
    private PlayerAim PlayerAim
    {
        get
        {
            if (m_PlayerAim == null)  
                if (Player.isLocalPlayer)
                    m_PlayerAim = GameManager.Instance.LocalPlayer.playerAim;
            return m_PlayerAim;
        }
    }

    private PlayerScript m_Player;
    public PlayerScript Player
    {
        get
        {
            if (m_Player == null)
                m_Player = GetComponent<PlayerScript>();
            return m_Player;
        }
    }

    void GetLocalPlayerInput()
    {
        Vertical = Player.Inputstate.Vertical;
        Horizontal = Player.Inputstate.Horizontal;
        isWalking = Player.Inputstate.IsWalking;
        isSprinting = Player.Inputstate.IsSprinting;
        isCrouched = Player.Inputstate.IsCrouched;
        isReloading = Player.Inputstate.IsReloading;
        isJumping = Player.Inputstate.IsJumping;
        isAiming = Player.Inputstate.IsAiming;
        IsLeaningRight = Player.Inputstate.IsLeaningRight;
        IsLeaningLeft = Player.Inputstate.IsLeaningLeft;
        AimAngle = PlayerAim.GetAngle();
        isFiring = GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.FIRING || GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMEDFIRING;
        
    }   

    void Awake () {
        animator = GetComponentInChildren<Animator>();
	}

    void Update()
    {
        /*if (GameManager.Instance.isPaused)
            return; */
       
        if (Player.isLocalPlayer)
        {
            GetLocalPlayerInput();
           
        }
        animator.SetFloat("Vertical", Vertical);
        animator.SetFloat("Horizontal", Horizontal);
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsSprinting", isSprinting);
        animator.SetBool("IsCrouched", isCrouched);
        animator.SetBool("IsReloading", isReloading);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsAiming", isAiming);
        animator.SetBool("IsLeaningRight", IsLeaningRight);
        animator.SetBool("IsLeaningLeft", IsLeaningLeft);
        animator.SetFloat("AimAngle", AimAngle);
        animator.SetBool("IsFiring", isFiring);



    }
}
