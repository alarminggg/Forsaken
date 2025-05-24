using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPotionCountdown : MonoBehaviour
{
    public TMP_Text cooldownText;

    private float cooldownDuration = 10f;
    private float lastHealTime = -Mathf.Infinity;
    private bool isOnCooldown = false;

    void Update()
    {
        if (isOnCooldown)
        {
            float timeRemaining = (lastHealTime + cooldownDuration) - Time.time;

            if (timeRemaining > 0)
            {
                cooldownText.text =  Mathf.Ceil(timeRemaining).ToString() ;
            }
            else
            {
                cooldownText.text = "Press 2";
                isOnCooldown = false;
            }
        }
    }

    public bool CanHeal()
    {
        return Time.time >= lastHealTime + cooldownDuration;
    }

    public void TriggerCooldown()
    {
        lastHealTime = Time.time;
        isOnCooldown = true;
    }

    public void SetCooldownDuration(float duration)
    {
        cooldownDuration = duration;
    }
}
