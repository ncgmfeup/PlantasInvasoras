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

    instantiateTools();
  }

  private void instantiateTools()
  {
    /*m_playerTools[0]
    m_playerTools[0] = new Bomb();
    m_playerTools[1] = new Axe();
    m_playerTools[2] = new Flame();
    m_playerTools[3] = new Net();
*/
  }

  /**
   * Useful for explosions!
   */
  public void UseTool2(Vector2 position)
  {
    if (m_selectedTool > -1 && m_selectedTool < m_playerTools.Length &&
        m_playerTools[m_selectedTool] != null)
      m_playerTools[m_selectedTool].GetComponent<Tool>().UseTool(position);
  }

  /**
   * Used only for certain weapons
   */
  public void UseToolOnObject(GameObject plantObject)
  {
    Plant plant = plantObject.GetComponent<Plant>();

    // Bomb is handled dynamically, through physics
    GameObject newTool;

    newTool = Instantiate(m_playerTools[m_selectedTool],
         plantObject.transform.position, m_playerTools[m_selectedTool].transform.rotation);
    if (m_selectedTool.Equals(Utils.AXE_SEL))
    {
      plant.cut();
      newTool.GetComponent<Axe>().UseTool(plantObject.transform.position);
    }
    else if (m_selectedTool.Equals(Utils.FIRE_SEL))
    {
      plant.burnt();
      Debug.Log("Burning");
      newTool.GetComponent<Flame>().UseTool(plantObject.transform.position);
    }
    if (m_selectedTool.Equals(Utils.NET_SEL))
    {
      plant.caught();
      newTool.GetComponent<Net>().UseTool(plantObject.transform.position);
    }
  }

  public void SelectWeapon2(int newSelected)
  {
    m_selectedTool = newSelected;
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
