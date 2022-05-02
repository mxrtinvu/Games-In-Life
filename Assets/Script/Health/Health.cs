using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
  [Header ("Health")]
  [SerializeField] private float startingHealth;
  public float currentHealth { get; private set; }
  private Animator animation;
  private bool dead;

  [Header ("Invulnerability")]
  [SerializeField] private float invulnerabilityDuration;
  [SerializeField] private int numberOfFlashes;
  private SpriteRenderer spriteRend;

  [Header("Components")]
  [SerializeField] private Behaviour[] components;
  private bool invulnerable;

  private void Awake()
  {
    currentHealth = startingHealth;
    animation = GetComponent<Animator>();
    spriteRend = GetComponent<SpriteRenderer>();
  }

  public void TakeDamage(float _damage)
  {
    currentHealth = Mathf.Clamp(currentHealth - _damage, 0 , startingHealth);

    if(currentHealth > 0)
    {
      animation.SetTrigger("Hurt");
      StartCoroutine(Invulnerability());
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
      }

    }
  }
  public void AddHealth(float _value)
  {
    currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
  }

  private IEnumerator Invulnerability()
  {
    Physics2D.IgnoreLayerCollision(8,9, true);
    for (int i = 0; i < numberOfFlashes; i++)
    {
      spriteRend.color = new Color (1, 0, 0, 0.5f);
      yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
      spriteRend.color = Color.white;
      yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
    }
    Physics2D.IgnoreLayerCollision(8,9, false);
  }

  public void Respawn()
  {
    dead = false;
    AddHealth(startingHealth);
    animation.ResetTrigger("Die");
    animation.Play("Idle");
    StartCoroutine(Invulnerability());

    foreach (Behaviour component in components)
      component.enabled = true;
  }
}
