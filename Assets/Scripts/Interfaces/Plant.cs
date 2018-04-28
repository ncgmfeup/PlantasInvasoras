using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateNamespace;
namespace PlantNamespace {

    public abstract class Plant : MonoBehaviour {
		
		protected StageManager manager; // So I can alter the managers state
		public enum PlantState {
			DRYING, WATERED // Add here more states you need
		}
        public void Start() {
            initializeVariables();
        }

        public void Update()  {
			updatePlantState();
        }

        public void DeSpawn()
        {
            gameObject.SetActive(false);
        }

		public abstract void updatePlantState();
        public abstract void bombed(float impact);
        public abstract void burnt();
        public abstract void cut();
        public abstract void caught();
        public abstract void initializeVariables();

        // Dying coroutine
        public abstract IEnumerator Die();
    }
}
