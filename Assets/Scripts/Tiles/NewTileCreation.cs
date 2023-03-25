using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class NewTileCreation : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField] private GameObject player;
    [SerializeField] private Tile tile;
    private const Tile DANGER_TILE = null;

    private int cameraTopPosition;
    private int cameraRightPosition;

    private float randomNumber = 0;

    List<Vector3Int> positionsOfTiles= new List<Vector3Int>();
    List<Tile> tiles = new List<Tile>();
    private void Awake()
    {
        tilemap= GetComponent<Tilemap>();
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
        //string debug = "";
        //for(int i = 0; i < positionsOfTiles.Count; i++)
        //{
        //    debug+=positionsOfTiles[i].x + " ";
        //}
        //Debug.Log(debug);


        //getting position of where new tile should be planted and position of the one to be deleted
        int cameraLeftPosition = (int)Camera.main.ViewportToWorldPoint(Vector3Int.left).x - 3;

        cameraRightPosition = (int)Camera.main.ViewportToWorldPoint(Vector3Int.right).x + 3;


        //deleting tiles out of view to the left of the camera
        for (int i = -2; i < cameraTopPosition; i++)
        {
            tilemap.SetTile(tilemap.WorldToCell(new Vector3Int(cameraLeftPosition,i,0)), null);
        }


        if (positionsOfTiles.Count <10)
        {
            positionsOfTiles.Add(new Vector3Int(cameraRightPosition, -2, 0));
            tiles.Add(tile);
            return;
        }

        //if(positionsOfTiles.Count ==6) {
        //    positionsOfTiles.Add(new Vector3Int(cameraRightPosition, -2, 0));
        //    tiles.Add(DANGER_TILE);
        //    positionsOfTiles.Add(new Vector3Int(cameraRightPosition+1, -2, 0));
        //    tiles.Add(DANGER_TILE);
        //}


        randomNumber = Random.Range(-1f, 2f);
     
        //creating abyss of random length between 1 and 2 tiles
        if (randomNumber < 0 && !checkForAbyss())
        {
            int length =Mathf.RoundToInt(Random.Range(2f, 3f));
            for (int i = 0; i < length; i++)
            {
                positionsOfTiles.Add(new Vector3Int(positionsOfTiles[positionsOfTiles.Count - 1].x+1, -2, 0));
                tiles.Add(DANGER_TILE);
            }

        }
        else
        {
            positionsOfTiles.Add(new Vector3Int(positionsOfTiles[positionsOfTiles.Count-1].x+1, -2, 0));
            tiles.Add(tile);
        }

    

        if (!tilemap.HasTile(positionsOfTiles[0]) && positionsOfTiles[0].x <cameraRightPosition +2)
        {
       
            tilemap.SetTile(tilemap.WorldToCell(positionsOfTiles[0]), tiles[0]);
            positionsOfTiles.RemoveAt(0);
            tiles.RemoveAt(0);

        }


    }

    private bool checkForAbyss()
    {
        bool bool1 = tiles[tiles.Count - 1] == DANGER_TILE;
        bool bool2=tiles[tiles.Count - 2] == DANGER_TILE;
        return bool1 || bool2;
    }
}
