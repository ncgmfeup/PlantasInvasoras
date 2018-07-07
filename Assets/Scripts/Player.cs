using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolNamespace;
using StateNamespace;
using PlantNamespace;

/// <summary>
/// Defines the player in the game. Contains all the tools the player has access to, 
/// and also creates the gameplay effects in the scene.
/// </summary>
public class Player : MonoBehaviour
{

  public enum ToolType { None, Bomb, Axe, Fire, Net }

  public ToolType tool = ToolType.None;
  private GameObject tool_obj;

  public GameObject[] tools;

  [SerializeField]
  private GameObject[] m_playerTools;

  [SerializeField]
  private int m_selectedTool = -1;

  void Start()
  {
    if (!StageManager.sharedInstance)
      Debug.LogWarning("No state manager found in the scene.");
  }



  public int GetSelectedWeapon()
  {
    return m_selectedTool;
  }

  public GameObject GetTool(int numTool)
  {
    return m_playerTools[numTool];
  }

  //Select a new tool.
  public void SelectWeapon(int tool_id)
  {
    if (tool != ToolType.None)
      return;
    tool = (ToolType)tool_id;
    switch (tool)
    {
      case ToolType.Bomb:
        tool_obj = Instantiate(tools[0], Vector3.zero, Quaternion.identity);
        break;
      case ToolType.Axe:
        tool_obj = Instantiate(tools[1], Vector3.zero, Quaternion.identity);
        break;
      case ToolType.Fire:
        tool_obj = Instantiate(tools[2], Vector3.zero, Quaternion.identity);
        break;
      case ToolType.Net:
        tool_obj = Instantiate(tools[3], Vector3.zero, Quaternion.identity);
        break;
      default:
        ResetTool();
        break;
    }
    Debug.Log("Tool:" + tool.ToString());
  }

  //Sets the position of tool_obj
  public void UpdateToolPosition(Vector2 position)
  {
    //Debug.Log("Updating Tool Position");
    if (tool_obj != null)
      tool_obj.transform.position = new Vector3(position.x, position.y, tool_obj.transform.position.z);
  }

  //Deletes current object and sets tool to ToolType.None
  public void ResetTool()
  {
    if (tool_obj != null)
    {
      //Debug.Log("Destroy Tool");
      Destroy(tool_obj);
    }
    tool_obj = null;
    tool = ToolType.None;
  }

	//Calls use tool on position. Warning!! Doesn't deletes the object.
	public void UseTool(Vector2 position)
  {
		tool_obj.GetComponent<Tool>().UseTool(position);
		tool_obj = null;
    tool = ToolType.None;
  }

}
