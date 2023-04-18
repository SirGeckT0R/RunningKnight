using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [Header ("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Enemy Layer")]
    [SerializeField] private LayerMask EnemyLayer;
    private float cooldownTimer=Mathf.Infinity;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    private Animator anim;
    private Health enemyHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && !PauseMenu.GameIsPaused)
        {

            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack3");
                SoundManager.instance.PlaySound(attackSound);
            }
        }

    }

    private bool EnemyInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range*transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range,boxCollider.bounds.size.y, boxCollider.bounds.size.z)
            , 0,Vector2.left,0,EnemyLayer);

        if (hit.collider != null){
            enemyHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider!=null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x* colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
            );
    }

    private void DamageEnemy()
    {
        if (EnemyInSight() && enemyHealth!=null)
        {
           enemyHealth.TakeDamage(damage);  
        }
    }
}
