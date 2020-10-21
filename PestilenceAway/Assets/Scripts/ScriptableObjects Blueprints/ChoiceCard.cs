using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choice Card", menuName = "Choice Card")]
public class ChoiceCard : ScriptableObject
{
    public new string name;
    [TextArea(1,10)]public string description;
    public Sprite spriteArt;

    public float effectOnCure1;
    public float effectOnCureSpeed1;
    public float effectOnPlague1;
    public float effectOnPlagueSpeed1;
    public float effectOnMoney1;
    public float effectOnHappiness1;

    public float effectOnCure2;
    public float effectOnCureSpeed2;
    public float effectOnPlague2;
    public float effectOnPlagueSpeed2;
    public float effectOnMoney2;
    public float effectOnHappiness2;
    public void AnswerYes()
    {
        Manager.Instance.ChangeCure(effectOnCure1);
        Manager.Instance.ChangeCureSpeed(effectOnCureSpeed1);
        Manager.Instance.ChangePlague(effectOnPlague1);
        Manager.Instance.ChangePlagueSpeed(effectOnPlagueSpeed1);
        Manager.Instance.ChangeMoney(effectOnMoney1);
        Manager.Instance.ChangePopulationHappiness(effectOnHappiness1);

        Manager.Instance.confirmChoice();
    }

    public void AnswerNo()
    {
        Manager.Instance.ChangeCure(effectOnCure2);
        Manager.Instance.ChangeCureSpeed(effectOnCureSpeed2);
        Manager.Instance.ChangePlague(effectOnPlague2);
        Manager.Instance.ChangePlagueSpeed(effectOnPlagueSpeed2);
        Manager.Instance.ChangeMoney(effectOnMoney2);
        Manager.Instance.ChangePopulationHappiness(effectOnHappiness2);

        Manager.Instance.confirmChoice();
    }
}
