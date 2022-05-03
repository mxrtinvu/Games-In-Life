using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
  [Header ("Health")]
  [SerializeField] private float startingHealth;
  public float currentHealth { get; private set; }
  private Animator animation;
  private bool dead;
  //Health component

  [Header ("Invulnerability")]
  [SerializeField] private float invulnerabilityDuration;
  [SerializeField] private int numberOfFlashes;
  private SpriteRenderer spriteRend;
  //Invulnerability component

  [Header("Components")]
  [SerializeField] private Behaviour[] components;
  private bool invulnerable;
  //Controls component

  private void Awake()
  {
    currentHealth = startingHealth;
    animation = GetComponent<Animator>();
    spriteRend = GetComponent<SpriteRenderer>();
    //Assign components to variables
  }

  public void TakeDamage(float _damage)
  {
    currentHealth = Mathf.Clamp(currentHealth - _damage, 0 , startingHealth);
    //clamp will not let the value exceed the current and starting health

    if(currentHealth > 0)
    {
      animation.SetTrigger("Hurt");
      StartCoroutine(Invulnerability());
      //if animation of hurt is played then start invulnerable function
    }
    else
    {
      if(!dead)
      {
        foreach (Behaviour component in components)
          component.enabled = false;

        animation.SetTrigger("Die");
        GetComponent<PlayerMovement>().enabled = false;
        dead = true;
        //if player animation is die then stop all movement and components
      }

    }
  }
  public void AddHealth(float _value)
  {
    currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    //add health does not exceed value of starting health which is 3
  }

  private IEnumerator Invulnerability()
  {
    Physics2D.IgnoreLayerCollision(8,9, true);
    //invulnerable so ignore collisions of layers 8 and 9 which is player and enemies
    for (int i = 0; i < numberOfFlashes; i++)
    {
      spriteRend.color = new Color (1, 0, 0, 0.5f);
      //new sprite colour will turn red for every flash
      yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
      //edit settings through unity
      spriteRend.color = Color.white;
      //return back to regular sprite colour
      yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
    }
    Physics2D.IgnoreLayerCollision(8,9, false);
    //stops invulnerable and player can collide with enemies again
  }

  public void Respawn()
  {
    dead = false;
    AddHealth(startingHealth);
    //health returns back to starting which is value 3
    animation.ResetTrigger("Die");
    animation.Play("Idle");
    //play idle animation after die animation
    StartCoroutine(Invulnerability());
    //small Invulnerability phase after respawn

    foreach (Behaviour component in components)
      component.enabled = true;
      //allow components such as movement after respawn
  }
}
