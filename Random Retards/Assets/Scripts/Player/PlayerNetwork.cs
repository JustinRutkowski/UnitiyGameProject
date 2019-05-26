using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerScript))]
public class PlayerNetwork : NetworkBehaviour
{


    PlayerScript player;
    PlayerMove playerMove;
    PlayerAnimation playerAnimation;
    NetworkState state;
    NetworkState lastSendState;
    NetworkState lastReceivedState;
    NetworkState lastSendRpcState;

    [System.Serializable]
    public partial class NetworkState : InputController.InputState
    {
        public float PositionX;
        public float PositionY;
        public float PositionZ;
        public float RotationangleY;
        public float Timestamp;
    }

    void Start()
    {
        player = GetComponent<PlayerScript>();
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();      
        state = new NetworkState();

        if (isLocalPlayer)
            player.SetAsLocalPlayer();

    }

    private NetworkState CollectInput()
    {
        state = new NetworkState
        {
            Fire1 = GameManager.Instance.InputController.Fire1,
            Fire2 = GameManager.Instance.InputController.Fire2,
            Horizontal = GameManager.Instance.InputController.Horizontal,
            Vertical = GameManager.Instance.InputController.Vertical,
            AimAngle = GameManager.Instance.InputController.AimAngle,
            IsWalking = GameManager.Instance.InputController.IsWalking,
            IsCrouched = GameManager.Instance.InputController.IsCrouched,
            IsSprinting = GameManager.Instance.InputController.IsSprinting,
            IsReloading = GameManager.Instance.InputController.IsReloading,
            IsJumping = GameManager.Instance.InputController.IsJumping,
            IsProneing = GameManager.Instance.InputController.IsProneing,
            IsAiming = GameManager.Instance.InputController.IsAiming,
            IsLeaningRight = GameManager.Instance.InputController.IsLeaningRight,
            IsLeaningLeft = GameManager.Instance.InputController.IsLeaningLeft,
            IsFiring = GameManager.Instance.InputController.IsFiring,
            RotationangleY = transform.rotation.eulerAngles.y,
            Timestamp = Time.time
        };
        return state;
    }

    void Update()
    {
        print("Network.isLocalPlayer: " + isLocalPlayer);
        if (isLocalPlayer)
        {
            state = CollectInput();
            playerMove.SetInputController(state);
            playerMove.Move(state.Horizontal, state.Vertical);
        }

        UpdateState();

        if (lastReceivedState == null)
            return;

       
    }

    void UpdateState()
    {
        playerAnimation.Vertical = lastReceivedState.Vertical;
        playerAnimation.Horizontal = lastReceivedState.Horizontal;
        playerAnimation.AimAngle = lastReceivedState.AimAngle;
        playerAnimation.isWalking = lastReceivedState.IsWalking;
        playerAnimation.isSprinting = lastReceivedState.IsSprinting;
        playerAnimation.isCrouched = lastReceivedState.IsCrouched;      
        playerAnimation.IsLeaningLeft = lastReceivedState.IsLeaningLeft;
        playerAnimation.IsLeaningRight = lastReceivedState.IsLeaningRight;
        playerAnimation.isFiring = lastReceivedState.IsFiring;
        playerAnimation.isAiming = lastReceivedState.IsAiming;
        playerAnimation.isJumping = lastReceivedState.IsJumping;
        playerAnimation.isReloading = lastReceivedState.IsReloading;

        if (!isLocalPlayer)
        {
            Vector3 solution = new Vector3(lastReceivedState.PositionX, lastReceivedState.PositionY, lastReceivedState.PositionZ);

            playerMove.SetInputController(lastReceivedState);
            player.SetInputState(lastReceivedState);

            transform.rotation = Quaternion.Euler(transform.transform.rotation.eulerAngles.x, lastReceivedState.RotationangleY, transform.transform.rotation.eulerAngles.z);
            playerMove.Move(lastReceivedState.Horizontal, lastReceivedState.Vertical);

            if (!isServer)
            {
                float positionDifferenceFromServer = Vector3.Distance(transform.position, solution);

                if (positionDifferenceFromServer > .3f)
                    transform.position = Vector3.Lerp(transform.position, solution, player.Settings.runSpeed * Time.deltaTime);
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            if (isInputStateDirty(state, lastSendState))
            {
                lastSendState = state;
                Cmd_HandleInput(SerializeState(lastSendState));
            }
        }

        if(isServer && lastReceivedState != null)
        {
            NetworkState stateSolution = new NetworkState
            {
                PositionX = transform.position.x,
                PositionY = transform.position.y,
                PositionZ = transform.position.z,
                Horizontal = lastReceivedState.Horizontal,
                Vertical = lastReceivedState.Vertical,
                AimAngle = lastReceivedState.AimAngle,
                Fire1 = lastReceivedState.Fire1,
                Fire2 = lastReceivedState.Fire2,
                IsAiming = lastReceivedState.IsAiming,
                IsCrouched = lastReceivedState.IsCrouched,
                IsSprinting = lastReceivedState.IsSprinting,
                IsWalking = lastReceivedState.IsWalking,
                IsLeaningLeft = lastReceivedState.IsLeaningLeft,
                IsLeaningRight = lastReceivedState.IsLeaningRight,
                IsFiring = lastReceivedState.IsFiring,
                IsJumping = lastReceivedState.IsJumping,
                IsProneing = lastReceivedState.IsProneing,
                IsReloading = lastReceivedState.IsReloading,
                RotationangleY = lastReceivedState.RotationangleY,
                Timestamp = lastReceivedState.Timestamp
            };

            if (isInputStateDirty(stateSolution, lastSendRpcState))
            {
                lastSendRpcState = stateSolution;
                Rpc_HandleStateSolution(SerializeState(lastSendRpcState));
            }
        }
    }

    [Command]
    void Cmd_HandleInput(byte[] data)
    {
        lastReceivedState = DeseializeState(data);
    }

    [ClientRpc]
    void Rpc_HandleStateSolution(byte[] data)
    {
        lastReceivedState = DeseializeState(data);
    }

    bool isInputStateDirty(NetworkState a, NetworkState b)
    {
        if (b == null)
            return true;

        return a.AimAngle != b.AimAngle ||
            a.Fire1 != b.Fire1 ||
            a.Fire2 != b.Fire2 ||
            a.Horizontal != b.Horizontal ||
            a.Vertical != b.Vertical ||
            a.IsAiming != b.IsAiming ||
            a.IsWalking != b.IsWalking ||
            a.IsSprinting != b.IsSprinting ||
            a.IsCrouched != b.IsCrouched ||
            a.IsLeaningLeft != b.IsLeaningLeft ||
            a.IsLeaningRight != b.IsLeaningRight ||
            a.IsFiring != b.IsFiring ||
            a.IsReloading != b.IsReloading ||
            a.IsJumping != b.IsJumping ||
            a.IsProneing != b.IsProneing ||
            a.RotationangleY != b.RotationangleY;
    }

    private BinaryFormatter bf = new BinaryFormatter();

    private byte[] SerializeState(NetworkState state)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            bf.Serialize(stream, state);
            return stream.ToArray();
        }
    }

    private NetworkState DeseializeState(byte[] bytes)
    {
        using (MemoryStream stream = new MemoryStream(bytes))
            return (NetworkState)bf.Deserialize(stream);
    }

}
