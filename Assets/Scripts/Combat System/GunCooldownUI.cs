using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunCooldownUI : MonoBehaviour
{
    [SerializeField]
    private Image cooldownBar;
    [SerializeField]
    private Gradient barColorGradient;

    private GunCooldownSystem gunCooldownSystem;

    private void Start()
    {
        gunCooldownSystem = GetComponent<GunCooldownSystem>();
    }

    private void Update()
    {
        float heatPercentage = gunCooldownSystem.CurrentHeat / gunCooldownSystem.MaxHeat;
        cooldownBar.fillAmount = heatPercentage;
        cooldownBar.color = barColorGradient.Evaluate(heatPercentage);
    }
}
