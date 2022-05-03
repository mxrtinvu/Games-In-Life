using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
  public class DialogueBaseClass : MonoBehaviour
  {
    public bool finished { get; protected set; }
    //when one dialogue is finished
    protected IEnumerator WriteText(string input, Text textHolder, float delay, float delayBetweenLines)
    {

      for (int i = 0; i < input.Length; i++)
      {
        textHolder.text += input[i];
        yield return new WaitForSeconds(delay);
        //loop every letter into our textholder with a delay so that it is typed out
      }

      //yield return new WaitForSeconds(delayBetweenLines);
      yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
      finished = true;

    }
  }
}
