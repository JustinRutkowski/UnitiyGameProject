using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    Animator animator;

    private PlayerAim m_PlayerAim;
    private PlayerAim playerAim
    {
        get
        {
            if (m_PlayerAim == null)
                m_PlayerAim = GameManager.Instance.LocalPlayer.playerAim;
            return m_PlayerAim;
        }
    }


	void Awake () {
        animator = GetComponentInChildren<Animator>();
	}

    void Update()
    {
        animator.SetFloat("Vertical", GameManager.Instance.InputController.Vertical);
        animator.SetFloat("Horizontal", GameManager.Instance.InputController.Horizontal);

        animator.SetBool("IsWalking", GameManager.Instance.InputController.IsWalking);
        animator.SetBool("IsSprinting", GameManager.Instance.InputController.IsSprinting);
        animator.SetBool("IsCrouched", GameManager.Instance.InputController.IsCrouched);
        animator.SetBool("IsReloading", GameManager.Instance.InputController.IsReloading);
        animator.SetBool("IsJumping", GameManager.Instance.InputController.IsJumping);
        animator.SetBool("IsAiming", GameManager.Instance.InputController.IsAiming);
        animator.SetFloat("AimAngle", playerAim.GetAngle());
        animator.SetBool("IsLeaningRight", GameManager.Instance.InputController.IsLeaningRight);
        animator.SetBool("IsLeaningLeft", GameManager.Instance.InputController.IsLeaningLeft);
        animator.SetBool("IsFiring", GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.FIRING
        || GameManager.Instance.LocalPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMEDFIRING);
    }
}
