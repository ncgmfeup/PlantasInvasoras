using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantNamespace;

public class Bomb : ToolNamespace.Tool {

    public float m_radius = 100f;
    public float m_force = 370f;

    [SerializeField]
    private Sprite[] sprites;

    public float m_threshold = 0.8f;
    public Bomb() {
        InitializeVariables();
    }

    public override void UseTool(Vector3 pos) {

        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[UnityEngine.Random.Range(0,sprites.Length)];

        StartCoroutine("Explode");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, m_radius);

        foreach (Collider2D nearbyObject in colliders) {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();

            // Hit plant
            if (rb != null) {

                // Calculations necessary to prevent massive explosion, jacintas everywhere
                float xImpact, yImpact;
               
                // X Coordinate
                if (rb.transform.position.x > pos.x)
                    xImpact = m_force/Mathf.Max(rb.transform.position.x - pos.x, m_threshold);
                else 
                    xImpact = m_force/Mathf.Min(rb.transform.position.x - pos.x, -m_threshold);

                // Y Coordinate
                if (rb.transform.position.y > pos.y)
                    yImpact = m_force/Mathf.Max(rb.transform.position.y - pos.y, m_threshold);
                else 
                    yImpact = m_force/Mathf.Min(rb.transform.position.y - pos.y, -m_threshold);
                
                
                Vector2 impact = new Vector2(xImpact, yImpact);

                Debug.Log("Impact was " + impact);
            
                rb.AddForce(impact);

                Plant affected = nearbyObject.GetComponent<Plant>();
                if (affected != null)
                    affected.bombed(impact.magnitude);
            }
        }
    }

    private IEnumerator Explode() {
        Debug.Log("Exploded, fam");
        Debug.Log("Extracting from water");

		Vector3 startScale = new Vector3(0,0,0);
		Vector3 finalScale = this.transform.localScale/2f;
		
		float elapsedTime = 0;

        this.transform.localScale = startScale;
        float explosionTime = 0.4f;

		while (elapsedTime < explosionTime) 	{
			transform.localScale = Vector3.Lerp(startScale, finalScale * 2, 
				Easing.Quadratic.Out(elapsedTime / explosionTime));
			elapsedTime += Time.deltaTime;
        	yield return new WaitForEndOfFrame();
      	}

		elapsedTime = 0;

		/* while (elapsedTime < m_secondsToLift) 	{
			transform.position = Vector3.Lerp(startPos, finalPos, 
				Easing.Back.InOut(elapsedTime / m_secondsToLift));
			elapsedTime += Time.deltaTime;
        	yield return new WaitForEndOfFrame();
		} */

		Destroy(gameObject);
		yield return null;
        yield return null;
    }

    public override void InitializeVariables() {
	}
	
	// Update is called once per frame
	public override void UpdateToolState() {
        
	}
}

