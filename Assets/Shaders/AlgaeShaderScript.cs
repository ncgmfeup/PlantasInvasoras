using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgaeShaderScript : MonoBehaviour {

	[SerializeField]
	private Material EffectMaterial;
	private float m_startMagnitude, m_currMagnitude, m_finalMagnitude;
	private float m_randomTime;
	private float m_elapsedTime = 0;
	
	void Start() {
		m_startMagnitude = Random.Range(0f, 0.12f); 
		m_currMagnitude = m_startMagnitude;
		m_finalMagnitude = Random.Range(0f, 0.12f);
		m_randomTime = Random.Range(0.3f, 0.7f);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst){
		Graphics.Blit(src, dst, EffectMaterial);
	}

	void Update() {
		if (m_elapsedTime >= m_randomTime) {
			m_elapsedTime = 0;
			m_startMagnitude = m_currMagnitude; 
			m_finalMagnitude = Random.Range(0f, 0.17f);
			m_randomTime = Random.Range(1f, 2.5f);	
		}

		Debug.Log("Magnitude " + EffectMaterial.GetFloat("_Magnitude"));
		

		m_currMagnitude = m_startMagnitude +
			 (m_finalMagnitude - m_startMagnitude) * (m_elapsedTime / m_randomTime); 
	

		EffectMaterial.SetFloat("_Magnitude", m_currMagnitude);
		
		m_elapsedTime += Time.deltaTime;
		
		
	}	
}
