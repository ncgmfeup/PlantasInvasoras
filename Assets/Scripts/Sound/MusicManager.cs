using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : AudioSourceManager {

	public void SetHealth(float health){
		SetPitch(0.75f + health/400f);
	} 
}
