using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour {

    public ObjectPooling.ObjectPool m_sourcePool { get; set; }

	void OnEnable () {
        if (m_sourcePool != null)
            m_sourcePool.ActivateObject();
	}
	
	void OnDisable () {
        if (m_sourcePool != null)
            m_sourcePool.DeactivateObject();
	}
}
