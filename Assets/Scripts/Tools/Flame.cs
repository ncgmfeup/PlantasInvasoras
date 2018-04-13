using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Flame : ToolNamespace.Tool {

    public Flame() {
        initializeVariables();
    }

    public override void initializeVariables() {
		
	}
	
	// Update is called once per frame
	public override void updateToolState() {
        Debug.Log("Flame Swipe");
	}

}
