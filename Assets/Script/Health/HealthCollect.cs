using UnityEngine;

public class HealthCollect : MonoBehaviour
{
  [SerializeField] private float healthValue;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.tag == "Player")
    //if game object health has collided with plauer then:
    {
      collision.GetComponent<Health>().AddHealth(healthValue);
      //add health value which can be changed in unity
      gameObject.SetActive(false);
      //remove game object when health is picked up
    }
  }
}
