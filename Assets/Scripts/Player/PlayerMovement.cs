using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    //[Header("Wall Jump")]
    //[SerializeField] private float wallJumpX;
    //[SerializeField] private float wallJumpY;

    [Header ("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Health health;
    private Animator anim;
    private BoxCollider2D boxCollider;
    //private float wallJumpCooldown;
   // private const float WALL_JUMP_POWER_X = 3;
    //private const float WALL_JUMP_POWER_Y = 7;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        //getting references og rigidbody and animator
        body = GetComponent<Rigidbody2D>();   
        anim=GetComponent<Animator>();
        boxCollider= GetComponent<BoxCollider2D>();
        health= GetComponent<Health>();
    }

    private void Update()
    {
        //PlayerInputMovement();
        if (Input.GetKeyDown(KeyCode.E))
        {
            health.TakeDamage(1);
        };

        anim.SetBool("IsGrounded", isGrounded());

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //Adjustable jump height
        if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }

        //if (onWall())
        //{
        //    body.gravityScale = 0;
        //    body.velocity = Vector2.zero;
        //}
        body.gravityScale = 5;
        body.velocity = new Vector2(speed, body.velocity.y);
        anim.SetBool("Run", true);

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (coyoteCounter <= 0  && jumpCounter <= 0){
            return;
        }


       // SoundManager.instance.PlaySound(jumpSound);
        //if (onWall())
        //{
        //    WallJump();
        //}
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        else
        {
            if(coyoteCounter > 0)
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else
            {
                if(jumpCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    jumpCounter--;
                }
            }
        }
        coyoteCounter = 0;
        
    }

    //private void WallJump()
    //{
    //    body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * WALL_JUMP_POWER_X, WALL_JUMP_POWER_Y));
    //    wallJumpCooldown = 0;
    //}
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider != null;
    }

    //private bool onWall()
    //{
    //    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    //    return raycastHit.collider != null;
    //}

    public bool canAttack()
    {
        return isGrounded() && horizontalInput == 0;
    }

    public void PlayerInputMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        float transfScaleX = Mathf.Abs(transform.localScale.x);

        //flipping player while moving left to right
        if (horizontalInput >= 0.01f || horizontalInput <= -0.01f)
        {
            transform.localScale = new Vector2(transfScaleX * Mathf.Sign(horizontalInput), transform.localScale.y);
        }

        //set Animator parameters


        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            Vector3 t = col.GetContact(0).normal;
            float angle = Vector2.SignedAngle(t, Vector2.up);
            if (angle>-91 && angle <-89)
            {
                Debug.Log("Left");
                enabled= false;
            }
        }
    }
}
