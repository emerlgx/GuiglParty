using UnityEngine;
using System.Collections;

public class JukeBox : MonoBehaviour {
	public float pitchMean = 1.2f;
	public float pitchStdDev = 0.6f;
	public float backwardsChance = 0.25f;
	public AudioClip[] musics;
	private AudioSource musicMaker;
	private AudioSource recordScratch;

	public float goalDiff;
	private float goalPitch;
	private float startPitch;
	private float clipDuration;
	private float clipTime;

	float fadeTimeLeft;

	void Awake(){
		musicMaker = GetComponent<AudioSource>();
		recordScratch = transform.FindChild("RecordScratch").GetComponent<AudioSource>();
		recordScratch.loop = false;
		recordScratch.priority = 0;
	}

	void switchSong(AudioClip nextSong){
		recordScratch.pitch = UnityEngine.Random.Range(1f, 2f);
		recordScratch.Play();
		musicMaker.Stop();
		musicMaker.clip = nextSong;
		if (UnityEngine.Random.value < backwardsChance) {
			musicMaker.pitch = -1.0f*Constants.Gauissian(pitchMean,pitchStdDev);
			goalPitch = musicMaker.pitch - goalDiff;
		} else {
			musicMaker.pitch = Constants.Gauissian (pitchMean, pitchStdDev);
			goalPitch = musicMaker.pitch + goalDiff;
		}
		startPitch = musicMaker.pitch;
		musicMaker.time = UnityEngine.Random.Range(10f, nextSong.length - 40f);
		musicMaker.Play();
	}

	public void pickSong(float _clipDuration){
		clipDuration = _clipDuration;
		clipTime = 0.0f;
		AudioClip nextSong = musics[UnityEngine.Random.Range(0, musics.Length)];
		switchSong(nextSong);
	}

	public void Update() {
		musicMaker.pitch = Mathf.Lerp (startPitch, goalPitch, clipTime / clipDuration);
		clipTime += Time.deltaTime;
		//Debug.Log (musicMaker.pitch);
	}

	public void pickBeyonce(){
		switchSong (musics [8]);
	}
}
