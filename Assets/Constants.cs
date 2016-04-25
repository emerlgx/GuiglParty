using UnityEngine;
using System.Collections;

public class Constants {
	private static int spriteWidthPixels = 112; //ish
	public static int guiglWidth = 1;
	public static Vector2 gameSize = new Vector2(1000, 1000); //unity units	
}

public enum ControlCommand { Start, Stop, /*GoFast, GoSlow*/ };

public enum PartyNames { Guigl, Ubaldino, Walusneaki, Blooch };