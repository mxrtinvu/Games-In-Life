using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
  [SerializeField] private GameObject dialogue;

  public void ActivateDialogue()
  {
    dialogue.SetActive(true);
    //When npc is interacted with activate dialogue on screen
  }

  public bool DialogueActive()
  {
    return dialogue.activeInHierarchy;
    //play each dialogue in order 
  }
}
