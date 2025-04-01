using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;

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
    private GameObject[] currentTargets;
    public bool slowDown = false;
    [SerializeField] private GameObject continueButton;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI result;
    [SerializeField] TextMeshProUGUI ammunition;
    [SerializeField] TextMeshProUGUI passed;

    [System.Obsolete]
    void Start()
    {
        readyToFire = true;

       spawnerScripts = FindObjectsOfType(typeof(spawnerScript)) as spawnerScript[];
       levelTracker = GameObject.FindGameObjectWithTag("LevelTracker");
       continueButton.SetActive(false);
       trackerName = levelTracker.name;
       currentScore = 0;
       stageMinimum = 10;
       theGun = FindFirstObjectByType<RaycastCursorU>();
        switch (trackerName)
        {

            case "TestLevel":
                stageMinimum = 10;
                targetsLeft = 15;
                slowDown = false;
                break;
            case "Level2":
                stageMinimum = 15;
                targetsLeft = 18;
                slowDown = false;
                break;
            case "Level3":
                stageMinimum = 20;
                targetsLeft = 22;
                slowDown = true;
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

        if(currentScore >= stageMinimum)
        {
            passed.text = "congrats you beat the minimum you can move on !";
            continueButton.SetActive(true);
        }else
        {
            passed.text = "to bad you didnt meet the minimum go on try again";
        }
       

        if (targetsLeft <= 0)
        {
            winScreen.gameObject.SetActive(true);
            wereDone = false;
            currentTargets = GameObject.FindGameObjectsWithTag("Target");
            for (int i = 0; i < currentTargets.Length; i++) {
              Destroy(currentTargets[i]);   
            }
        }
    }
}
