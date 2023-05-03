using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private Queue<CollectibleTypes> collected=new Queue<CollectibleTypes>();
    //[SerializeField] private Collectible collect;

    [Header("Iframes")]
    [SerializeField] protected float iframeDuration;
    [SerializeField] protected int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private BonusesVizualization ui;
    [Header("SFX")]
    [SerializeField] private AudioClip lightningSound;
    [SerializeField] private AudioClip steelShoeSound;

    private int playerLayer;
    private int spikeLayer;


    private GameManager gameManager;
    private void Awake()
    {
        gameManager =GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRend = GetComponent<SpriteRenderer>();
        playerLayer = LayerMask.NameToLayer("Player");
        spikeLayer = LayerMask.NameToLayer("Spike");
        ui=GameObject.Find("Bonuses").GetComponent<BonusesVizualization>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && collected.Count!=0){
            switch (collected.Dequeue())
            {
                case CollectibleTypes.Destruction:
                    SpawnManager spawnManager = (SpawnManager)FindAnyObjectByType(typeof(SpawnManager));
                    SoundManager.instance.PlaySound(lightningSound);
                    gameManager.PlayLightningAnim();
                    spawnManager.emptySpawnedList();
                    ui.RefreshUIList();
                    break;
                case CollectibleTypes.SteelShoes:
                    SoundManager.instance.PlaySound(steelShoeSound);
                    ui.RefreshUIList();
                    StartCoroutine(InvulnerabilityFromSpikes());
                    break;
                case CollectibleTypes.Default:
                    break;
            }
        }
    }

    public List<CollectibleTypes> getList()
    {
        return collected.ToList();
    }

    public void AddBonus(CollectibleTypes type)
    {
        if(collected.Count < maxAmount)
        {
            collected.Enqueue(type);
            ui.RefreshUIList();
        }
    }

    protected IEnumerator InvulnerabilityFromSpikes()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, spikeLayer, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(0.58f, 0.58f, 0.58f, 0.5f);
            yield return new WaitForSeconds(iframeDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iframeDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(playerLayer, spikeLayer, false);
    }

}
