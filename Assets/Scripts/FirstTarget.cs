using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class FirstTarget : BaseTarget
{
    private float perc;
    private Vector3 currentPosition;
    private Vector3 endPosition;
    public MIInemumScore gameManager;
    public Animator bottleAnimator;

    new void Start()
    {
        base.Start();
        bottleAnimator = GetComponent<Animator>();  
        gameManager = FindFirstObjectByType(typeof( MIInemumScore )) as MIInemumScore;
        currentPosition = transform.position;
        currentPosition.y += 2;
        endPosition = currentPosition;
        gameManager.readyToFire = false;
        StartCoroutine(UpAndDown());
        
        if(selectedBehaviour == 1)
        {
            bottleAnimator.SetBool("Behaviour1", true);
            endPosition.y += 9;
        }
        else if(selectedBehaviour == 2) 
        {
            endPosition.y += 15;
            bottleAnimator.SetBool("Behaviour2",true);
        }
    }


    IEnumerator UpAndDown()
    {
      
        perc = 0;
        

        while(perc < 1)
        {
            perc += Time.deltaTime / 1;
            gameObject.transform.position = Vector3.Lerp(currentPosition, endPosition, perc);
            yield return new WaitForEndOfFrame();
        
        }

        perc = 0;

        while (perc < 1)
        {
            perc += Time.deltaTime / 1;
            gameObject.transform.position = Vector3.Lerp(endPosition, currentPosition, perc);
            yield return new WaitForEndOfFrame();

        }

        gameManager.targetsLeft -= 1;
        gameManager.readyToFire = true;
        Destroy(gameObject);
    }
}
