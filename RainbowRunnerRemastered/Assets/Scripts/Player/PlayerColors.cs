using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColors : MonoBehaviour
{
    [SerializeField] List<SkinnedMeshRenderer> meshesToEdit;
    [SerializeField] Material color1Mat;
    [SerializeField] Material color2Mat;
    [SerializeField] Material color3Mat;


    LayerMask color1Layer;
    LayerMask color2Layer;
    LayerMask color3Layer;

    int currentColor;

    // Start is called before the first frame update
    void Start()
    {
        color1Layer = LayerMask.NameToLayer("Color1");
        color2Layer = LayerMask.NameToLayer("Color2");
        color3Layer = LayerMask.NameToLayer("Color3");

        //Sets the initial material
        currentColor = 1;
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
            gameObject.layer = color1Layer;

            foreach (SkinnedMeshRenderer meshRenderer in meshesToEdit)
            {
                meshRenderer.material = color1Mat;
            }
        }
        else if (InputManager.Instance.GetColor2Input())
        {
            currentColor = 2;
            gameObject.layer = color2Layer;

            foreach (SkinnedMeshRenderer meshRenderer in meshesToEdit)
            {
                meshRenderer.material = color2Mat;
            }
        }
        else if (InputManager.Instance.GetColor3Input())
        {
            currentColor = 3;
            gameObject.layer = color3Layer;

            foreach (SkinnedMeshRenderer meshRenderer in meshesToEdit)
            {
                meshRenderer.material = color3Mat;
            }
        }
    }
}
