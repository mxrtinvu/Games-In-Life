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

    [SerializeField] private float delay;
    [SerializeField] private float delayBetweenLines;

    private IEnumerator lineAppear;

    private void Awake()
    {

    }

    private void OnEnable()
    {
      ResetLine();
      lineAppear = WriteText(input, textHolder, delay, delayBetweenLines);
      StartCoroutine(lineAppear);
    }

    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
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
