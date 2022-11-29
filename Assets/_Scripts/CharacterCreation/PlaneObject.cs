using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneObject : MonoBehaviour
{
    public void ChangeMaterial(Material mat)
    {
        //3rd material (with index 2) on the plane is the main color
        Material[] materials = GetComponent<Renderer>().materials;
        materials[2] = mat;
        GetComponent<Renderer>().materials = materials;
    }
}
