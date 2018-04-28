using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantNamespace;

namespace ToolNamespace {
    public abstract class Tool : MonoBehaviour
    {
        public string m_toolName { get; set; }

        public enum ToolState
        {
            ACTIVE, INACTIVE // Add here more states you need
        }

        public Tool() {
            InitializeVariables();
        }
    
        public abstract void UpdateToolState(); // Whenever an update is triggered

        public abstract void InitializeVariables();

        public abstract void UseTool(Vector3 position);

        // void outraCenaQueNaoSeiONome();

    }
}