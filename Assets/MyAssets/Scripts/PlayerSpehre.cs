using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpehre : MonoBehaviour
{
    [SerializeField]
    GameObject[] SuctionObjects;
    List<GameObject> SuctionObjectsArray = new List<GameObject>();
    bool m_bIsCollision = false;

    // Use this for initialization
    void Start()
    {
        SuctionObjects = GameObject.FindGameObjectsWithTag("SuctionObject");

        foreach (GameObject obj in SuctionObjects)
            SuctionObjectsArray.Add(obj);

    }

    // Update is called once per frame
    void Update()
    {
        // このオブジェクトが何にもぶつかっていない場合以下の処理は行わない.
        if (!m_bIsCollision)
            return;

        foreach (GameObject obj in SuctionObjectsArray)
        {
            Vector3 toPosition = transform.position - obj.transform.position;
            obj.rigidbody.velocity += toPosition.normalized * (10.0f / Mathf.Max(1.0f, toPosition.magnitude));
        }


         //ThisDestroyメソッドのコルーチンを開始
        StartCoroutine("ThisDestroy");
    }

    void OnCollisionEnter(Collision col)
    {
        // 何かにぶつかった事を知らせるフラグを立てる.
        m_bIsCollision = true;
        // このオブジェクトを不動の物とする
        gameObject.rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        if (col.gameObject.tag == "SuctionObject")
        {
            SuctionObjectsArray.Remove(col.gameObject);
            Destroy(col.gameObject);
        }

        // ビューから表示しないようにする.
        //gameObject.renderer.enabled = false;
    }

    //public void DestroyStageObject(int index)
    //{
    //    // StageObjectsからSendMessageで呼ばれるこのメソッドは与えられたIndexを元に自身が持つ
    //    // 配列から特定のIndexを持つオブジェクトを削除する.
    //    foreach (GameObject obj in SuctionObjects)
    //        if (obj.GetComponent<StageObjects>().m_iIndex == index)
    //            Destroy(obj);
    //}

    private IEnumerator ThisDestroy()
    {
        // 3秒後に処理を開始する.
        yield return  new WaitForSeconds(2.0f);
        // 3秒後に自身を削除する
        Destroy(gameObject);
    }
}
