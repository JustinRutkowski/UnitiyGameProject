using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour {

	public void Despawm(GameObject go, float inSeconds)
    {
        go.SetActive(false);
        GameManager.Instance.Timer.Add(() => {
            go.SetActive(true);
        }, inSeconds);
    }

    
}
