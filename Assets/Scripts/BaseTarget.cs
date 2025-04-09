using Unity.VisualScripting;
using UnityEngine;

public class BaseTarget : MonoBehaviour
{
    public int selectedBehaviour;
    public AudioSource myDeathSound;
    public void Start() {
    myDeathSound = GetComponent<AudioSource>();
        if(Random.value > 0.5f)
        {
            selectedBehaviour = 1;
        }
        else
        {
            selectedBehaviour = 2;
        }

    }

  

}
