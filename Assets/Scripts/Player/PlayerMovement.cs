using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float MAX_SPEED;


    [Header("Roll parameters")]
    [SerializeField] private float rollCooldown;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header ("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Vector2 baseSize;
    private Vector2 baseOffset;
    private Vector2 rollOffset;
    private Vector2 rollSize;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;
    private bool hasGrounded=true;

    private void Awake()
    {
        //getting references og rigidbody and animator
        body = GetComponent<Rigidbody2D>();   
        anim=GetComponent<Animator>();
        boxCollider= GetComponent<BoxCollider2D>();
        baseSize = boxCollider.size;
        baseOffset = boxCollider.offset;
        rollOffset = new Vector2(boxCollider.offset.x, boxCollider.offset.y / 2);
        rollSize = new Vector2(boxCollider.size.x, boxCollider.size.y / 2);
    }

    private void Update()
    {
        //PlayerInputMovement();

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        cooldownTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.C) && isGrounded())
        {
            if (cooldownTimer >= rollCooldown)
            {
                cooldownTimer = 0;
                Roll();
            }   
        }

        //Adjustable jump height
        if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }

        body.gravityScale = 5;
        body.velocity = new Vector2(speed, body.velocity.y);

        anim.SetInteger("AnimState", 1);
        if(hasGrounded== false && isGrounded()) {
            SoundManager.instance.PlaySound(jumpSound);
        }
        hasGrounded = isGrounded();

        anim.SetBool("Grounded", hasGrounded);

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (speed<MAX_SPEED)
        {
            speed += Time.deltaTime/10;
        }
    }

    private void Roll()
    {
        anim.SetTrigger("Roll");
        boxCollider.size = rollSize;
        boxCollider.offset = rollOffset;
    }

    private void StandUp()
    {
        boxCollider.size = baseSize;
        boxCollider.offset = baseOffset;
    }

    private void Jump()
    {
        if (coyoteCounter <= 0  && jumpCounter <= 0){
            return;
        }


        if (isGrounded())
        {
            anim.SetTrigger("Jump");
            SoundManager.instance.PlaySound(jumpSound);
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        else
        {
            if(coyoteCounter > 0)
            {
                anim.SetTrigger("Jump");
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else
            {
                if(jumpCounter > 0)
                {
                    anim.SetTrigger("Jump");
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    jumpCounter--;
                }
            }
        }
        coyoteCounter = 0;
        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider != null;
    }


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


    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag.Equals("Ground"))
    //    {
    //        Vector3 t = col.GetContact(0).normal;
    //        float angle = Vector2.SignedAngle(t, Vector2.up);
    //        if (angle>-91 && angle <-89)
    //        {
    //            Debug.Log("Left");
    //            enabled= false;
    //        }
    //    }
    //}
}
