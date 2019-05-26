using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerScript : MonoBehaviour {

	[System.Serializable]
	public class MouseInput {
		public Vector2 Damping;
		public Vector2 Sensitivity;
        public bool LockMouse;
	}
	
    public SwatSoilder Settings;  

	[SerializeField] MouseInput MouseControl;
    [SerializeField] AudioController footSteps;
    [SerializeField] float minimalMoveTreshhold;

    [SerializeField] float slopeRayHeight;
    [SerializeField] float steepSlopeAngle; 
    [SerializeField] float slopeThreshold;

    Vector3 desiredVelocity = new Vector3(0,0,0);

    Rigidbody rigidbody;
    
    public PlayerAim playerAim;
    public bool isLocalPlayer;

    private PlayerShoot m_PlayerShoot;
    public PlayerShoot PlayerShoot
    {
        get
        {
            if (m_PlayerShoot == null)
                m_PlayerShoot = GetComponent<PlayerShoot>();
            return m_PlayerShoot;
        }
    }

	private InputController.InputState m_InputState;
	public InputController.InputState Inputstate {
		get {
			if (m_InputState == null) {
                m_InputState = GameManager.Instance.InputController.State;
			}
			return m_InputState;
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

    private PlayerHealth m_PlayerHealth;
    public PlayerHealth PlayerHealth
    {
        get
        {
            if (m_PlayerHealth == null)
            {
                m_PlayerHealth = GetComponent<PlayerHealth>();
            }
            return m_PlayerHealth;
        }
    }

    InputController playerInput;
    Vector2 mouseInput;
    Vector3 previousPosition;

    void Awake () {
        
        if (!GameManager.Instance.IsNetworkGame)
        {
            SetAsLocalPlayer();        
            rigidbody = this.GetComponent<Rigidbody>();
            desiredVelocity.Set(desiredVelocity.x, rigidbody.velocity.y, desiredVelocity.z);
        }     
    }

    public void SetInputState(InputController.InputState state)
    {
        m_InputState = state;
    }

    public void SetAsLocalPlayer()
    {
        isLocalPlayer = true;
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;

        if (MouseControl.LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
		
	void Update () {

        if (isLocalPlayer)
        {                 
            LookAround();

           if (checkMoveableTerrain(transform.position, new Vector3(desiredVelocity.x, 0, desiredVelocity.z), 10f)) // filter the y out, so it only checks forward... could get messy with the cosine otherwise.
            {
                rigidbody.velocity = desiredVelocity;
            }
        }
            
    }

    void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);
        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x); 
        playerAim.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }

    // Go up Slopes
    bool checkMoveableTerrain(Vector3 position, Vector3 desiredDirection, float distance)
    {
        Ray myRay = new Ray(position, desiredDirection); // cast a Ray from the position of our gameObject into our desired direction. Add the slopeRayHeight to the Y parameter.
        RaycastHit hit;

        if (Physics.Raycast(myRay, out hit, distance))
        {           
            if (hit.collider.gameObject.tag == "Slope") // Our Ray has hit the ground
            {         
                float slopeAngle = Mathf.Deg2Rad * Vector3.Angle(Vector3.up, hit.normal); // Here we get the angle between the Up Vector and the normal of the wall we are checking against: 90 for straight up walls, 0 for flat ground.
                float radius = Mathf.Abs(slopeRayHeight / Mathf.Sin(slopeAngle)); // slopeRayHeight is the Y offset from the ground you wish to cast your ray from.

                if (slopeAngle >= steepSlopeAngle * Mathf.Deg2Rad) // You can set "steepSlopeAngle" to any angle you wish.
                {
                    if (hit.distance - 0.3 > Mathf.Abs(Mathf.Cos(slopeAngle) * radius) + slopeThreshold) // Magical Cosine. This is how we find out how near we are to the slope / if we are standing on the slope. as we are casting from the center of the collider we have to remove the collider radius.                                                                                                // The slopeThreshold helps kills some bugs. ( e.g. cosine being 0 at 90° walls) 0.01 was a good number for me here
                    {                       
                        return true; // return true if we are still far away from the slope
                    }
                    return false; // return false if we are very near / on the slope && the slope is steep
                }
                return true; // return true if the slope is not steep
            }         
        }
        return false;
    }
}
