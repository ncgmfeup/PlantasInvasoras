using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class JacintaShader : MonoBehaviour
{

  public enum Priority {Evil_Aura, Decay_Aura}

  public Priority aura_priority; 


  [Header("Evil Aura Settings")]
  public Color evil_color = Color.white;

  [Range(0, 1)]
  public float max_strenght = 1.0f;

	public float lifetime = 1.0f;

	private float lastClick = 0;

  private float evil_aura_force = 0;

  [Header("Decay Aura Settings")]
  public Color decay_color = Color.white;

  private float decay_aura_force = 0;

  private SpriteRenderer spriteRenderer;

  [Header("Debug")]
  public bool force;

  [Range(0, 1)]
  public float force_evil;

  [Range(0, 1)]
  public float force_decay;

  void OnEnable()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    UpdateAura();
  }


  void Update()
  {
    UpdateEvilAura();
    UpdateDecayAura();
    if(force)
      ForceAura();
    UpdateAura();
  }

  

  void UpdateEvilAura()
  {
		float time = (Time.time - lastClick)/lifetime;
		evil_aura_force = Mathf.SmoothStep(max_strenght, 0, time);
  }

	void UpdateDecayAura()
  {
		//decay_aura_force = 0;
  }

  void ForceAura()
  {
    evil_aura_force = force_evil;
		decay_aura_force = force_decay;
  }

  void UpdateAura()
  {
    MaterialPropertyBlock mpb = new MaterialPropertyBlock();
    spriteRenderer.GetPropertyBlock(mpb);
    if(aura_priority == Priority.Evil_Aura)
      mpb.SetFloat("_AuraPriority", 0);
    else
      mpb.SetFloat("_AuraPriority", 1);
    mpb.SetColor("_EvilAuraColor", evil_color);
    mpb.SetFloat("_EvilAuraForce", evil_aura_force);
    mpb.SetColor("_DecayAuraColor", decay_color);
    mpb.SetFloat("_DecayAuraForce", decay_aura_force);
    spriteRenderer.SetPropertyBlock(mpb);
  }


  public void TurnOnEvilAura()
  {
		lastClick = Time.time;
  }

  public void SetDecay(float d) {
    decay_aura_force = d;
  }
}
