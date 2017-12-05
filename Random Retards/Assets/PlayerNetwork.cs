using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private MonoBehaviour[] playerControlScripts;

    private PhotonView photonview;

    private void Start()
    {
        photonview = GetComponent<PhotonView>();

        initialize();
    }

    private void initialize()
    {
        if (photonview.isMine)
        {

        }
        //handle functionality for non local player
        else
        {
            //disable its camera
            playerCamera.SetActive(false);
            //disable its control scripts
            foreach (MonoBehaviour m in playerControlScripts)
            {
                m.enabled = false;
            }
        }
    }
}
