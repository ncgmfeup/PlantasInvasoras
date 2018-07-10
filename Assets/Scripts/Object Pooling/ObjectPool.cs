using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    /// <summary>
    /// A gameObject pool. Can be used to hold multiple inactive gameObjects at a time. 
    ///Mainly used for optimizing gameplay (reduces the number of instantiate and destroy calls.)
    /// </summary>
    [Serializable]
    public class ObjectPool
    {
        /// <summary>
        /// The GameObject to spawn into the pool
        /// </summary>
        public GameObject m_gameObjectPrefab;

        /// <summary>
        /// The size of the object pool (how many will be spawned initially)
        /// </summary>
        public int m_objectPoolSize;

        /// <summary>
        /// The list that holds the object pool.
        /// </summary>
        private List<GameObject> m_gameObjectPool = new List<GameObject>();

        /// <summary>
        /// How many objects from the pool are active.
        /// </summary>
        [SerializeField]
        private int m_activeObjects = 0;
        
        /// <summary>
        /// Checks how many objects from the pool are currently active
        /// </summary>
        public int ActiveObjects { get { return m_activeObjects; } }

        /// <summary>
        /// Increases the number of active objects in the pool by one.
        /// </summary>
        public void ActivateObject()
        {
            m_activeObjects++;
        }

        /// <summary>
        /// Decreases the number of active objects in the pool by one.
        /// </summary>
        public void DeactivateObject()
        {
            m_activeObjects--;
        }
        
        /// <summary>
        /// Returns an object from this pool.
        /// </summary>
        /// <returns>The first inactive object.</returns>
        public GameObject GetObjectFromPool()
        {
            if (m_gameObjectPool.Count == 0)
                Debug.LogError("This object pool is empty.");

            foreach(GameObject gameObj in m_gameObjectPool)
            {
                if (!gameObj.activeInHierarchy)
                {
                    return gameObj;
                }
            }
            return null;
        }

        /// <summary>
        /// Create the objects for this pool.
        /// </summary>
        public void GenerateObjects()
        {
            if(!m_gameObjectPrefab)
            {
                Debug.LogError("No game object prefab assigned to this object pool component.");
                return;
            }
            else if(m_objectPoolSize <= 0)
            {
                Debug.LogError("Invalid object pool size. Will not generate.");
                return;
            }
            else if(!m_gameObjectPrefab.GetComponent<PoolableObject>())
            {
                Debug.LogError("The prefab has no Poolable Object component!");
                return;
            }

            for (int i = 0; i < m_objectPoolSize; i++)
            {
                GameObject currentObject = UnityEngine.Object.Instantiate(m_gameObjectPrefab);
                currentObject.SetActive(false);
                currentObject.GetComponent<PoolableObject>().m_sourcePool = this;
                m_gameObjectPool.Add(currentObject);
                //Debug.Log("Added obj " + currentObject.name + " to the pool.");
            }
        }
    }
}

