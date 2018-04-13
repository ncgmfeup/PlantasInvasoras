using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolNamespace {
    public abstract class Tool {

    public enum ToolState {
        ACTIVE, INACTIVE // Add here more states you need
    }

    public Tool() {
        initializeVariables();
    }

    public void activated() {
        updateToolState();
    }
    
    public abstract void updateToolState(); // Whenever an update is triggered

    public abstract void initializeVariables();

    // void outraCenaQueNaoSeiONome();

    }
}