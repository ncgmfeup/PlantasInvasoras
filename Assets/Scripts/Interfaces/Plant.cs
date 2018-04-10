using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateNamespace;
namespace PlantNamespace {

    public abstract class Plant : MonoBehaviour {
		
		public StageManager manager; // So I can alter the managers state
		public enum PlantState {
			DRYING, WATERED // Add here more states you need
		}
        public void Start() {
            initializeVariables();
        }

        public void Update()  {
			updatePlantState();
        }

		public abstract void updatePlantState();

        public abstract void bombed();

        public abstract void burnt();
        public abstract void cut();

        public abstract void initializeVariables();

        // void outraCenaQueNaoSeiONome();

    }
}
