using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThridPersonCamera : MonoBehaviour {

    [System.Serializable]
    public class CameraRig
    {
        public Vector3 cameraOffset;
        public float crouchHeight;
        public float damping;
    }

    // 0,5 0.04 -3
    
    [SerializeField] CameraRig defaultCamera;
    [SerializeField] CameraRig aimCamera;
    [SerializeField] CameraRig leanLeftCamera;
    [SerializeField] CameraRig leanRightCamera;
    [SerializeField] CameraRig leanLeftAimCamera;
    [SerializeField] CameraRig leanRightAimCamera;

    private PlayerScript m_Player;
    public PlayerScript Player
    {
        get
        {
            if (m_Player == null)
                m_Player = GameManager.Instance.LocalPlayer;
            return m_Player;
        }
    }

    private PlayerAim m_PlayerAim;
    private PlayerAim PlayerAim
    {
        get
        {
            if (m_PlayerAim == null)                
                    m_PlayerAim = GameManager.Instance.LocalPlayer.playerAim;
            return m_PlayerAim;
        }
    }

    Transform cameraLookTarget;
	PlayerScript localPlayer;
	void Awake () {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
	}
	
    void HandleOnLocalPlayerJoined (PlayerScript player) {
         
        localPlayer = player;
        cameraLookTarget = localPlayer.transform.Find("AimingPivot");

        if (cameraLookTarget == null) {
            cameraLookTarget = localPlayer.transform;
        }
        
    }

    // Update is called once per frame
    void Update () {

        if (localPlayer == null)
            return;

        
        
        CameraRig cameraRig = defaultCamera;

        if (localPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMING || localPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMEDFIRING)
            cameraRig = aimCamera;

        if (Player.Inputstate.IsLeaningLeft)
            cameraRig = leanLeftCamera;

        if (Player.Inputstate.IsLeaningRight)
            cameraRig = leanRightCamera;

        if ((localPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMING || localPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMEDFIRING) && Player.Inputstate.IsLeaningLeft)
            cameraRig = leanLeftAimCamera;

        if ((localPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMING || localPlayer.playerState.weaponState == PlayerState.EWeaponState.AIMEDFIRING) && Player.Inputstate.IsLeaningRight)
            cameraRig = leanRightAimCamera;

        float targetHeight = cameraRig.cameraOffset.y + (localPlayer.playerState.moveState == PlayerState.EMoveState.CROUCHING ? cameraRig.crouchHeight : 0);

        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * (cameraRig.cameraOffset.z + Mathf.Abs(cameraRig.cameraOffset.y * PlayerAim.GetAngle() * (float)0.6)) +
        localPlayer.transform.up * (cameraRig.cameraOffset.y * PlayerAim.GetAngle() + targetHeight) +
        localPlayer.transform.right * cameraRig.cameraOffset.x;

        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraRig.damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraLookTarget.rotation, cameraRig.damping * Time.deltaTime);
        
	}
}
