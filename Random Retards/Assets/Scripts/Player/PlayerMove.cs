using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    InputController.InputState playerInput;

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

    private MoveController m_MoveController;
    public MoveController MoveController
    {
        get
        {
            if (m_MoveController == null)
                m_MoveController = GetComponent<MoveController>();
            return m_MoveController;
        }
    }

    private void Awake()
    {
        playerInput = GameManager.Instance.InputController.State;
    }

    public void SetInputController(InputController.InputState state)
    {
        playerInput = state;
    }

    void Move()
    {
        if (playerInput == null)
        {
            playerInput = GameManager.Instance.InputController.State;
            if (playerInput == null)
                return;
        }

        Move(playerInput.Horizontal, playerInput.Vertical);
        
    }

    public void Move(float horizontal, float vertical)
    {      
        float moveSpeed = Player.Settings.runSpeed;

        if (playerInput.IsWalking)
            moveSpeed = Player.Settings.walkSpeed;

        if (playerInput.IsSprinting)
            moveSpeed = Player.Settings.sprintSpeed;

        if (playerInput.IsCrouched)
            moveSpeed = Player.Settings.crouchSpeed;

        Vector2 direction = new Vector2(vertical * moveSpeed, horizontal * moveSpeed);
        MoveController.Move(direction);
        // works
    }

    private void Update()
    {
        print("player.isLocalPlayer: " + Player.isLocalPlayer);
        if (!Player.isLocalPlayer)
            return;

        /*if (!Player.PlayerHealth.IsAlive || GameManager.Instance.IsPaused)
            return */

    }
}
