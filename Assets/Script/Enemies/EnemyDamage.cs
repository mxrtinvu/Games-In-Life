using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
  [SerializeField] private float damage;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.tag == "Player")
        collision.GetComponent<Health>().TakeDamage(damage);
        //if enemy collides with a game object tagged player then inflict damage
  }
}
