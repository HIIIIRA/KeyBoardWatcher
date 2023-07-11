using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Old_Recorder : MonoBehaviour
{
	//　操作キャラクター
	[SerializeField]
	private GhostChara ghostChara;
	// AnimatorController
	private Animator animator;
	//　現在記憶しているかどうか
	private bool isRecord;
	//　保存するデータの最大数
	[SerializeField]
	private int maxDataNum = 2000;
	//　記録間隔
	[SerializeField]
	private float recordDuration = 0.005f;
	//　Jumpキー
	private string animKey = "Jump";
	//　経過時間
	private float elapsedTime = 0f;
	//　ゴーストデータ
	private GhostData ghostData;
	//　再生中かどうか
	private bool isPlayBack;
	//　ゴースト用キャラ
	[SerializeField]
	private GameObject ghost;
	//　ゴーストデータが1周りした後の待ち時間
	[SerializeField]
	private float waitTime = 2f;
	//　保存先フォルダ
	private string saveDataFolder = "/Projects/Ghost";
	//　保存ファイル名
	private string saveFileName = "/ghostdata.dat";

	void Start() {
		animator = ghostChara.GetComponent<Animator> ();
	}

    //　ゴーストデータクラス
    [Serializable]
    private class GhostData {
        //　位置のリスト
        public List<Vector3> posLists = new List<Vector3>();
        //　角度リスト
        public List<Quaternion> rotLists = new List<Quaternion>();
        //　アニメーションパラメータSpeed値
        public List<float> speedLists = new List<float>();
        //　Jumpアニメーションリスト
        public List<bool> jumpAnimLists = new List<bool>();
    }
 
    // Update is called once per frame
    void Update () {
    
        if (isRecord) {
    
            elapsedTime += Time.deltaTime;
    
            if (elapsedTime >= recordDuration) {
                ghostData.posLists.Add (ghostChara.transform.position);
                ghostData.rotLists.Add (ghostChara.transform.rotation);
                ghostData.speedLists.Add(animator.GetFloat("Speed"));
    
                //　ジャンプデータの記憶
                if (Input.GetButtonDown (animKey)) {
                    ghostData.jumpAnimLists.Add (true);
                } else {
                    ghostData.jumpAnimLists.Add (false);
                }
    
                elapsedTime = 0f;
    
                //　データ保存数が最大数を超えたら記録をストップ
                if (ghostData.posLists.Count >= maxDataNum) {
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
            ghost.transform.position = ghostData.posLists [0];
            ghost.transform.rotation = ghostData.rotLists [0];
            ghost.SetActive (true);
            StartCoroutine (PlayBack ());
        }
    }
    
    //　ゴーストの停止
    public void StopGhost() {
        Debug.Log ("StopGhost");
        StopAllCoroutines ();
        isPlayBack = false;
        ghost.SetActive (false);
    }

    //　ゴーストの再生
    IEnumerator PlayBack() {
    
        var i = 0;
        var ghostAnimator = ghost.GetComponent<Animator> ();
    
        Debug.Log ("データ数: " + ghostData.posLists.Count);
    
        while (isPlayBack) {
    
            yield return new WaitForSeconds (recordDuration);
    
            ghost.transform.position = ghostData.posLists [i];
            ghost.transform.rotation = ghostData.rotLists [i];
            ghostAnimator.SetFloat("Speed", ghostData.speedLists[i]);
    
            if (ghostData.jumpAnimLists [i]) {
                ghostAnimator.SetTrigger ("Jump");
            }
    
            i++;
    
            //　保存データ数を超えたら最初から再生
            if (i >= ghostData.posLists.Count) {
                    
                ghostAnimator.SetFloat ("Speed", 0f);
                ghostAnimator.ResetTrigger ("Jump");
    
                //　アニメーション途中で終わった時用に待ち時間を入れる
                yield return new WaitForSeconds (waitTime);
    
                ghost.transform.position = ghostData.posLists [0];
                ghost.transform.rotation = ghostData.rotLists [0];
    
                i = 0;
            }
        }
    }
   
    public void Save() {
        if (ghostData != null) {
            //　GhostDataクラスをJSONデータに書き換え
            var data = JsonUtility.ToJson (ghostData);
            //　ゲームフォルダにファイルを作成
            File.WriteAllText (Application.dataPath + saveDataFolder + saveFileName, data);
            Debug.Log ("ゴーストデータをセーブしました");
        }
    }
    
    public void Load() {
    
        if (File.Exists (Application.dataPath + saveDataFolder + saveFileName)) {
            string readAllText = File.ReadAllText (Application.dataPath + saveDataFolder + saveFileName);
            //　ghostDataに読み込んだデータを書き込む
            if (ghostData == null) {
                ghostData = new GhostData ();
            }
            JsonUtility.FromJsonOverwrite (readAllText, ghostData);
            Debug.Log ("ゴーストデータをロードしました。");
        }
    }
    
    void OnApplicationQuit() {
        Debug.Log ("アプリケーション終了");
        Save ();
    }
 
}
