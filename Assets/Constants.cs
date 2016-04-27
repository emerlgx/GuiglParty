using UnityEngine;
using System.Collections;

public static class Constants {
	public static readonly int spriteWidthPixels = 112; //ish
	public static readonly int guiglWidth = 2;
	public static readonly Vector2 gameSize = new Vector2(1000, 1000); //unity units	
}

public enum ControlCommand { Start, Stop, /*GoFast, GoSlow*/ };

public enum PartyNames { Guigl=0, Ubaldino, Walusneaki, Blooch };