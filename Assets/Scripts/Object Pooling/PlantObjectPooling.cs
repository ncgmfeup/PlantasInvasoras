using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Object Pooling/Plant Object Pool")]
public class PlantObjectPooler : MonoBehaviour
{
    /// <summary>
    /// The instance to be used by all 
    /// </summary>
    public static PlantObjectPooler sharedInstance;

    public ObjectPooling.ObjectPool m_invadingPlantsPool, m_nativePlantsPool;
        
    void Awake()
    {
        if(!sharedInstance)
            sharedInstance = this;
        else
        {
            Debug.LogWarning("Found another object pooler in the scene. Destroying the instance on gameObj: " + gameObject.name);
            Destroy(this);
        }

        if (!m_nativePlantsPool)
            Debug.LogError("No native plant pool created!");
        else
            m_nativePlantsPool.GenerateObjects();

        if (!m_invadingPlantsPool)
            Debug.LogError("No invating plant to instantiate!");
        else
            m_invadingPlantsPool.GenerateObjects();
    }
        
    public GameObject GetNativePlant()
    {
        return m_nativePlantsPool.GetObjectFromPool();
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
        return m_invadingPlantsPool.GetObjectFromPool();
    }

    public GameObject SpawnInvadingPlantAtPosition(Vector2 plantPos)
    {
        GameObject invadingPlant = GetInvadingPlant();
        if (invadingPlant)
            invadingPlant.transform.position = plantPos;
        return invadingPlant;
    }

}
