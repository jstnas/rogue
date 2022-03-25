using UnityEngine;

[ExecuteInEditMode]
public class Palette : MonoBehaviour
{
    [SerializeField] private Material material;
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, material);
    }
}
