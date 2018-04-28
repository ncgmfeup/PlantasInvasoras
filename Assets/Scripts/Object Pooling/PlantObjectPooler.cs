using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;

/// <summary>
/// Holds 2 object poolers for use with plant objects.
/// </summary>
[AddComponentMenu("Object Pooling/Plant Object Pool")]
public class PlantObjectPooler : MonoBehaviour
{
    /// <summary>
    /// The instance to be used by all external objects. (singleton shared instance)
    /// </summary>
    public static PlantObjectPooler sharedInstance;

    [SerializeField]
    private ObjectPool m_invadingPlantsPool, m_nativePlantsPool;

    void Awake()
    {
        if(!sharedInstance) {
            sharedInstance = this;
        }
        else 
        {
            Debug.LogWarning("Found another plant object pooler in the scene. " +
            "Destroying the instance on gameObj: " + gameObject.name);
            Destroy(this);
        }

        if (m_nativePlantsPool == null)
            Debug.LogError("No native plant pool created!");
        else
            m_nativePlantsPool.GenerateObjects();

        if (m_invadingPlantsPool == null)
            Debug.LogError("No invading plant to instantiate!");
        else
            m_invadingPlantsPool.GenerateObjects();
    }

    public int GetNumberOfActiveInvadingPlants()
    {
        return m_invadingPlantsPool.ActiveObjects;
    }
    
    public GameObject GetNativePlant()
    {
        return m_nativePlantsPool.GetObjectFromPool();
    }

    public GameObject GetInvadingPlant()
    {
        return m_invadingPlantsPool.GetObjectFromPool();
    }

    public GameObject SpawnNativePlantAtPosition(Vector3 plantPos)
    {
        GameObject nativePlant = GetNativePlant();
        if (nativePlant)
        {
            nativePlant.SetActive(true);
            nativePlant.transform.position = plantPos;
        }
        return nativePlant;
    }

    public GameObject SpawnInvadingPlantAtPosition(Vector3 plantPos)  {
        GameObject invadingPlant = GetInvadingPlant();
        if (invadingPlant)
        {
            invadingPlant.SetActive(true);
            invadingPlant.transform.position = plantPos;
        }
        return invadingPlant;
    }

}
