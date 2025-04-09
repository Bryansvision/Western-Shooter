using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonlogic : MonoBehaviour
{
  
    public void SendtoControls()
    {
        SceneManager.LoadScene(3);
    }

    public void SendtoLevel1()
    {
        SceneManager.LoadScene(2);
    }

    public void SendtoTitle()
    {
        SceneManager.LoadScene(1);
        
    }

    public void Sendtolvl2()
    {
        SceneManager.LoadScene(4);
    }

    public void Sendtolvl3()
    {
        SceneManager.LoadScene(5);
    }

    public void sendtoEnd()
    {
        SceneManager.LoadScene(6);
    }

    public void sendtoCredits()
    {
        SceneManager.LoadScene(8);
    }
    public void retry()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuittheGame()
    {
        Application.Quit();
    }
}
