using UnityEngine;

public class HealthCollectible : Collectible
{
    [SerializeField] private float healthValue;
    private Collider2D collision;
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            //SoundManager.instance.PlaySound(pickupSound);
            collision = collider;

            collision.GetComponent<Health>().addHealth(healthValue);
            DestroyColectible();
        }
    }

}
