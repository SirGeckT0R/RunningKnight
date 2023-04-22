using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    //[SerializeField] private float attackCooldown;
   //[SerializeField] private Transform firePoint;
    [SerializeField] private GameObject arrow;
    //private float cooldownTimer;
    //private bool wasSpawned = true;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private void Awake()
    {
        Attack();
    }
    private void Attack()
    {   
        //cooldownTimer = 0;

        //SoundManager.instance.PlaySound(arrowSound);
        int random = Mathf.RoundToInt(Random.Range(2f, 3f));
        arrow.transform.position =new Vector2(Camera.main.ViewportToWorldPoint(Vector3Int.right).x + 3, random);
        arrow.GetComponent<EnemyProjectile>().ActivateProjectile();
    }
     

    //private int FindArrow()
    //{
    //    for (int i = 0; i < arrows.Length; i++)
    //    {
    //        if (!arrows[i].activeInHierarchy)
    //        {
    //            return i;
    //        }
    //    }
    //    return 0;
    //}
    //private void Update()
    //{
    //    cooldownTimer+= Time.deltaTime;
    //    if((cooldownTimer>=attackCooldown && !arrow.activeInHierarchy) || wasSpawned)
    //    {
    //        wasSpawned = false;
    //        Attack();
    //    }
    //}
}
