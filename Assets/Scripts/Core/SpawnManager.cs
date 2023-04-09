using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float maxCooldown;
    private List<GameObject> spawned=new List<GameObject>();
    private float timer = 0;
    private float cooldown=3;

    void Update()   
    {
        timer += Time.deltaTime;
        if(timer >= cooldown)
        {
            cooldown = Random.Range(0.5f, maxCooldown);
            timer = 0;
            int random = Random.Range(0, obstacles.Length);
            int cameraRightPosition = (int)Camera.main.ViewportToWorldPoint(Vector3Int.right).x + 3;
            while(spawned.Count > 6) {
                Destroy(spawned[0]);
                spawned.RemoveAt(0);
            }
            spawned.Add(Instantiate(obstacles[random]));
            spawned.LastOrDefault().SetActive(true);
            if (obstacles[random].name == "Spikes")
            {
                spawned.LastOrDefault().transform.position = new Vector2(cameraRightPosition, 0f);
            }
            else if (obstacles[random].name == "KnightMelee")
            {
                spawned.LastOrDefault().transform.position = new Vector2(cameraRightPosition, 0.8f);
            }
        }
        if (maxCooldown > 1)
        {
            maxCooldown -= Time.deltaTime / 100;
        }
    }

    public List<GameObject> getSpawnedObjects()
    {
        return spawned;
    }

    public void emptySpawnedList()
    {
        while (spawned.Count > 0)
        {
            Destroy(spawned[0]);
            spawned.RemoveAt(0);
        }
    }
}
