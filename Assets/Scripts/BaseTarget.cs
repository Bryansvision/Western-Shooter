using Unity.VisualScripting;
using UnityEngine;

public class BaseTarget : MonoBehaviour
{
    public int selectedBehaviour;

    public void Start() {
    
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
