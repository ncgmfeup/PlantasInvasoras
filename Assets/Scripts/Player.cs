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
    private GameObject[] m_playerTools;

    private int m_selectedTool = -1;

    void Start() {
        if (!StageManager.sharedInstance)
            Debug.LogWarning("No state manager found in the scene.");

        instantiateTools();
    }

    private void instantiateTools() {
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
    public void UseTool(Vector2 position) {
        if (m_selectedTool > -1 && m_selectedTool < m_playerTools.Length && 
            m_playerTools[m_selectedTool] != null)
            m_playerTools[m_selectedTool].GetComponent<Tool>().UseTool(position);
    }

    /**
     * Used only for certain weapons
     */
    public void UseToolOnObject(GameObject plantObject) {
        Plant plant = plantObject.GetComponent<Plant>();

        // Bomb is handled dynamically, through physics
        GameObject newTool;

        newTool = Instantiate(m_playerTools[m_selectedTool],
             plantObject.transform.position, m_playerTools[m_selectedTool].transform.rotation);
        if (m_selectedTool.Equals(Utils.AXE_SEL)) {
            plant.cut();
            newTool.GetComponent<Axe>().UseTool(plantObject.transform.position);
        } else if (m_selectedTool.Equals(Utils.FIRE_SEL)) {
            plant.burnt();
            Debug.Log("Burning");
            newTool.GetComponent<Flame>().UseTool(plantObject.transform.position);
        } if (m_selectedTool.Equals(Utils.NET_SEL)) {
            plant.caught();
            newTool.GetComponent<Net>().UseTool(plantObject.transform.position);
        }     
    }

    public void SelectWeapon(int newSelected) {
        m_selectedTool = newSelected;
    }

    public int GetSelectedWeapon() {
        return m_selectedTool;
    }

    public GameObject GetTool(int numTool) {
        return m_playerTools[numTool];
    }

    
}
