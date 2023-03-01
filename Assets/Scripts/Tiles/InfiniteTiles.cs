
using UnityEngine;

public class InfiniteTiles : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles;

    [SerializeField] private Transform playerTransform;
    void Update()
    {
        

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
    }


    private int FindTile()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (!tiles[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
