using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fire : ToolNamespace.Tool
{

  public Fire()
  {
    InitializeVariables();
  }

  public override void InitializeVariables()
  {
  }

  public override void UseTool(Vector3 pos)
  {
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
    if (hit)
    	hit.collider.gameObject.SendMessage("burnt", null, SendMessageOptions.DontRequireReceiver);
  
  }

  // Update is called once per frame
  public override void UpdateToolState()
  {
    Debug.Log("Flame Swipe");
  }

  void FixedUpdate()
  {
    UseTool(transform.position);
  }

}
