using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class SecondTarget : BaseTarget
{
    private Vector3 myPosition;
    private Vector3 myDestination;
    private float perc;
    private Animator plateAnimator;
    public MIInemumScore gameManager;
    
    new void Start()
    {
        base.Start();
        myPosition = transform.position;
        myDestination = myPosition;
        plateAnimator = GetComponent<Animator>();
        gameManager = FindFirstObjectByType(typeof(MIInemumScore)) as MIInemumScore;
        gameManager.readyToFire = false;


        StartCoroutine(FlyOff());

        if (selectedBehaviour == 1)
        {
            plateAnimator.SetBool("Behaviour1", true);  
            myDestination.x -= 70;
        }
        else if (selectedBehaviour == 2) {
            plateAnimator.SetBool("Behaviour2",true);
            myDestination.x -= 70;
            myDestination.y -= 10;
        }
    }


    IEnumerator FlyOff()
    {

        perc = 0;


        while (perc < 1)
        {
            perc += Time.deltaTime / 2.5f;
            gameObject.transform.position = Vector3.Lerp(myPosition, myDestination, perc);
            yield return new WaitForEndOfFrame();

        }

        gameManager.targetsLeft -= 1;
        gameManager.readyToFire = true;
        Destroy(gameObject);

    }

    }
