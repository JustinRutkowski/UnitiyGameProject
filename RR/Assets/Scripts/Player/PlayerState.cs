using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public enum EMoveState
    {
        WALKING,
        RUNNING,
        CROUCHING,
        SPRINTING,
        PRONEING
    }

    public enum EWeaponState
    {
        IDLE,
        FIRING,
        AIMING,
        AIMEDFIRING
    }

    public EMoveState moveState;
    public EWeaponState weaponState;

    private InputController m_inputController;
    public InputController InputController
    {
        get
        {
            if (m_inputController == null)
                m_inputController = GameManager.Instance.InputController;
            return m_inputController;
        }
    }

    private void Update()
    {
        SetMoveState();
        SetWeaponState();
    }

    void SetWeaponState()
    {
        weaponState = EWeaponState.IDLE;

        if (InputController.Fire1)
            weaponState = EWeaponState.FIRING;

        if (InputController.Fire2)
            weaponState = EWeaponState.AIMING;

        if (InputController.Fire1 && InputController.Fire2)
            weaponState = EWeaponState.AIMEDFIRING;
    }

    void SetMoveState()
    {
        moveState = EMoveState.RUNNING;

        if (InputController.IsSprinting)
            moveState = EMoveState.SPRINTING;

        if (InputController.IsWalking)
            moveState = EMoveState.WALKING;

        if (InputController.IsCrouched)
            moveState = EMoveState.CROUCHING;

        if (InputController.IsCrouched)
            moveState = EMoveState.CROUCHING;

        if (InputController.IsProneing)
            moveState = EMoveState.PRONEING;
    }

}
