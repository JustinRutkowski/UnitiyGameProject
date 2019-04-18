using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    [SerializeField] AudioClip[] clips;
    [SerializeField] float delayBetweenClips;

    bool canPlay;
    AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        canPlay = true;
	}
	
	
	public void PLay()
    {
        if (!canPlay)
            return;

        GameManager.Instance.Timer.Add(() => {
            canPlay = true;
        }, delayBetweenClips);

        canPlay = false;

        int clipIndex = Random.Range(0, clips.Length);
        AudioClip clip = clips[clipIndex];
        source.PlayOneShot(clip);
    }
}
