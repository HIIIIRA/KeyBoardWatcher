using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serial : MonoBehaviour
{
    //SerialHandler.cのクラス
    public SerialHandler serialHandler;
    private string message;
    private string preMessage = string.Empty;

    // Start is called before the first frame update
    void Start()
    {

    }
    void FixedUpdate()
    {
        if(message != preMessage)
        {
            serialHandler.Write(message);
            Debug.Log(message + " send");
            preMessage = message;
        } else
        {
            preMessage = message;
        }
    }
    public void SendKeyId(string keyId)
    {
        message = keyId;
    }
}
