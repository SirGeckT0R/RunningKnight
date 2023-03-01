using UnityEngine;
using UnityEngine.Tilemaps;

public class NewTileCreation : MonoBehaviour
{
    Tilemap tilemap;
    [SerializeField] private Tile tile;
    private void Awake()
    {
        tilemap= GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        tilemap.SetTile(tilemap.WorldToCell(new Vector3Int((int)Camera.main.ViewportToWorldPoint(Vector3Int.right).x + 3, -2, 0)), tile);
        tilemap.SetTile(tilemap.WorldToCell(new Vector3Int((int)Camera.main.ViewportToWorldPoint(Vector3Int.left).x - 3, -2, 0)), null);
    }
}
