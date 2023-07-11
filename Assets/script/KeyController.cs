using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    public KeyCode valueKey;
    private string keyCodeString;
    Image image;
    public Color right;
    public Color dark;
    public bool isPushing;
    public bool pushRecord;
    public Recorder recorder;
    public Serial serial;

    void Start()
    {
        image = this.GetComponent<Image>();
        recorder = GameObject.Find("KeyLamps").GetComponent<Recorder>();
        serial = GameObject.Find("KeyLamps").GetComponent<Serial>();
        keyCodeString = valueKey.ToString();
    }

    void Update () {

        if (recorder.isPlayBack) {
            //record再生用
            if (pushRecord){
                TurnOn();
            } else {
                TurnOff();
            }
        } else {
            if (Input.GetKey(valueKey)){
                TurnOn();
            } else {
                TurnOff();
            }
        }
    }

    void TurnOn() {
        image.color = right;
        serial.SendKeyId(keyCodeString);
        isPushing = true;
    }

    void TurnOff() {
        image.color = dark;
        isPushing = false;
    }
}
