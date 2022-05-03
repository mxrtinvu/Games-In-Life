using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    //change value of speed and jump power through unity
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
        //components given variables
    }

    private void Update()
    {
        if(!inDialogue())
        {
          horizontalInput = Input.GetAxis("Horizontal");
          //horizontal axis defined by unity, a key is for left and d key is for right

          //flipping player sprite from left right
          if (horizontalInput > 0.01f)
              transform.localScale = Vector3.one;
          else if (horizontalInput < -0.01f)
          //if horizontal input is smaller than -0.01 for looking left
              transform.localScale = new Vector3(-1, 1, 1);
              //change scale of sprite to these values to flip

          animation.SetBool("Run", horizontalInput != 0);
          //when sprite is moving play animation run, when arrow key is not pressed then no run
          animation.SetBool("Grounded", isGrounded());

          if(wallJumpCooldown > 0.2f)
          {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            //body.velocity is used to move the sprite

            if(onWall() && !isGrounded())
            {
              body.gravityScale = 0;
              body.velocity = Vector2.zero;
              //when player is on the wall then they will be stuck and not fall down
            }
            else
                body.gravityScale = 2.2f;

            if (Input.GetKey(KeyCode.Space))
            //using the key space to process the jump function
                Jump();
          }
          else
              wallJumpCooldown += Time.deltaTime;
              //allowing cooldown between jumps to walljump
          }
      }


    private void Jump()
    {
      if(isGrounded())
      //can only jump when they are on the ground
      {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        //velocity x for vertical jump using the value of jump power in unity
        animation.SetTrigger("Jump");
        //setting the animation trigger named jump
      }
      else if (onWall() && !isGrounded())
      //wall jumping, if on the wall and not on the ground then:
      {
        if (horizontalInput == 0)
        {
          body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 8, 0);
          //greater force on the x axis to push away from the wall
          transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
          //the sprite is flipped when jumping off the wall
        }
        else
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            //3 is value the power of which the player is pushed from the wall. 6 is the force pushed up

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
      //raycasting line collides with our box collider against the layer mask wall
      return raycastHit.collider != null;
    }

    private bool inDialogue()
    {
      if(npc != null)
        return npc.DialogueActive();
        //return dialogue function to process
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
          //key code E to interact with NPC and start dialogue
      }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      npc = null;
    }
}
