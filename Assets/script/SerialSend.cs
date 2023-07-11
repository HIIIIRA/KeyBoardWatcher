using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialSend : MonoBehaviour
{
    //SerialHandler.cのクラス
    public SerialHandler serialHandler;
    int i = 0;

    void FixedUpdate() //ここは0.001秒ごとに実行される
    {
        Debug.Log(i);
        i++ ;   //iを加算していって1秒ごとに"1"のシリアル送信を実行
        ///*
        if (i == 100) //
        {
            serialHandler.Write("?");

        }
        if (i == 200) //
        {
            serialHandler.Write("A");

        }
        if (i == 400)
        {
            serialHandler.Write("W");
        }
        if (i == 500) //
        {
            serialHandler.Write("S");

        }
        if (i == 600)
        {
            i = 0;
        }
        //*/
        /*
        if (i < 1000) //
        {
            serialHandler.Write("?");

        }
        else if (i < 2000) //
        {
            serialHandler.Write("A");

        }
        else if (i < 4000)
        {
            serialHandler.Write("S");
        }
        else if (i < 5000) //
        {
            serialHandler.Write("W");

        }
        if (i == 6000)
        {
            i = 0;
        }
        */
    }
}