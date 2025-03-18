using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    public bool canDispense;
    public GameObject spawnTarget;

    public void spawnAnItem()
    {
        Instantiate(spawnTarget, gameObject.transform.position, Quaternion.identity);
    }

   
}
