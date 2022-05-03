using System.Collections;
using UnityEngine;

namespace DialogueSystem
{
  public class DialogueHolder : MonoBehaviour
  {
    private IEnumerator dialogueSeq;

    private void OnEnable()
    {
      dialogueSeq = dialogueSequence();
      StartCoroutine(dialogueSeq);
      //start dialogue sequence between user and npc
    }

    private void Update()
    {
      if(Input.GetKey(KeyCode.Q))
      {
        Deactivate();
        gameObject.SetActive(false);
        StopCoroutine(dialogueSeq);
        //stop the dialogue sequence by using the key Q
        //remove the game object dialogue 
      }
    }
    private IEnumerator dialogueSequence()
    {
      for (int i = 0; i < transform.childCount; i++)
      {
        Deactivate();
        transform.GetChild(i).gameObject.SetActive(true);
        yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
        //waits until previous line is finifhsed
      }
      gameObject.SetActive(false);
    }

    private void Deactivate()
    {
      for (int i = 0; i < transform.childCount; i++)
      {
        transform.GetChild(i).gameObject.SetActive(false);
      }
    }
  }
}
