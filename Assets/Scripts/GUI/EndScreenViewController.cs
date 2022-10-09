using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class EndScreenViewController
{
    public EndScreenView view;

    public void ShowVictory()
    {
        view.message.text = "Victory!";
        view.message.color = Color.yellow;
        
        view.gameObject.SetActive(true);
    }

    public void ShowDeath()
    {
        view.message.text = "Death!";
        view.message.color = Color.red;
        
        view.gameObject.SetActive(true);
    }

    public void Hide()
    {
        SceneManager.LoadScene($"Main");
    }
}
