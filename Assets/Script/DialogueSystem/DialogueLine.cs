using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
  public class DialogueLine : DialogueBaseClass
  {
    private Text textHolder;

    [Header ("Text Options")]
    [SerializeField] private string input;
    //can edit input from unity

    [SerializeField] private float delay;
    [SerializeField] private float delayBetweenLines;
    //edit the delay speed

    private IEnumerator lineAppear;

    private void Awake()
    {

    }

    private void OnEnable()
    {
      ResetLine();
      lineAppear = WriteText(input, textHolder, delay, delayBetweenLines);
      StartCoroutine(lineAppear);
      //start the process of executing the loop of each letter and therefore a dialogue
    }

    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
      //mouse button down to make line appear automatically
      {
        if(textHolder.text != input)
        {
          StopCoroutine(lineAppear);
          textHolder.text = input;
        }
        else
          finished = true;
      }
    }

    private void ResetLine()
    {
      textHolder = GetComponent<Text>();
      textHolder.text = "";
      finished = false;
    }
  }

}
