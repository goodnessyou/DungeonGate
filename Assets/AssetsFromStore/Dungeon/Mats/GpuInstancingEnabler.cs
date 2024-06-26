using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GpuInstancingEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
