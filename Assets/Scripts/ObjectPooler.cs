using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Object Pooling/Plant Object Pool")]
public class PlantObjectPooler : MonoBehaviour
{
    //The singleton instance of this object (not more than one in the scene.)
    public static PlantObjectPooler sharedInstance;

    public List<GameObject> m_nativePlantObjects, m_invadingPlantObjects;
    public GameObject m_nativePlantPrefab, m_invadingPlantPrefab;
    public int m_nativePlantsToInstantiate, m_invadingPlantsToInstantiate;
        
    void Awake()
    {
        if(!sharedInstance)
            sharedInstance = this;
        else
        {
            Debug.LogWarning("Found another object pooler in the scene. Destroying the instance on gameObj: " + gameObject.name);
            Destroy(this);
        }

        if (!m_nativePlantPrefab)
            Debug.LogError("No native plant to instantiate!");
        else if (m_nativePlantsToInstantiate == 0)
            Debug.LogWarning("Number of native plants to instantiate is 0!");
        else
            SpawnPlants(m_nativePlantPrefab, m_nativePlantsToInstantiate, ref m_nativePlantObjects);

        if (!m_invadingPlantPrefab)
            Debug.LogError("No invating plant to instantiate!");
        else if (m_invadingPlantsToInstantiate == 0)
            Debug.LogWarning("Number of invading plants to instantiate is 0!");
        else
            SpawnPlants(m_invadingPlantPrefab, m_invadingPlantsToInstantiate, ref m_invadingPlantObjects);
    }

    private void SpawnPlants(GameObject plantPrefab, int numberOfPlantsToInstantiate, ref List<GameObject> plantList)
    {
        for (int i = 0; i < numberOfPlantsToInstantiate; i++)
        {
            GameObject currentPlant = Instantiate(plantPrefab);
            currentPlant.SetActive(false);
            plantList.Add(currentPlant);
        }
    }
        
    public GameObject GetNativePlant()
    {
        foreach(GameObject nativePlant in m_nativePlantObjects)
        {
            if (!nativePlant.activeInHierarchy)
                return nativePlant;
        }
        return null;
    }

    public GameObject SpawnNativePlantAtPosition(Vector2 plantPos)
    {
        GameObject nativePlant = GetNativePlant();
        if (nativePlant)
            nativePlant.transform.position = plantPos;
        return nativePlant;
    }

    public GameObject GetInvadingPlant()
    {
        foreach (GameObject invadingPlant in m_invadingPlantObjects)
        {
            if (!invadingPlant.activeInHierarchy)
                return invadingPlant;
        }
        return null;
    }

    public GameObject SpawnInvadingPlantAtPosition(Vector2 plantPos)
    {
        GameObject invadingPlant = GetInvadingPlant();
        if (invadingPlant)
            invadingPlant.transform.position = plantPos;
        return invadingPlant;
    }

}
