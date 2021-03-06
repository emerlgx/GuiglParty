﻿using UnityEngine;
using System.Collections;

public static class Constants {
	public static readonly int spriteWidthPixels = 112; //ish
	public static readonly int guiglWidth = 2;
	public static readonly Vector2 gameSize = new Vector2(1000, 1000); //unity units	
	public static readonly Color[] lights = new Color[5]{ 
		new Color(  2f/255, 255f/255, 247f/255, 1),
		new Color(255f/255, 252f/255,   0f/255, 1),
		new Color(246f/255, 243f/255,  16f/255, 1),
		new Color( 66f/255, 201f/255, 213f/255, 1),
		new Color(191f/255, 106f/255,  30f/255, 1)
	};
	public static readonly Color[] darks = new Color[5] {
		new Color(  7f/255, 195f/255,  20f/255, 1),
		new Color(255f/255,   5f/255,   5f/255, 1),
		new Color(152f/255,  18f/255, 155f/255, 1),
		new Color( 28f/255,  46f/255, 179f/255, 1),
		new Color(246f/255, 243f/255,  16f/255, 1)
	};

	public static void ShuffleArray<T>(T[] arr) {
		for (int i = arr.Length - 1; i > 0; i--) {
			int r = Random.Range(0, i);
			T tmp  = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}
	}

	public static float Gauissian(float mean, float stdDev) {
		float u1 = UnityEngine.Random.value;
		float u2 = UnityEngine.Random.value;
		float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
			Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
		float randNormal =
			mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
		return randNormal;
	}
}

public enum ControlCommand { Start, Stop, InSwap, DoneSwap };

public enum PartyNames { Guigl=0, Ubaldino, Walusneaki, Blooch };