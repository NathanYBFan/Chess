using UnityEngine;

public sealed class Rows : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tiles;

    public GameObject[] Tiles => tiles;
}
