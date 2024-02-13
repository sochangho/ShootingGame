using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartScene : MonoBehaviour
{
    [SerializeField] private Button button;

    public void Awake()
    {
        button.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        Debug.Log("Ω√¿€");
        SceneManager.LoadScene("GameScene");
    }


}
