using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(PlayerState))]
public class PlayerScript : MonoBehaviour {

	[System.Serializable]
	public class MouseInput {
		public Vector2 Damping;
		public Vector2 Sensitivity;
        public bool LockMouse;
	}

	[SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float sprintSpeed;  
	[SerializeField] MouseInput MouseControl;
    [SerializeField] AudioController footSteps;
    [SerializeField] float minimalMoveTreshhold;

    Vector3 previousPosition;

    public PlayerAim playerAim;

	private MoveController m_moveController;
	public MoveController moveController {
		get {
			if (m_moveController == null) {
				m_moveController = GetComponent<MoveController>();
			}
			return m_moveController;
		}
	}

    private PlayerState m_playerState;
    public PlayerState playerState
    {
        get
        {
            if (m_playerState == null)
                m_playerState = GetComponentInChildren<PlayerState>();
            return m_playerState;
        }
    }

    InputController playerInput;
    Vector2 mouseInput;

	void Awake () {
		playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;

        if (MouseControl.LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        Move();
        LookAround();
    }

    void Move()
    {
        float moveSpeed = runSpeed;

        if (playerInput.IsWalking)
            moveSpeed = walkSpeed;

        if (playerInput.IsSprinting)
            moveSpeed = sprintSpeed;

        if (playerInput.IsCrouched)
            moveSpeed = crouchSpeed;

        Vector2 direction = new Vector2(playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);
        moveController.Move(direction);

        if (Vector3.Distance(transform.position, previousPosition) > minimalMoveTreshhold)
            //footSteps.PLay();

         previousPosition = transform.position;
        
    }

    void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);
        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x); 
        playerAim.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }
}
