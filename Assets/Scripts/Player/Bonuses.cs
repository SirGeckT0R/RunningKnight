using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleTypes
{
    Destruction,
    SteelShoes,
    Default
}
public class Bonuses : MonoBehaviour
{
    [Header("Max Amount")]
    [SerializeField] private int maxAmount;
    private List<CollectibleTypes> collected=new List<CollectibleTypes>();
    [SerializeField] private Collectible collect;

    [Header("Iframes")]
    [SerializeField] protected float iframeDuration;
    [SerializeField] protected int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private int playerLayer;
    private int spikeLayer;

    private void Awake()
    {
        spriteRend= GetComponent<SpriteRenderer>();
        playerLayer = LayerMask.NameToLayer("Player");
        spikeLayer = LayerMask.NameToLayer("Spike");
    }
    private void Update()
    {
            Debug.Log(collected.Count);
        if (Input.GetKeyDown(KeyCode.F) && collected.Count!=0){
            switch (collected[0])
            {
                case CollectibleTypes.Destruction:
                    SpawnManager spawnManager = (SpawnManager)FindAnyObjectByType(typeof(SpawnManager));
                    spawnManager.emptySpawnedList();
                    collected.RemoveAt(0);
                    break;
                case CollectibleTypes.SteelShoes:
                    collected.RemoveAt(0);
                    StartCoroutine(InvulnerabilityFromSpikes());
                    break;
                case CollectibleTypes.Default:
                    break;
            }
        }
    }

    public void AddBonus(CollectibleTypes type)
    {
        if(collected.Count < maxAmount)
        {
            collected.Add(type);
        }
    }

    protected IEnumerator InvulnerabilityFromSpikes()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, spikeLayer, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(0, 1, 0, 0.5f);
            yield return new WaitForSeconds(iframeDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iframeDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(playerLayer, spikeLayer, false);
    }

}
