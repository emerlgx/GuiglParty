using UnityEngine;
using System.Collections;

public class JukeBox : MonoBehaviour {
	public AudioClip[] musics;
	private AudioSource musicMaker;
	private AudioSource recordScratch;

	public float fadeOutTime = 2f;
	public float fadeInTime = 1f;

	private bool switchSongs = false;
	float goalVol = 1;
	float fadeTimeLeft;

	void Awake(){
		musicMaker = GetComponent<AudioSource>();
		recordScratch = transform.FindChild("RecordScratch").GetComponent<AudioSource>();
		recordScratch.loop = false;
		recordScratch.priority = 0;
	}

	public void pickSong(){
		recordScratch.pitch = UnityEngine.Random.Range(1f, 2f);
		recordScratch.Play();
		musicMaker.Stop();
		AudioClip nextSong = musics[UnityEngine.Random.Range(0, musics.Length)];
		musicMaker.clip = nextSong; 
		musicMaker.time = UnityEngine.Random.Range(10f, nextSong.length - 40f);
		musicMaker.Play();
	}
}
