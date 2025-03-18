using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using WiimoteApi;

public class TESTMII : MonoBehaviour
{

    public Wiimote mote;
    public bool letGoOfButton = false;
    public float motion;
    public float[] pointer;
    public bool Failsafe = false;

    void Start()
    {
        StartCoroutine(activate());
        WiimoteManager.FindWiimotes();
        mote = WiimoteManager.Wiimotes[0];
        mote.SendPlayerLED(true,true,true,true);
        mote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL_EXT16);
        mote.Accel.CalibrateAccel(AccelCalibrationStep.A_BUTTON_UP);
        mote.SetupIRCamera(IRDataType.BASIC);
        mote.Ir.GetProbableSensorBarIR();
       
       
    }

   
    void Update()
    {
       if(WiimoteManager.HasWiimote())
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
                else if (!mote.Button.b) { 
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
                Mouse.current.WarpCursorPosition(new Vector2(-pointer[1] * 2000 + Screen.width, pointer[0] * 2000));
            }


            if (Input.GetKeyDown(KeyCode.Escape)   && Failsafe == false)
            {
                Failsafe = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape)  && Failsafe != false) {
            
                Failsafe = false;
            }

        }
       

    }

    public IEnumerator activate()
    {
        yield return new WaitUntil(() => WiimoteManager.HasWiimote());
        mote = WiimoteManager.Wiimotes[0];
    }
}
