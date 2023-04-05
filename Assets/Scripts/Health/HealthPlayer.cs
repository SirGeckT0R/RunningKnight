using System.Collections;
using UnityEngine;

public class HealthPlayer : Health
{
    [Header("Game Manager")]
    [SerializeField] protected GameManager gameManager;

    private Rigidbody2D rigidbodyPlayer;
    override public void TakeDamage(float _damage)
    {
        if (invulnerability)
        {
            return;
        }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        //player hurt
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
            //SoundManager.instance.PlaySound(hurtSound);
        }
        //player died
        else
        {
            if (!dead)
            {
                foreach (Behaviour comp in behaviours)
                {
                    comp.enabled = false;
                }
                rigidbodyPlayer=GetComponent<Rigidbody2D>();
                rigidbodyPlayer.velocity = Vector2.zero;


                //anim.SetBool("Grounded", true);
                anim.SetTrigger("Death");

                dead = true;
                Invoke("EndGame", 2f);

                //SoundManager.instance.PlaySound(deathSound);

            }

        }
    }

    private void EndGame()
    {
        gameManager.EndGame();
    }
}
