using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantNamespace;

public class Bomb : ToolNamespace.Tool {

    public float m_radius = 100f;
    private float m_force = 370f;

    private float m_threshold = 0.8f;
    public Bomb() {
        InitializeVariables();
    }

    public override void UseTool(Vector3 pos) {
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
                affected.bombed(impact.magnitude);
            }
        }
    

    }

    public override void InitializeVariables() {
	}
	
	// Update is called once per frame
	public override void UpdateToolState() {
        
	}
}

