using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantNamespace;

namespace ToolNamespace
{
  public abstract class Tool : MonoBehaviour
  {
    public string m_toolName { get; set; }

    public enum ToolState
    {
      ACTIVE, INACTIVE // Add here more states you need
    }

    protected SoundEffectsManager soundManager;

    public Tool()
    {
      InitializeVariables();
    }


    void Start()
    {
      soundManager = GameObject.Find("SoundEffectsManager").GetComponent<SoundEffectsManager>();
    }

    public abstract void UpdateToolState(); // Whenever an update is triggered

    public abstract void InitializeVariables();

    public abstract void UseTool(Vector3 position);

    public virtual void SetPosition(Vector3 position)
    {
      transform.position = position;
    }

    // void outraCenaQueNaoSeiONome();

  }
}