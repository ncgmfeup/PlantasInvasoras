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
public class Player : MonoBehaviour {

    [SerializeField]
    private Tool[] m_playerTools;

    private int m_selectedTool = -1;

    void Start() {
        if (!StageManager.sharedInstance)
            Debug.LogWarning("No state manager found in the scene.");

        instantiateTools();

        CheckIfToolsExist();
    }

    private void instantiateTools() {
        m_playerTools = new Tool[4];
        m_playerTools[0] = new Bomb();
        m_playerTools[1] = new Axe();
        m_playerTools[2] = new Flame();
        m_playerTools[3] = new Net();

    }
    
    private void CheckIfToolsExist()  {
        if (m_playerTools.Length == 0)
            Debug.LogError("No array of tools created!");
        else
            foreach (Tool t in m_playerTools)
                if (t == null)
                    Debug.Log("One of the player's tools does not exist!");
    }

    /**
     * Useful for explosions!
     */
    public void UseTool(Vector2 position) {
        if (m_selectedTool > -1 && m_selectedTool < m_playerTools.Length && 
            m_playerTools[m_selectedTool] != null)
            m_playerTools[m_selectedTool].UseTool(position);
    }

    /**
     * Used only for certain weapons
     */
    public void UseToolOnObject(GameObject plantObject) {

        Plant plant = plantObject.GetComponent<Plant>();
        
        switch(m_selectedTool) {
            case 1: // Axe
                plant.cut();
                break;
            case 2: // Flame
                plant.burnt();
                break;
            case 3: // Net
                plant.caught();
                break;
        }
    }

    public void SelectWeapon(int newSelected) {
        m_selectedTool = newSelected;
    }

    public int GetSelectedWeapon() {
        return m_selectedTool;
    }

    
}
