using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpehre : MonoBehaviour
{
    /// <summary>
    /// ステージ内にあるSuctionObjectタグを持つオブジェクトを格納するための配列.
    /// </summary>
    List<GameObject> SuctionObjectsArray = new List<GameObject>();
    GameObject m_StageClearManager;
    GameObject m_GrabMotion;
    GameObject m_ObjectAbsorbManager;
    bool m_bIsCollision = false;

    float m_fStageDirtinessdegree = 0.0f;

    // Use this for initialization
    void Start()
    {
        m_StageClearManager = GameObject.Find("StageClearManager");
        m_GrabMotion = GameObject.Find("Main Camera");
        m_ObjectAbsorbManager = GameObject.Find("ObjectAbsorbManager");
        // FindGameObjectsWithTagはGameObject[]型を返すためテンポラリとして宣言しているSuctionObjectsにキャッシュする.
        GameObject[] SuctionObjects = GameObject.FindGameObjectsWithTag("SuctionObject");

        // 上記で取得したオブジェクトをList型にAddしていく.
        foreach (GameObject obj in SuctionObjects)
        {
            SuctionObjectsArray.Add(obj);
            m_fStageDirtinessdegree = obj.GetComponent<StageObjects>().m_fDirtinessdegree;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // このオブジェクトが何にもぶつかっていない場合以下の処理は行わない.
        if (!m_bIsCollision)
            return;

        // オブジェクトのサイズに応じてステージオブジェクトに対する吸収率を変化させる.
        float absorptivity = transform.localScale.magnitude * 100.0f;

        // Startメソッドで取得した全オブジェクトに対し以下の処理を行う.
        foreach (GameObject obj in SuctionObjectsArray)
        {
            // オブジェクトからプレイヤーの生成したブラックホールへと向かうベクトルを取得.
            Vector3 toPosition = transform.position - obj.transform.position;
            // 取得したベクトルをオブジェクト自身のvelocityへと加えていく.
            // absorptivityというのは吸収率.
            obj.rigidbody.AddForce(toPosition.normalized * (absorptivity / Mathf.Max(1.0f, toPosition.magnitude)), ForceMode.Force);
        }

         //ThisDestroyメソッドのコルーチンを開始
        StartCoroutine("ThisDestroy");
    }

    void OnCollisionEnter(Collision col)
    {
        // 何かにぶつかった事を知らせるフラグを立てる.
        m_bIsCollision = true;
        // このオブジェクトの位置を動かさないようにする
        gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePosition;

        if (col.gameObject.tag == "SuctionObject")
        {
            m_ObjectAbsorbManager.GetComponent<ObjectAbsorbManager>().CollectObject(col.gameObject);
            // ぶつかったSuctionObjectから不潔度を取得し、清潔度を管理するCleanlinessへ値を渡す.
            Cleanliness.m_fCleanliness += col.gameObject.GetComponent<StageObjects>().m_fDirtinessdegree;
            // 配列からこの要素を削除する.
            SuctionObjectsArray.Remove(col.gameObject);
            // Gameシーン上から削除する.
            //col.gameObject.active = false;
            Destroy(col.gameObject);
        }
    }

    private IEnumerator ThisDestroy()
    {
        // 3秒後に処理を開始する.
        yield return  new WaitForSeconds(2.0f);
        // 自身を削除する前にStageClearManagerに対し現時点でステージをクリアしたか否かを確認させる.
        m_StageClearManager.GetComponent<StageClearManager>().ShowResult();
        // 自身が削除された事を GrabMotion 側へ通知する.
        m_GrabMotion.GetComponent<GrabMotion>().DeleteBlackHall();
        // 3秒後に自身を削除する
        Destroy(gameObject);
    }
}
