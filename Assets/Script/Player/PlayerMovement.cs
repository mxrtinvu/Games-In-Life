using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator animation;
    private bool grounded;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private NPC_Controller npc;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(!inDialogue())
        {
          horizontalInput = Input.GetAxis("Horizontal");

          //flipping player sprite from left right
          if (horizontalInput > 0.01f)
              transform.localScale = Vector3.one;
          else if (horizontalInput < -0.01f)
              transform.localScale = new Vector3(-1, 1, 1);

          animation.SetBool("Run", horizontalInput != 0);
          animation.SetBool("Grounded", isGrounded());

          if(wallJumpCooldown > 0.2f)
          {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if(onWall() && !isGrounded())
            {
              body.gravityScale = 0;
              body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 2.2f;

            if (Input.GetKey(KeyCode.Space))
                Jump();
          }
          else
              wallJumpCooldown += Time.deltaTime;
          }
      }


    private void Jump()
    {
      if(isGrounded())
      {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        animation.SetTrigger("Jump");
      }
      else if (onWall() && !isGrounded())
      {
        if (horizontalInput == 0)
        {
          body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 8, 0);
          transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

        wallJumpCooldown = 0;
      }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool isGrounded()
    {
      RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
      return raycastHit.collider != null;
    }

    private bool onWall()
    {
      RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
      return raycastHit.collider != null;
    }

    private bool inDialogue()
    {
      if(npc != null)
        return npc.DialogueActive();
      else
        return false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "NPC")
      {
        npc = collision.gameObject.GetComponent<NPC_Controller>();

        if(Input.GetKey(KeyCode.E))
          npc.ActivateDialogue();
      }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      npc = null;
    }
}
