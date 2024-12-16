
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CutoutMaskUI : Image
{
    private static readonly int _stencilComp = Shader.PropertyToID("_StencilComp");

    public override Material materialForRendering
    {
        get
        {
            Material mat = new Material(base.materialForRendering);
            mat.SetInt(_stencilComp, (int)CompareFunction.NotEqual);
            return mat;
        }
    }
}
