using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCleanupCrew : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
       yield return new WaitForSeconds(0.9f);
       Destroy(gameObject);
    }
   
}
