using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set;}
    private Animator anim;
    private bool dead;


    [Header("Iframes")]
    [SerializeField] private float iframeDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerability;

    [Header("Hurt Sound")]
    [SerializeField] private AudioClip hurtSound;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend= GetComponent<SpriteRenderer>();
    }

   public void TakeDamage(float _damage)
    {
        if(invulnerability)
        {
            return;
        }
        currentHealth= Mathf.Clamp(currentHealth-_damage, 0, startingHealth);

        //player hurt
        if(currentHealth > 0) {
            //anim.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
            //SoundManager.instance.PlaySound(hurtSound);
        }
        //player died
        else
        {
            if (!dead)
            { 
                foreach(Behaviour comp in components)
                {
                    comp.enabled = false;
                }

                //anim.SetBool("Grounded", true);
                //anim.SetTrigger("Died");

                dead = true;
                //SoundManager.instance.PlaySound(deathSound);

            }

        }
    }

    public void addHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }


    private IEnumerator Invulnerability()
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

        foreach (Behaviour comp in components)
        {
            comp.enabled = true; ;
        }
    }
}
