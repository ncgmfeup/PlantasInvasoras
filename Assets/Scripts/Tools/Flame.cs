using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Flame : ToolNamespace.Tool {

     private float m_secondsToBurn;

    public Flame() {
        InitializeVariables();
    }

    public override void UseTool(Vector3 pos)  {
        StartCoroutine("Burn", pos);
    }
    IEnumerator Burn(Vector3 pos)
    {
        transform.position = pos + new Vector3(0, 0.5f, 0);
        yield return new WaitForSeconds(m_secondsToBurn);
        if (gameObject != null)
            Destroy(gameObject);
    }

    public override void InitializeVariables() {
        m_secondsToBurn = 1f;
	}
	
	// Update is called once per frame
	public override void UpdateToolState() {
        Debug.Log("Flame Swipe");
	}

}
