using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{

    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    //private Animator anim;
    private BoxCollider2D coll;

    [Header("SFX")]
    [SerializeField] private AudioClip hitSound;

    private bool hit;
    private void Awake()
    {
       // anim= GetComponent<Animator>();
        coll= GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile() {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }

    private void Update()
    {
        if(hit)
        {
            return;
        }
        float movementSpeed=speed*Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > resetTime) {

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        SoundManager.instance.PlaySound(hitSound);
        base.OnTriggerEnter2D (collision);
        coll.enabled = false;

        Deactivate();

        //if (anim != null)
        //{
        //    anim.SetTrigger("Explode");
        //}
        //else
        //{
        //    gameObject.SetActive(false);
        //}
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
