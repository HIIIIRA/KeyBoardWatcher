using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class Recorder : MonoBehaviour
{
    private KeyController keyAcontroller;
    private KeyController keyBcontroller;
    private KeyController keyCcontroller;
    private KeyController keyDcontroller;
    private KeyController keyEcontroller;
    private KeyController keyFcontroller;
    private KeyController keyGcontroller;
    private KeyController keyHcontroller;
    private KeyController keyIcontroller;
    private KeyController keyJcontroller;
    private KeyController keyKcontroller;
    private KeyController keyLcontroller;
    private KeyController keyMcontroller;
    private KeyController keyNcontroller;
    private KeyController keyOcontroller;
    private KeyController keyPcontroller;
    private KeyController keyQcontroller;
    private KeyController keyRcontroller;
    private KeyController keyScontroller;
    private KeyController keyTcontroller;
    private KeyController keyUcontroller;
    private KeyController keyVcontroller;
    private KeyController keyWcontroller;
    private KeyController keyXcontroller;
    private KeyController keyYcontroller;
    private KeyController keyZcontroller;
    

    //　現在記憶しているかどうか
    private bool isRecord;
    //　保存するデータの最大数
	[SerializeField]
	private int maxDataNum = 10000;
	//　記録間隔
	[SerializeField]
	private float recordDuration = 0.005f;
    //　経過時間
    private float elapsedTime = 0f;
    //　ゴーストデータ
    private GhostData ghostData;
    //　再生中かどうか
	public bool isPlayBack;
    //　保存先フォルダ
	private string saveDataFolder = "/Projects/Ghost";
	//　保存ファイル名
	private string saveFileName = "/ghostdata.dat";
    // Start is called before the first frame update

    void Start() {
        keyAcontroller =  GameObject.Find("KeyA").gameObject.GetComponent<KeyController>();
        keyBcontroller =  GameObject.Find("KeyB").gameObject.GetComponent<KeyController>();
        keyCcontroller =  GameObject.Find("KeyC").gameObject.GetComponent<KeyController>();
        keyDcontroller =  GameObject.Find("KeyD").gameObject.GetComponent<KeyController>();
        keyEcontroller =  GameObject.Find("KeyE").gameObject.GetComponent<KeyController>();
        keyFcontroller =  GameObject.Find("KeyF").gameObject.GetComponent<KeyController>();
        keyGcontroller =  GameObject.Find("KeyG").gameObject.GetComponent<KeyController>();
        keyHcontroller =  GameObject.Find("KeyH").gameObject.GetComponent<KeyController>();
        keyIcontroller =  GameObject.Find("KeyI").gameObject.GetComponent<KeyController>();
        keyJcontroller =  GameObject.Find("KeyJ").gameObject.GetComponent<KeyController>();
        keyKcontroller =  GameObject.Find("KeyK").gameObject.GetComponent<KeyController>();
        keyLcontroller =  GameObject.Find("KeyL").gameObject.GetComponent<KeyController>();
        keyMcontroller =  GameObject.Find("KeyM").gameObject.GetComponent<KeyController>();
        keyNcontroller =  GameObject.Find("KeyN").gameObject.GetComponent<KeyController>();
        keyOcontroller =  GameObject.Find("KeyO").gameObject.GetComponent<KeyController>();
        keyPcontroller =  GameObject.Find("KeyP").gameObject.GetComponent<KeyController>();
        keyQcontroller =  GameObject.Find("KeyQ").gameObject.GetComponent<KeyController>();
        keyRcontroller =  GameObject.Find("KeyR").gameObject.GetComponent<KeyController>();
        keyScontroller =  GameObject.Find("KeyS").gameObject.GetComponent<KeyController>();
        keyTcontroller =  GameObject.Find("KeyT").gameObject.GetComponent<KeyController>();
        keyUcontroller =  GameObject.Find("KeyU").gameObject.GetComponent<KeyController>();
        keyVcontroller =  GameObject.Find("KeyV").gameObject.GetComponent<KeyController>();
        keyWcontroller =  GameObject.Find("KeyW").gameObject.GetComponent<KeyController>();
        keyXcontroller =  GameObject.Find("KeyX").gameObject.GetComponent<KeyController>();
        keyYcontroller =  GameObject.Find("KeyY").gameObject.GetComponent<KeyController>();
        keyZcontroller =  GameObject.Find("KeyZ").gameObject.GetComponent<KeyController>();
    }
private class GhostData {
        //　位置のリスト
        public List<bool> ALists = new List<bool>();
        public List<bool> BLists = new List<bool>();
        public List<bool> CLists = new List<bool>();
        public List<bool> DLists = new List<bool>();
        public List<bool> ELists = new List<bool>();
        public List<bool> FLists = new List<bool>();
        public List<bool> GLists = new List<bool>();
        public List<bool> HLists = new List<bool>();
        public List<bool> ILists = new List<bool>();
        public List<bool> JLists = new List<bool>();
        public List<bool> KLists = new List<bool>();
        public List<bool> LLists = new List<bool>();
        public List<bool> MLists = new List<bool>();
        public List<bool> NLists = new List<bool>();
        public List<bool> OLists = new List<bool>();
        public List<bool> PLists = new List<bool>();
        public List<bool> QLists = new List<bool>();
        public List<bool> RLists = new List<bool>();
        public List<bool> SLists = new List<bool>();
        public List<bool> TLists = new List<bool>();
        public List<bool> ULists = new List<bool>();
        public List<bool> VLists = new List<bool>();
        public List<bool> WLists = new List<bool>();
        public List<bool> XLists = new List<bool>();
        public List<bool> YLists = new List<bool>();
        public List<bool> ZLists = new List<bool>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isRecord) {
    
            elapsedTime += Time.deltaTime;
    
            if (elapsedTime >= recordDuration) {
                GhostDataAdd();
                elapsedTime = 0f;
    
                //　データ保存数が最大数を超えたら記録をストップ
                if (ghostData.WLists.Count >= maxDataNum) {
                    StopRecord ();
                }
            }
        }
    }
    //　キャラクターデータの保存
    public void StartRecord() {
        //　保存する時はゴーストの再生を停止
        StopAllCoroutines ();
        StopGhost ();
        isRecord = true;
        elapsedTime = 0f;
        ghostData = new GhostData ();
        Debug.Log ("StartRecord");
    }

    //　キャラクターデータの保存の停止
    public void StopRecord() {
        isRecord = false;
        Debug.Log ("StopRecord");
    }
    
    //　ゴーストの再生ボタンを押した時の処理
    public void StartGhost() {
        Debug.Log ("StartGhost");
        if (ghostData == null) {
            Debug.Log ("ゴーストデータがありません");
        } else {
            isRecord = false;
            isPlayBack = true;
            GhostDataReset();
            StartCoroutine (PlayBack ());
        }
    }
    
    //　ゴーストの停止
    public void StopGhost() {
        Debug.Log ("StopGhost");
        StopAllCoroutines ();
        isPlayBack = false;
    }

    //　ゴーストの再生
    IEnumerator PlayBack() {
    
        var i = 0;
        Debug.Log ("データ数: " + ghostData.WLists.Count);
    
        while (isPlayBack) {
    
            yield return new WaitForSeconds (recordDuration);
    
            keyAcontroller.pushRecord = ghostData.ALists[i];
            keyBcontroller.pushRecord = ghostData.BLists[i];
            keyCcontroller.pushRecord = ghostData.CLists[i];
            keyDcontroller.pushRecord = ghostData.DLists[i];
            keyEcontroller.pushRecord = ghostData.ELists[i];
            keyFcontroller.pushRecord = ghostData.FLists[i];
            keyGcontroller.pushRecord = ghostData.GLists[i];
            keyHcontroller.pushRecord = ghostData.HLists[i];
            keyIcontroller.pushRecord = ghostData.ILists[i];
            keyJcontroller.pushRecord = ghostData.JLists[i];
            keyKcontroller.pushRecord = ghostData.KLists[i];
            keyLcontroller.pushRecord = ghostData.LLists[i];
            keyMcontroller.pushRecord = ghostData.MLists[i];
            keyNcontroller.pushRecord = ghostData.NLists[i];
            keyOcontroller.pushRecord = ghostData.OLists[i];
            keyPcontroller.pushRecord = ghostData.PLists[i];
            keyQcontroller.pushRecord = ghostData.QLists[i];
            keyRcontroller.pushRecord = ghostData.RLists[i];
            keyScontroller.pushRecord = ghostData.SLists[i];
            keyTcontroller.pushRecord = ghostData.TLists[i];
            keyUcontroller.pushRecord = ghostData.ULists[i];
            keyVcontroller.pushRecord = ghostData.VLists[i];
            keyWcontroller.pushRecord = ghostData.WLists[i];
            keyXcontroller.pushRecord = ghostData.XLists[i];
            keyYcontroller.pushRecord = ghostData.YLists[i];
            keyZcontroller.pushRecord = ghostData.ZLists[i];
            i++;
    
            //　保存データ数を超えたら最初から再生
            if (i >= ghostData.WLists.Count) {
                GhostDataReset();
                i = 0;
            }
        }
    }

 
