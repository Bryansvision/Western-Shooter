using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using WiimoteApi;
using UnityEngine.SceneManagement;

public class TESTMII : MonoBehaviour
{

    public Wiimote mote;
    public bool letGoOfButton = false;
    public float motion;
    public float[] pointer;
    public bool Failsafe = false;
    private static GameObject instance;
    private float offset = 2000;

    void Start()
    {
        if (instance == null)
        {
            instance = this.gameObject;
            Object.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
            GetAWiimote();
        
    }


    void Update()
    {
        if (WiimoteManager.HasWiimote())
        {

            //if (mote.Button.home)
            //{
            //    mote.Accel.CalibrateAccel(AccelCalibrationStep.A_BUTTON_UP);
            //}

            if (mote != null)
            {
                if (mote.Button.b)
                {
                    letGoOfButton = true;
                }
                else if (!mote.Button.b)
                {
                    letGoOfButton = false;
                }

                float[] accel = mote.Accel.GetCalibratedAccelData();
                float[] zeroP = mote.Accel.GetAccelZeroPoints();

                motion = accel[0] - 0.3f;
                float motionMinus = -accel[1] + 0.3f;

            }

            pointer = mote.Ir.GetPointingPosition();

            if (Failsafe)
            {
                CallibratePointer();
            }


            if (Input.GetKeyDown(KeyCode.Escape) && Failsafe == false)
            {
                Failsafe = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && Failsafe != false)
            {

                Failsafe = false;
            }

        }
        else
        {
            SceneManager.LoadScene(7);
        }


    }

    public void CallibratePointer()
    {
        Mouse.current.WarpCursorPosition(new Vector2(-pointer[1] * offset + Screen.width, pointer[0] * offset));
    }

    public void GetAWiimote()
    {
        StartCoroutine(activate());
        WiimoteManager.FindWiimotes();
        mote = WiimoteManager.Wiimotes[0];
        mote.SendPlayerLED(true, true, true, true);
        mote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL_EXT16);
        mote.Accel.CalibrateAccel(AccelCalibrationStep.A_BUTTON_UP);
        mote.SetupIRCamera(IRDataType.BASIC);
        mote.Ir.GetProbableSensorBarIR();
    }

    public IEnumerator activate()
    {
        yield return new WaitUntil(() => WiimoteManager.HasWiimote());
        mote = WiimoteManager.Wiimotes[0];
    }
}
