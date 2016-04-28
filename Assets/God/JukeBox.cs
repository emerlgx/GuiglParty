using UnityEngine;
using System.Collections;

public class JukeBox : MonoBehaviour {
	public AudioClip[] musics;
	private AudioSource musicMaker;
	private AudioSource recordScratch;

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
		musicMaker.time = UnityEngine.Random.Range(10f, nextSong.length - 40f);
		musicMaker.Play();
	}

	public void pickSong(){
		AudioClip nextSong = musics[UnityEngine.Random.Range(0, musics.Length)];
		switchSong(nextSong);
	}

	public void pickBeyonce(){
		switchSong (musics [8]);
	}
}
