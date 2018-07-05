using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlantAura : MonoBehaviour
{

  public Color color = Color.white;

  [Range(0, 1)]
  public float max_strenght = 1.0f;

	private float strenght = 1.0f;

	public float lifetime = 1.0f;

	private float lastClick = 0;

  private SpriteRenderer spriteRenderer;

  void OnEnable()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    UpdateOutline();
  }


  void Update()
  {
    UpdateAuraStrenght();
		UpdateOutline();
  }

	void UpdateAuraStrenght()
  {
		float time = (Time.time - lastClick)/lifetime;
		time = Mathf.Clamp(time, 0, 1);
		strenght = Mathf.SmoothStep(max_strenght, 0, time); 
  }

  void UpdateOutline()
  {
    MaterialPropertyBlock mpb = new MaterialPropertyBlock();
    spriteRenderer.GetPropertyBlock(mpb);
    mpb.SetColor("_AuraColor", color);
    mpb.SetFloat("_AuraForce", strenght);
    spriteRenderer.SetPropertyBlock(mpb);
  }
}
