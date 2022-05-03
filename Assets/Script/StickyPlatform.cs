using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.name == "Player")
    {
      collision.gameObject.transform.SetParent(transform);
      //on collision with moving platform change player to child of game object parent
      //so that the player can move as one with the game object
    }
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.name == "Player")
    {
      collision.gameObject.transform.SetParent(null);
      //remove player from the set parent
    }
  }
}
