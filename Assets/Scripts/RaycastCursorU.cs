using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaycastCursorU : MonoBehaviour
{

    Camera theCamera;
    public MIInemumScore miiScore;
    public TESTMII WIImote;
    public int Ammo = 4;
    private float cooldown;
    [SerializeField] private Button selectedButton;

    void Start()
    {
        SceneManager.sceneLoaded += startSwitch;
        miiScore = FindFirstObjectByType<MIInemumScore>();
        WIImote = FindFirstObjectByType<TESTMII>();
    }

    void startSwitch(Scene scene, LoadSceneMode mode)
    {
        miiScore = FindFirstObjectByType<MIInemumScore>();
        WIImote = FindFirstObjectByType<TESTMII>();
        Ammo = 4;
    }

  
    

    void Update()
    {

        theCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 100f;
        mousePosition = theCamera.ScreenToWorldPoint(mousePosition);
        Debug.DrawRay(transform.position, mousePosition - transform.position,Color.blue);
   

        if (Input.GetMouseButtonDown(0) || WIImote.mote.Button.b && WIImote.letGoOfButton == true)
        {

            Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Ammo > 0 && cooldown <= 0) {
                Ammo -= 1;
                cooldown = 90;

            }else
            {
                return;
            }
        
            if (Physics.Raycast(ray, out hit, 100) && Ammo >= 0) {
                if (hit.transform.tag == ("Target")){
                    Destroy(hit.transform.gameObject);
                    miiScore.currentScore += 1;
                    miiScore.targetsLeft -= 1;
                    miiScore.readyToFire = true;
                    if (hit.transform.parent != null)
                    {
                        Destroy(hit.transform.parent.gameObject);
                    }


                }
                else if(hit.collider.tag == ("Button") || hit.collider.tag == ("ContinueButton"))
                {
                    selectedButton = hit.collider.gameObject.GetComponent<Button>();
                    selectedButton.onClick.Invoke();
                }
                
            }
        }

        if (Input.GetMouseButtonDown(2) || WIImote.motion > 0.6f)
        {
            Ammo = 4;
        }
        if (cooldown > 0) {
            cooldown--;
        }

     
       
    
    }
}
