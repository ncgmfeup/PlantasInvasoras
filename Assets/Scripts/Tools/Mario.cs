using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mario : ToolNamespace.Tool {

	public Mario() {
		initializeVariables();
	}

    public override void initializeVariables() {
		
	}
	
	// Update is called once per frame
	public override void updateToolState() {
        Debug.Log("Mario Swipe");
	}
}
