using System.Collections;
using UnityEngine;

public class ThirdTarget : MonoBehaviour
{
    public MIInemumScore gameManager;
    public Animator flippingAnimator;
    public AudioSource myDeath;
   

    void Start()
    {
        myDeath = GetComponent<AudioSource>();
        gameObject.transform.eulerAngles = new Vector3 (0f, 0f, 90f);
        flippingAnimator = GetComponent<Animator>();    
        gameManager = FindFirstObjectByType<MIInemumScore>();
        gameManager.readyToFire = false;
        flippingAnimator.SetTrigger("Behaviour1");
        StartCoroutine(targetsStanderds());
      

    }

    
   
    IEnumerator targetsStanderds()
    {
       
        yield return new WaitForSeconds(2f);
        gameManager.readyToFire = true;
        gameManager.targetsLeft -= 1;
        Destroy(gameObject);
       
    }

  
}
