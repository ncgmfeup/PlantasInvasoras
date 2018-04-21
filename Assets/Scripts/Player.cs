using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolNamespace;
using StateNamespace;

/// <summary>
/// Defines the player in the game. Contains all the tools the player has access to, and also creates the gameplay effects in the scene.
/// </summary>
public abstract class Player : MonoBehaviour {

    [SerializeField]
    private Tool[] m_playerTools;

    private int m_selectedTool = -1;
    public int SelectedTool
    {
        get { return m_selectedTool; }
        set { if(value < 4 || value > -1) m_selectedTool = value; }
    }

    void Start()
    {
        if (!StageManager.sharedInstance)
            Debug.LogWarning("No state manager found in the scene.");

        CheckIfToolsExist();
    }
    
    private void CheckIfToolsExist()
    {
        if (m_playerTools.Length == 0)
            Debug.LogError("No array of tools created!");
        else
            foreach (Tool t in m_playerTools)
                if (t == null)
                    Debug.Log("One of the player's tools does not exist!");
    }

    public void UseTool()
    {
        if (m_selectedTool > -1 && m_selectedTool < m_playerTools.Length && m_playerTools[m_selectedTool] != null)
            m_playerTools[m_selectedTool].UseTool();
    }

    
}
