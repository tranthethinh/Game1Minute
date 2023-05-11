using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
        // Create a new Material and set its shader to Standard
        Material material = new Material(Shader.Find("Standard"));

        // Set the material color to a random color
        material.color = new Color(Random.value, Random.value, Random.value);

        // Add Specular Highlights to the material
        material.EnableKeyword("_SPECGLOSSMAP");
        material.SetColor("_SpecColor", Color.white);
        material.SetFloat("_Glossiness", 0.5f);

        // Add Metallic Material to the material
        material.EnableKeyword("_METALLICGLOSSMAP");
        material.SetFloat("_Metallic", 1.0f);

        // Assign the material to the Renderer component of the GameObject
        GetComponent<Renderer>().material = material;

        //GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

// Update is called once per frame
void Update()
    {
        
    }
}
