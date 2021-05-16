using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("------Health------")]
    [SerializeField]
    private Image health_slider;

    [SerializeField]
    private Text health_num;

    [Header("------Score------")]
    [SerializeField]
    private Text score_num;
    private int current_score;
    float temp_score;

    [Header("------Energy------")]
    [SerializeField]
    private Text energy_num;
    [SerializeField]
    private Image energy_slider;
    private float current_energy = 0;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float health_percentage){
        StartCoroutine(UpdateHealthCoroutine(health_percentage));
        
    }

    public void UpdateScore(int score){
        StartCoroutine(UpdateScoreCoroutine(score));
    }

    public void UpdateEnergy(float energy_percentage){
        //energy_slider.fillAmount = energy_percentage;
        DOVirtual.Float(energy_slider.fillAmount, energy_percentage, 1, ChangeEnergy);
        //DOVirtual.Float(current_energy, energy_percentage, 2, ChangeEnergy);
        energy_num.text = current_energy.ToString("0") + "%";
    }

    void ChangeEnergy(float energy){
        energy_slider.fillAmount = energy;
        current_energy = energy * 100f;

    }

    IEnumerator UpdateScoreCoroutine(int score){
        temp_score = current_score;
        current_score += score;
        if (current_score % 45 == 0)
        {
            Time.timeScale = 0.1f;
            yield return new WaitForSecondsRealtime(1);
            Time.timeScale = 1f;
        }
        while(temp_score < current_score){
            DOVirtual.Float(temp_score, current_score, 1, ChengeScore);
            //temp_score = Mathf.Lerp(temp_score, current_score, Time.deltaTime);
            score_num.text = temp_score.ToString("0");
            yield return null;
        }
    }

    void ChengeScore(float score){
        temp_score = score;
    }

    IEnumerator UpdateHealthCoroutine(float health_percentage){
        while(health_slider.fillAmount > health_percentage){
            health_slider.fillAmount = Mathf.Lerp(health_slider.fillAmount, health_percentage, Time.deltaTime);
            health_num.text = (health_slider.fillAmount * 100).ToString("0.0") + "%";
            yield return null;
        }
    }
}
