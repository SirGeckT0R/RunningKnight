using UnityEngine;

abstract public class Collectible : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioClip pickupSound;

    [Header("Destroy time")]
    [SerializeField] private float destroyTime;
    private float timer;
    abstract public void OnTriggerEnter2D(Collider2D collision);

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>destroyTime)
        {
            DestroyColectible();
        }
    }
    protected void DestroyColectible()
    {
        Destroy(gameObject);
    }

}
