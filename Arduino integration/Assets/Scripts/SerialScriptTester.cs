using UnityEngine;
using System.Collections;
using System.IO.Ports;
public class SerialScriptTester : MonoBehaviour
{
    public GameObject player;
    public GameObject GreenColl;
    public GameObject YellowColl;
    public GameObject RedColl;
    public GameObject MainCollider;
    SerialPort sp;
    float next_time; int ii = 0;



     void OnTriggerEnter(Collider playerCollision)
    {
        //Debug.Log("something was hit");
        if (playerCollision.gameObject == player)
        {
            if(MainCollider == GreenColl)
            {
                Debug.Log("Green was hit");
                OpenSerialPort(4);
            }else if (MainCollider == YellowColl)
            {
                Debug.Log("Yellow was hit");
                OpenSerialPort(6);
            }else if (MainCollider == RedColl)
            {
                Debug.Log("Red was hit");
                OpenSerialPort(5);
            }
        }
    }




    // Use this for initialization
    void Start()
    {
        string the_com = "";
        next_time = Time.time;

        foreach (string mysps in SerialPort.GetPortNames())
        {
            print(mysps);
            if (mysps != "COM1") { the_com = mysps; break; }
        }
        sp = new SerialPort("\\\\.\\" + the_com, 9600);
        if (!sp.IsOpen)
        {
            print("Opening " + the_com + ", baud 9600");
            sp.Open();
            sp.ReadTimeout = 100;
            sp.Handshake = Handshake.None;
            if (sp.IsOpen) { print("Open"); }
        }
        

    }
    // Update is called once per frame
    void Update()
    {
         
        


        if (Time.time > next_time)
        {
            if (!sp.IsOpen)
            {
                sp.Open();
                print("opened sp");
            }
            if (sp.IsOpen)
            {
                print("Writing " + ii);
                sp.Write((ii.ToString()));
            }
            next_time = Time.time + 5;
            if (++ii > 9) ii = 0;
        }
    }
   public void OpenSerialPort(int LightInt)
   {
        string the_com = "";
        next_time = Time.time;

        if (!sp.IsOpen)
        {
            print("Opening " + the_com + ", baud 9600");
            sp.Open();
            sp.ReadTimeout = 100;
            sp.Handshake = Handshake.None;
            if (sp.IsOpen) { print("Open"); }
            if (sp.IsOpen) { print(LightInt); }
        }

    }
}
