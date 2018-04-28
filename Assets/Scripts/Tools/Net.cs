using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : ToolNamespace.Tool {

	private float m_secondsToLift;

	public Net()   {	
		InitializeVariables();
	}


    public override void InitializeVariables() {
		m_secondsToLift = 2;	
		
	}

    public override void UseTool(Vector3 pos)  {
		StartCoroutine("ExtractFromWater", pos);
    }

	IEnumerator ExtractFromWater(Vector3 pos) {
		Debug.Log("Extracting from water");

		Vector3 startPos = pos - new Vector3(0,2,0);
		Vector3 finalPos = pos + new Vector3(0,2,0);
		
		float elapsedTime = 0;

		while (elapsedTime < m_secondsToLift) 	{
			transform.position = Vector3.Lerp(startPos, finalPos, 
				Easing.Back.InOut(elapsedTime / m_secondsToLift));
			elapsedTime += Time.deltaTime;
        	yield return new WaitForEndOfFrame();
      	}

		yield return null;
	}

	
	// Update is called once per frame
	public override void UpdateToolState() {
        Debug.Log("Net Swipe");
	}
}
