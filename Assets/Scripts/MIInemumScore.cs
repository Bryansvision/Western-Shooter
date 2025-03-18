using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class MIInemumScore : MonoBehaviour
{
    public int currentScore;
    public int stageMinimum;
    public int targetsLeft;
    public GameObject levelTracker;
    private string trackerName;
    public RaycastCursorU theGun;
    public spawnerScript[] spawnerScripts;
    public spawnerScript activeScript;
    public bool readyToFire;
    public GameObject winScreen;
    private bool wereDone = true;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI result;
    [SerializeField] TextMeshProUGUI ammunition;

    [System.Obsolete]
    void Start()
    {
        readyToFire = true;

       spawnerScripts = FindObjectsOfType(typeof(spawnerScript)) as spawnerScript[];
       levelTracker = GameObject.FindGameObjectWithTag("LevelTracker");
       trackerName = levelTracker.name;
       currentScore = 0;
       stageMinimum = 10;
       theGun = FindFirstObjectByType<RaycastCursorU>();
        switch (trackerName)
        {

            case "TestLevel":
                stageMinimum = 10;
                targetsLeft = 15;
                break;

        }
    }



   
    void Update()
    {
       text.text = currentScore.ToString() + "/" + stageMinimum.ToString();
       result.text = currentScore.ToString() + "/" + stageMinimum.ToString();
        ammunition.text = "Ammunition:" + theGun.Ammo;

        if (readyToFire && wereDone)
        {
            activeScript = spawnerScripts[Random.Range(0, spawnerScripts.Length)];
            activeScript.spawnAnItem();
        }

       

        if (targetsLeft <= 0)
        {
            winScreen.gameObject.SetActive(true);
            wereDone = false;
        }
    }
}
