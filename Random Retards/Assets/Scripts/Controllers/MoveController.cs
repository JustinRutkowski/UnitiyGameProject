using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    bool canJump;

	public void Move(Vector2 direction) {
		transform.position += transform.forward * direction.x * Time.deltaTime +
			transform.right * direction.y * Time.deltaTime;
	}

    void Start()
    {
        canJump = true;
    }

    public void Jump()
    {
        if (!canJump)
            return;

        GameManager.Instance.Timer.Add(() => {
            canJump = true;
        }, 1);

        canJump = false;

        
    }

}
