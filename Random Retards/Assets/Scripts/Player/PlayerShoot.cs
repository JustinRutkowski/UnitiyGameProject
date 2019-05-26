using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    [SerializeField] Shooter assaultRifle;

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

    void Update() {

        // the local player shoots only for himself 
        if (!Player.isLocalPlayer)
            return;

        if (GameManager.Instance.InputController.State.IsSprinting)
        {
            return;
        }

        if (GameManager.Instance.InputController.State.Fire1) {          
            assaultRifle.Fire();
        }
       
       

    }
}
