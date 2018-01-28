using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAudioController : MonoBehaviour {

	public AudioSource _thrusterSoundSource;
	private bool _playing;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		var thrusters = (TriebwerkController[]) GameObject.FindObjectsOfType(typeof(TriebwerkController));
		foreach(var t in thrusters)
		{
			if(t.On)
			{
				if(!_thrusterSoundSource.isPlaying)
					_thrusterSoundSource.Play();
				_playing = true;
				return;	
			}
		}
		if(_playing)
		{
			_thrusterSoundSource.Stop();
			_playing = false;
		}
		
	}
}
