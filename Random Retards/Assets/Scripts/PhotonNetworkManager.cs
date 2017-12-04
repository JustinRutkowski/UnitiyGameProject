using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkManager : Photon.PunBehaviour
{
    [SerializeField] private Text connectText;

	// Use this for initialization
	private void Start ()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	// Update is called once per frame
	private void Update ()
    {
        //FOR TESTING ONLY
        connectText.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
}
