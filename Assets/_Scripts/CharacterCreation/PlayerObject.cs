using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    [SerializeField]
    GameObject hat;
    [SerializeField]
    GameObject body;
    [SerializeField]
    GameObject leftArm;
    [SerializeField]
    GameObject rightArm;
    [SerializeField]
    GameObject leftLeg;
    [SerializeField]
    GameObject rightLeg;


    public void ChangeMaterial(Material mat)
    {
        hat.GetComponent<MeshRenderer>().material = mat;
        body.GetComponent<MeshRenderer>().material = mat;
        leftArm.GetComponent<MeshRenderer>().material = mat;
        rightArm.GetComponent<MeshRenderer>().material = mat; 
        leftLeg.GetComponent<MeshRenderer>().material = mat;
        rightLeg.GetComponent<MeshRenderer>().material = mat;
    }
}
