using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DisplayTask : MonoBehaviour
{
    public CanvasGroup task;
    public CanvasGroup lifeBar;
    public bool isActive = true;

    private void Start()
    {
        DOTween.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isActive = !isActive;
            if (isActive == true)
            {
                ShowTask();
                Time.timeScale = 0f; // Detener el tiempo
                Camera.main.GetComponent<CameraOrbit>().enabled = false;
            }
            else
            {
                HideTask();
                Time.timeScale = 1f; // Detener el tiempo
                Camera.main.GetComponent<CameraOrbit>().enabled = true;
            }
        }
    }

    public void ShowTask()
    {
        task.DOFade(1, 0.5f).SetUpdate(true); // Ignorar Time.timeScale
        HideLifeBar();
    }

    public void HideTask()
    {
        task.DOFade(0, 0.5f).SetUpdate(true); // Ignorar Time.timeScale
        ShowLifeBar();
    }

    public void ShowLifeBar()
    {
        lifeBar.DOFade(1, 0.5f).SetUpdate(true); // Ignorar Time.timeScale
    }

    public void HideLifeBar()
    {
        lifeBar.DOFade(0, 0.5f).SetUpdate(true); // Ignorar Time.timeScale
    }


}
