using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOverlay : MonoBehaviour {

	 public Material overlay;
    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, overlay);
    }
}
