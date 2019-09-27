using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartCtrl : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // play 모드를 false
#elif UNITY_WEBPLAYER
        Application.OpenURL("https://google.com"); // 구글로 진입
#else
        Application.Quit(); // 앱 종료
#endif

    }
}
