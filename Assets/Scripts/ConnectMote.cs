using UnityEngine;
using WiimoteApi;
using UnityEngine.SceneManagement;

public class ConnectMote : MonoBehaviour
{

    public TESTMII connecterMote;


    private void Start()
    {
        connecterMote = FindFirstObjectByType<TESTMII>();
    }

    void Update()
    {

        if (WiimoteManager.HasWiimote())
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            connecterMote.GetAWiimote();
        }
        
    }
}
