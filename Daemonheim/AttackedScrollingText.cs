using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedScrollingText : MonoBehaviour, IAttackable
{
    public ScrollingText Text;
    private TextMesh tm;

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var text = attack.Damage.ToString();

        var scrollingText = Instantiate(Text, transform.position, Quaternion.identity);
        scrollingText.SetText(text);
        if (tag == "Player")
            scrollingText.SetColor(Color.red);
        else if (tag == "Enemy")
            scrollingText.SetColor(Color.yellow);
        else
            scrollingText.SetColor(Color.white);

        if (attack.IsCritical)
        {
            scrollingText.transform.localScale = new Vector3 (-1.5f, 1.5f, 1.5f);
        }
    }
}
