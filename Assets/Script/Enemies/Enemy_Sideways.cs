using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
  [SerializeField] private float movementDistance;
  [SerializeField] private float speed;
  [SerializeField] private float damage;
  private bool movingLeft;
  private float rightEdge;
  private float leftEdge;

  private void Awake()
  {
    leftEdge = transform.position.x - movementDistance;
    rightEdge = transform.position.x + movementDistance;
    //range of movement left and right
  }

  private void Update()
  {
    if (movingLeft)
    {
      if(transform.position.x > leftEdge)
      //if moving left
      {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        //increase the transform position of the trap by the speed and time
      }
      else
          movingLeft = false;
          //moving object to the right
    }
    else
    {
      if(transform.position.x < rightEdge)
      {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
      }
      else
          movingLeft = true;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      collision.GetComponent<Health>().TakeDamage(damage);
      //if the saw touches the player then it will inflict damage of 1 (edited in unity)
    }
  }
}