public void Save()
{
    if (ghostData != null)
    {
        //　GhostDataクラスをJSONデータに書き換え
        var data = JsonUtility.ToJson(ghostData);
        //　ゲームフォルダにファイルを作成
        File.WriteAllText(Application.dataPath + saveDataFolder + saveFileName, data);
        Debug.Log("ゴーストデータをセーブしました");
    }
}

public void Load()
{

    if (File.Exists(Application.dataPath + saveDataFolder + saveFileName))
    {
        string readAllText = File.ReadAllText(Application.dataPath + saveDataFolder + saveFileName);
        //　ghostDataに読み込んだデータを書き込む
        if (ghostData == null)
        {
            ghostData = new GhostData();
        }
        JsonUtility.FromJsonOverwrite(readAllText, ghostData);
        Debug.Log("ゴーストデータをロードしました。");
    }
}

void OnApplicationQuit()
{
    Debug.Log("アプリケーション終了");
    Save();
}

    private void GhostDataAdd()
    {
        ghostData.ALists.Add(keyAcontroller.isPushing);
        ghostData.BLists.Add(keyBcontroller.isPushing);
        ghostData.CLists.Add(keyCcontroller.isPushing);
        ghostData.DLists.Add(keyDcontroller.isPushing);
        ghostData.ELists.Add(keyEcontroller.isPushing);
        ghostData.FLists.Add(keyFcontroller.isPushing);
        ghostData.GLists.Add(keyGcontroller.isPushing);
        ghostData.HLists.Add(keyHcontroller.isPushing);
        ghostData.ILists.Add(keyIcontroller.isPushing);
        ghostData.JLists.Add(keyJcontroller.isPushing);
        ghostData.KLists.Add(keyKcontroller.isPushing);
        ghostData.LLists.Add(keyLcontroller.isPushing);
        ghostData.MLists.Add(keyMcontroller.isPushing);
        ghostData.NLists.Add(keyNcontroller.isPushing);
        ghostData.OLists.Add(keyOcontroller.isPushing);
        ghostData.PLists.Add(keyPcontroller.isPushing);
        ghostData.QLists.Add(keyQcontroller.isPushing);
        ghostData.RLists.Add(keyRcontroller.isPushing);
        ghostData.SLists.Add(keyScontroller.isPushing);
        ghostData.TLists.Add(keyTcontroller.isPushing);
        ghostData.ULists.Add(keyUcontroller.isPushing);
        ghostData.VLists.Add(keyVcontroller.isPushing);
        ghostData.WLists.Add(keyWcontroller.isPushing);
        ghostData.XLists.Add(keyXcontroller.isPushing);
        ghostData.YLists.Add(keyYcontroller.isPushing);
        ghostData.ZLists.Add(keyZcontroller.isPushing);
    }

    private void GhostDataReset()
    {
        keyAcontroller.pushRecord = ghostData.ALists[0];
        keyBcontroller.pushRecord = ghostData.BLists[0];
        keyCcontroller.pushRecord = ghostData.CLists[0];
        keyDcontroller.pushRecord = ghostData.DLists[0];
        keyEcontroller.pushRecord = ghostData.ELists[0];
        keyFcontroller.pushRecord = ghostData.FLists[0];
        keyGcontroller.pushRecord = ghostData.GLists[0];
        keyHcontroller.pushRecord = ghostData.HLists[0];
        keyIcontroller.pushRecord = ghostData.ILists[0];
        keyJcontroller.pushRecord = ghostData.JLists[0];
        keyKcontroller.pushRecord = ghostData.KLists[0];
        keyLcontroller.pushRecord = ghostData.LLists[0];
        keyMcontroller.pushRecord = ghostData.MLists[0];
        keyNcontroller.pushRecord = ghostData.NLists[0];
        keyOcontroller.pushRecord = ghostData.OLists[0];
        keyPcontroller.pushRecord = ghostData.PLists[0];
        keyQcontroller.pushRecord = ghostData.QLists[0];
        keyRcontroller.pushRecord = ghostData.RLists[0];
        keyScontroller.pushRecord = ghostData.SLists[0];
        keyTcontroller.pushRecord = ghostData.TLists[0];
        keyUcontroller.pushRecord = ghostData.ULists[0];
        keyVcontroller.pushRecord = ghostData.VLists[0];
        keyWcontroller.pushRecord = ghostData.WLists[0];
        keyXcontroller.pushRecord = ghostData.XLists[0];
        keyYcontroller.pushRecord = ghostData.YLists[0];
        keyZcontroller.pushRecord = ghostData.ZLists[0];
    }
    
}


