using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkManager : MonoBehaviour
{
    [SerializeField] private Text connectText;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lobbycamera;
    [SerializeField] private Transform spawnpoint;

    // Use this for initialization
    private void Start ()
    {
        //connect to Photon Server
        PhotonNetwork.ConnectUsingSettings("0.1");
	}

    public virtual void OnConnectedToMaster()
    {
        //Message for Joining the Master
        Debug.Log("We joined Master");
    }

    public virtual void OnJoinedLobby()
    {
        //Message for Joining the Lobby
        Debug.Log("We joined Lobby");
        //Join existing room or Create new room
        PhotonNetwork.JoinOrCreateRoom("NEW", null, null);
    }

    public virtual void OnJoinedRoom()
    {
        //Spawn in the Player
        PhotonNetwork.Instantiate(player.name, spawnpoint.position, spawnpoint.rotation, 0);
        //Deactivate Lobby Camera
        lobbycamera.SetActive(false);
    }

    // Update is called once per frame
    private void Update ()
    {
        //FOR TESTING ONLY
        connectText.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
}
