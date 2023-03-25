
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EndlessTiles : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField] private GameObject player;
    [SerializeField] private Tile tile;

    private int cameraTopPosition;
    private int cameraRightPosition;
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        cameraTopPosition = (int)Camera.main.ViewportToWorldPoint(Vector3Int.up).y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.y < -6)
        {
            EditorApplication.isPlaying = false;
            Application.Quit();

        }


        //getting position of where new tile should be planted and position of the one to be deleted
        int cameraLeftPosition = (int)Camera.main.ViewportToWorldPoint(Vector3Int.left).x - 3;

        cameraRightPosition = (int)Camera.main.ViewportToWorldPoint(Vector3Int.right).x + 1;


        //deleting tiles out of view to the left of the camera
        for (int i = -2; i < cameraTopPosition; i++)
        {
            tilemap.SetTile(tilemap.WorldToCell(new Vector3Int(cameraLeftPosition, i, 0)), null);
        }



        tilemap.SetTile(tilemap.WorldToCell(new Vector3Int(cameraRightPosition, -2, 0)), tile);
    }
}
