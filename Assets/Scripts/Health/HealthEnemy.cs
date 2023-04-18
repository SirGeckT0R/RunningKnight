using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : Health
{
    [Header("Score Points")]
    [SerializeField] private int scorePoints;
    private Score scoreManager;


    [Header("Bonuses")]
    [SerializeField] private List<GameObject> bonuses;
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

                DropBonus();
                anim.SetTrigger("Death");

                dead = true;
                SoundManager.instance.PlaySound(hurtSound);
            }

        }
    }

    private void DropBonus()
    {
        float random= Random.Range(-1f, 1f);
        if(random> 0)
        {
            random = Random.Range(0, bonuses.Count);
            GameObject spawn = Instantiate(bonuses[Mathf.FloorToInt(random)]);    
            spawn.transform.position = gameObject.transform.position;
        }
    }
}
