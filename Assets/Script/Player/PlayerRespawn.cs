using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
  private Transform currentCheckpoint;
  private Health playerHealth;

  private void Awake()
  {
    playerHealth = GetComponent<Health>();
  }

  public void Respawn()
  {
    transform.position = currentCheckpoint.position;
    //move player to checkpoint position
    playerHealth.Respawn();
    //restore the players health

    Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    //if there is object checkpoint under a parent room then move camera to this room
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Checkpoint")
    //if there a collision with an object with tag checkpoint then:
    {
      currentCheckpoint = collision.transform;
      collision.GetComponent<Collider2D>().enabled = false;
      collision.GetComponent<Animator>().SetTrigger("appear");
      //run appear animation
    }
  }
}
