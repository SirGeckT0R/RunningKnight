using UnityEngine;

public class DestructionCollectible : Collectible
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<Bonuses>().AddBonus(CollectibleTypes.Destruction);
            DestroyColectible();
        }
    }
}
