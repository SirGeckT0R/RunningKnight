using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthEnemy : Health
{
    [Header("Score Points")]
    [SerializeField] private int scorePoints;
    private Score scoreManager;
    override public void TakeDamage(float _damage)
    {
        if(invulnerability)
        {
            return;
        }
        currentHealth= Mathf.Clamp(currentHealth-_damage, 0, startingHealth);

        //enemy hurt
        if(currentHealth > 0) {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
            //SoundManager.instance.PlaySound(hurtSound);
        }
        //enemy died
        else
        {
            if (!dead)
            { 
                foreach(Behaviour comp in behaviours)
                {
                    comp.enabled = false;
                }
                scoreManager = (Score)FindAnyObjectByType(typeof(Score));
                //anim.SetBool("Grounded", true);
                scoreManager.addScore(scorePoints);
                anim.SetTrigger("Death");

                dead = true;
                //SoundManager.instance.PlaySound(deathSound);
            }

        }
    }
}
