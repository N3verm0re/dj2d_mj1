using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject
{
    public new string name;
    [TextArea(1, 10)] public string description;

    public float minimumCure;
    public float maximumCure;
    public float minimumCureSpeed;
    public float maximumCureSpeed;
    public float minimumPlague;
    public float maximumPlague;
    public float minimumPlagueSpeed;
    public float maximumPlagueSpeed;
    public float minimumMoney;
    public float maximumMoney;
    public float minimumHappiness;
    public float maximumHappiness;

    public float effectOnCure;
    public float effectOnCureSpeed;
    public float effectOnPlague;
    public float effectOnPlagueSpeed;
    public float effectOnMoney;
    public float effectOnHappiness;

    public bool RunEvent()
    {
        if ((Manager.Instance.cure >= minimumCure && Manager.Instance.cure < maximumCure) &&
            (Manager.Instance.cureSpeed >= minimumCureSpeed && Manager.Instance.cureSpeed < maximumCureSpeed) &&
            (Manager.Instance.plague >= minimumPlague && Manager.Instance.plague < maximumPlague) &&
            (Manager.Instance.plagueSpeed >= minimumPlagueSpeed && Manager.Instance.plagueSpeed < maximumPlagueSpeed) &&
            (Manager.Instance.money >= minimumMoney && Manager.Instance.money < maximumMoney) &&
            (Manager.Instance.populationHappiness >= minimumHappiness && Manager.Instance.populationHappiness < maximumHappiness))
        {
            Manager.Instance.ChangeCure(effectOnCure);
            Manager.Instance.ChangeCureSpeed(effectOnCureSpeed);
            Manager.Instance.ChangePlague(effectOnPlague);
            Manager.Instance.ChangePlagueSpeed(effectOnPlagueSpeed);
            Manager.Instance.ChangeMoney(effectOnMoney);
            Manager.Instance.ChangePopulationHappiness(effectOnHappiness);

            return true;
        }
        else
        {
            return false;
        }
    }
}
