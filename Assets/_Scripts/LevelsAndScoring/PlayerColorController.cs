using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerColorController : MonoBehaviour
{
    [SerializeField]
    UnityEvent<Material> planeChange;
    [SerializeField]
    UnityEvent<Material> playerChange;

    [SerializeField]
    List<Material> colorMaterials;
    public void SetUpGraphics(int planeColor, int playerColor)
    {
        planeChange.Invoke(colorMaterials[planeColor]);
        playerChange.Invoke(colorMaterials[playerColor]);
    }
}
