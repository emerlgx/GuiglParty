﻿using UnityEngine;
using System.Collections;

public class Constants {
	private static int spriteWidthPixels = 112; //ish
	public static int guigleWidth = 1;
	public static Vector2 gameSize = {1000, 1000}; //unity units	
}

enum ControlCommand { Start, Stop, /*GoFast, GoSlow*/ };