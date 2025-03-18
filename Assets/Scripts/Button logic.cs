using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonlogic : MonoBehaviour
{
  
    public void SendtoControls()
    {
        SceneManager.LoadScene(2);
    }

    public void SendtoLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void SendtoTitle()
    {
        SceneManager.LoadScene(0);
    }
}
