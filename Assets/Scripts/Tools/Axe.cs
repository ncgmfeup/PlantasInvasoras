using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Axe : ToolNamespace.Tool {
    
    public Axe() {
        initializeVariables();
    }
    public override void initializeVariables() {
		
	}
	
	// Update is called once per frame
	public override void updateToolState() {
		Debug.Log("Axe Swipe");
	}
}
