using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{

    /// <summary>
    /// A gameObject pool. Can be used to hold multiple inactive gameObjects at a time. Mainly used for optimizing gameplay (reduces the number of instantiate and destroy calls.)
    /// </summary>
    public class ObjectPool : MonoBehaviour
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
        private List<GameObject> m_gameObjectPool;
        
        /// <summary>
        /// Returns an object from this pool.
        /// </summary>
        /// <returns>The first inactive object.</returns>
        public GameObject GetObjectFromPool()
        {
            if (m_gameObjectPool.Count == 0)
                Debug.LogError("This object pool is empty.");

            foreach(GameObject gameObj in m_gameObjectPool)
                if(!gameObj.activeInHierarchy)
                    return gameObj;
            return null;
        }

        /// <summary>
        /// Create the objects for this pool.
        /// </summary>
        public void GenerateObjects()
        {
            if(!m_gameObjectPrefab)
            {
                throw new System.NullReferenceException("No game object prefab assigned to this object pool component.");
            }
            else if(m_objectPoolSize <= 0)
            {
                throw new System.ArgumentException("Invalid object pool size. Will not generate.");
            }

            for (int i = 0; i < m_objectPoolSize; i++)
            {
                GameObject currentObject = Instantiate(m_gameObjectPrefab);
                currentObject.SetActive(false);
                m_gameObjectPool.Add(currentObject);
            }
        }
    }
}

