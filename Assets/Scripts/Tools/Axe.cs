using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Axe : ToolNamespace.Tool
{

  private float m_secondsToSweep;

  private Vector3 m_displacement;

  public Axe()
  {
    InitializeVariables();
  }

  public override void UseTool(Vector3 pos)
  {
    //StartCoroutine("Sweep", pos);
  }

  IEnumerator Sweep(Vector3 pos)
  {
    Vector3 startAngle = new Vector3(0, 0, 45);
    Vector3 finalAngle = new Vector3(0, 0, -45);

    Vector3 startPos = pos + m_displacement;
    Vector3 finalPos = pos + m_displacement;

    float elapsedTime = 0;

    while (elapsedTime < m_secondsToSweep)
    {
      transform.eulerAngles = Vector3.Lerp(startAngle, finalAngle, (elapsedTime / m_secondsToSweep));
      transform.position = Vector3.Lerp(startPos, finalPos, (elapsedTime / m_secondsToSweep));

      elapsedTime += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }
    if (gameObject != null)
      Destroy(gameObject);
  }

  public override void InitializeVariables()
  {
    m_secondsToSweep = 0.6f;
    m_displacement = new Vector3(-2f, 0, 0);
  }

  // Update is called once per frame
  public override void UpdateToolState()
  {
    Debug.Log("Axe Swipe");
  }
}
