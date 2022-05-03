using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField] private Transform player;
  private float currentPosX;

  private void Update()
  {
    transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    //transform.position will be a player followed camera only horizontal
  }

  public void MoveToNewRoom(Transform _newRoom)
  {
    currentPosX = _newRoom.position.x;
  }
}
