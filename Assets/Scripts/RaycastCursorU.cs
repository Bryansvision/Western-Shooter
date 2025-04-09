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
    public GameObject explode;
    [SerializeField]
    private GameObject Boomer;
    private RawImage LightFlash;
    private GameObject explodingInstance;
    private AudioSource gunnerSound;
    [SerializeField]
    private AudioSource glassersound;
    [SerializeField] private Button selectedButton;

    void Start()
    {
        SceneManager.sceneLoaded += startSwitch;
        gunnerSound = GetComponent<AudioSource>();
        miiScore = FindFirstObjectByType<MIInemumScore>();
        WIImote = FindFirstObjectByType<TESTMII>();
    }

    void startSwitch(Scene scene, LoadSceneMode mode)
    {
        Boomer = GameObject.FindWithTag("FlashBoom");
        LightFlash = Boomer.GetComponent<RawImage>();
        miiScore = FindFirstObjectByType<MIInemumScore>();
        WIImote = FindFirstObjectByType<TESTMII>();
        Ammo = 4;
    }

  
    

    void Update()
    {
        if (WIImote.mote == null)
        {
            SceneManager.LoadScene(7);
        }

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
                gunnerSound.Play();
                StartCoroutine(BoomAndBack());

            }else
            {
                return;
            }
        
            if (Physics.Raycast(ray, out hit, 100) && Ammo >= 0) {
                if (hit.transform.tag == ("Target")){
                    Destroy(hit.transform.gameObject);
                    glassersound.Play();
                    miiScore.currentScore += 1;
                    miiScore.targetsLeft -= 1;
                    miiScore.readyToFire = true;
                    explodingInstance = Instantiate(explode,hit.transform.position,Quaternion.identity);
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

        IEnumerator BoomAndBack()
        {
            LightFlash.enabled = true;
            yield return new WaitForSeconds(0.2f);
            LightFlash.enabled = false; 
            yield break;
        }
    }
}
