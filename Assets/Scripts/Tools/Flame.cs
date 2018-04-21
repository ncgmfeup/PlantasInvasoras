using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Flame : ToolNamespace.Tool {

    public Flame() {
        InitializeVariables();
    }

    public override void UseTool(Vector2 pos)  {
        throw new System.NotImplementedException();
    }

    public override void InitializeVariables() {
		
	}
	
	// Update is called once per frame
	public override void UpdateToolState() {
        Debug.Log("Flame Swipe");
	}

}
