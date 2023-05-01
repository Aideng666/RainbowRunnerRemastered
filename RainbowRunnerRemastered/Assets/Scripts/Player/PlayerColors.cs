using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerColors : MonoBehaviour
{
    [SerializeField] List<SkinnedMeshRenderer> meshesToEdit;
    [SerializeField] Material color1Mat;
    [SerializeField] Material color2Mat;
    [SerializeField] Material color3Mat;
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] Color color3;
    [SerializeField] VolumeProfile volume;

    LayerMask color1Layer;
    LayerMask color2Layer;
    LayerMask color3Layer;

    int currentColor;
    Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        volume.TryGet(out vignette);

        color1Layer = LayerMask.NameToLayer("Color1");
        color2Layer = LayerMask.NameToLayer("Color2");
        color3Layer = LayerMask.NameToLayer("Color3");

        //Sets the initial material
        currentColor = 1;
        vignette.color.Override(color1);
        gameObject.layer = color1Layer;

        foreach (SkinnedMeshRenderer meshRenderer in meshesToEdit)
        {
            meshRenderer.material = color1Mat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Swaps the players current selected color based on input (Updates the players layer and material)
        if (InputManager.Instance.GetColor1Input())
        {
            currentColor = 1;
            vignette.color.Override(color1);
            gameObject.layer = color1Layer;

            foreach (SkinnedMeshRenderer meshRenderer in meshesToEdit)
            {
                meshRenderer.material = color1Mat;
            }
        }
        else if (InputManager.Instance.GetColor2Input())
        {
            currentColor = 2;
            vignette.color.Override(color2);
            gameObject.layer = color2Layer;

            foreach (SkinnedMeshRenderer meshRenderer in meshesToEdit)
            {
                meshRenderer.material = color2Mat;
            }
        }
        else if (InputManager.Instance.GetColor3Input())
        {
            currentColor = 3;
            vignette.color.Override(color3);
            gameObject.layer = color3Layer;

            foreach (SkinnedMeshRenderer meshRenderer in meshesToEdit)
            {
                meshRenderer.material = color3Mat;
            }
        }
    }
}
