using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] Collider[] tutorialTriggers = new Collider[4];

    private void Start()
    {
        StartCoroutine(TriggerStartingPrompt());
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < tutorialTriggers.Length; i++)
        {
            if (other == tutorialTriggers[i])
            {
                Tutorial.Instance.ShowPrompt(i+1);
                break;
            }
        }
    }

    IEnumerator TriggerStartingPrompt()
    {
        yield return new WaitForSeconds(1.5f);
        Tutorial.Instance.ShowPrompt(0);
    }
}
