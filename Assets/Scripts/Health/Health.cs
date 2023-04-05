using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] protected float startingHealth;
    public float currentHealth { get; protected set;}
    protected Animator anim;
    protected bool dead;


    [Header("Iframes")]
    [SerializeField] protected float iframeDuration;
    [SerializeField] protected int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] protected Behaviour[] behaviours;
    protected bool invulnerability;

    [Header("Hurt Sound")]
    [SerializeField] protected AudioClip hurtSound;

    [Header("Death Sound")]
    [SerializeField] protected AudioClip deathSound;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend= GetComponent<SpriteRenderer>();
    }

    abstract public void TakeDamage(float _damage);
   //public void TakeDamage(float _damage)
   // {
   //     if(invulnerability)
   //     {
   //         return;
   //     }
   //     currentHealth= Mathf.Clamp(currentHealth-_damage, 0, startingHealth);

    //     //player hurt
    //     if(currentHealth > 0) {
    //         anim.SetTrigger("Hurt");
    //         StartCoroutine(Invulnerability());
    //         //SoundManager.instance.PlaySound(hurtSound);
    //     }
    //     //player died
    //     else
    //     {
    //         if (!dead)
    //         { 
    //             foreach(Behaviour comp in behaviours)
    //             {
    //                 comp.enabled = false;
    //             }
    //             if(rigidbodyPlayer!= null)
    //             {
    //                 rigidbodyPlayer.velocity = Vector2.zero;
    //             }


    //             //anim.SetBool("Grounded", true);
    //             anim.SetTrigger("Death");

    //             dead = true;
    //             if (this.gameObject.tag=="Player")
    //             {
    //                 Invoke("EndGame", 2f);
    //             }

    //             //SoundManager.instance.PlaySound(deathSound);

    //         }

    //     }
    // }

    public void addHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    protected IEnumerator Invulnerability()
    {
        invulnerability= true;
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for(int i = 0;i < numberOfFlashes;i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframeDuration/(numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iframeDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        invulnerability = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        dead = false;
        addHealth(startingHealth);
        anim.ResetTrigger("Died");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());
    }
}
