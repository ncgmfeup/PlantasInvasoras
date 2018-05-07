using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class WaterShaderScript : MonoBehaviour {

	[SerializeField]
	private Material EffectMaterial;
	private float m_startMagnitudeWave, m_currMagnitudeWave, m_finalMagnitudeWave;
	private float m_startMagnitudeDisp, m_currMagnitudeDisp, m_finalMagnitudeDisp;

	private float m_startPosWave, m_finalPosWave;
	private float m_randomTime;
	private float m_elapsedTime = 0;

	private Vector4 wavePosition;


	private float badColorR = .42f, badColorG = 0.72f, badColorB = 0.5f;
	private Color colorWater;

	void Start() {
		colorWater = new Color(1,1,1,1);

		m_startMagnitudeWave = Random.Range(0f, 0.12f); 
		m_currMagnitudeWave = m_startMagnitudeWave;
		m_finalMagnitudeWave = Random.Range(0.01f, .2f);

		m_startMagnitudeDisp = Random.Range(0f, 0.12f); 
		m_currMagnitudeDisp = m_startMagnitudeDisp;
		m_finalMagnitudeDisp = Random.Range(0.01f, .2f);

		m_randomTime = Random.Range(0.3f, 0.7f);
		wavePosition = new Vector4(0,0,0,0);
		m_startPosWave = wavePosition.x;
			m_finalPosWave = Random.Range(wavePosition.x + 0.05f * m_randomTime,
				 wavePosition.x + 0.09f * m_randomTime);		
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst){
		Graphics.Blit(src, dst, EffectMaterial);
	}

	void Update() {
		if (m_elapsedTime >= m_randomTime) {
			m_elapsedTime = 0;
			m_randomTime = Random.Range(.3f, .5f);	
		
			m_startMagnitudeWave = m_currMagnitudeWave; 
			m_finalMagnitudeWave = Random.Range(Mathf.Max(0.01f,
				m_currMagnitudeWave - (0.02f / m_randomTime)), 
				Mathf.Min(m_currMagnitudeWave + (0.02f / m_randomTime), .14f));

			m_startMagnitudeDisp = m_currMagnitudeDisp; 
			m_finalMagnitudeDisp = Random.Range(Mathf.Max(0.01f,
				m_currMagnitudeDisp - (0.02f / m_randomTime)), 
				Mathf.Min(m_currMagnitudeDisp + (0.02f / m_randomTime), .2f));

			m_startPosWave = wavePosition.x;
			m_finalPosWave = Random.Range(wavePosition.x + 0.05f * m_randomTime,
				 wavePosition.x + 0.09f * m_randomTime);		
			//Debug.Log("Start " + m_startPosWave + " final " + m_finalPosWave);
		}

		m_currMagnitudeWave = Mathf.Lerp(m_startMagnitudeWave, m_finalMagnitudeWave, (m_elapsedTime / m_randomTime));
		m_currMagnitudeDisp = Mathf.Lerp(m_startMagnitudeDisp, m_finalMagnitudeDisp, (m_elapsedTime / m_randomTime));
		wavePosition.x = Mathf.Lerp(m_startPosWave, m_finalPosWave, m_elapsedTime / m_randomTime);
		//m_currMagnitude = m_startMagnitude +
			 //(m_finalMagnitude - m_startMagnitude) * (m_elapsedTime / m_randomTime); 

		//Debug.Log("Wave " + m_currMagnitudeWave + " disp " + m_currMagnitudeDisp) ;
		//Debug.Log("Offset " + wavePosition) ;

		EffectMaterial.SetFloat("_MagnitudeWave", m_currMagnitudeWave);
		EffectMaterial.SetFloat("_MagnitudeDisp", m_currMagnitudeDisp);
		EffectMaterial.SetVector("_Offset" , wavePosition);

		
		m_elapsedTime += Time.deltaTime;
		
	}	


	public void UpdateHealth(float health) {
		colorWater.r = badColorR + (1 - badColorR) * health/100f;
		colorWater.g = badColorG + (1 - badColorG) * health/100f;
		colorWater.b = badColorB + (1 - badColorB) * health/100f;

		EffectMaterial.SetColor("_ColorTint", colorWater);
	}
}
