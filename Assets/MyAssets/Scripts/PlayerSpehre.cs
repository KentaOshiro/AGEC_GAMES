using UnityEngine;
using System.Collections;

public class PlayerSpehre : MonoBehaviour
{

    GameObject[] SuctionObjects;
    bool m_bIsCollision = false;

    // Use this for initialization
    void Start()
    {
        // 初めにステージ内に配置されている「SuctionObject」というタグを持つオブジェクトを全て取得する.
        SuctionObjects = GameObject.FindGameObjectsWithTag("SuctionObject");
    }

    // Update is called once per frame
    void Update()
    {
        // このオブジェクトが何にもぶつかっていない場合以下の処理は行わない.
        if (!m_bIsCollision)
            return;

        // 何かにぶつかるとStartメソッドで取得したオブジェクトに対しメッセージを自身の位置情報付きで送信する.
        foreach (GameObject obj in SuctionObjects)
            obj.SendMessage("ToTarget", transform);

        // ThisDestroyメソッドのコルーチンを開始
        StartCoroutine("ThisDestroy");
    }

    void OnCollisionEnter(Collision col)
    {
        // 何かにぶつかった事を知らせるフラグを立てる.
        m_bIsCollision = true;
        // このオブジェクトを不動の物とする
        gameObject.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        // ビューから表示しないようにする.
        gameObject.renderer.enabled = false;
    }

    public void DestroyStageObject(int index)
    {
        // StageObjectsからSendMessageで呼ばれるこのメソッドは与えられたIndexを元に自身が持つ
        // 配列から特定のIndexを持つオブジェクトを削除する.
        foreach (GameObject obj in SuctionObjects)
            if (obj.GetComponent<StageObjects>().m_iIndex == index)
                Destroy(obj);
    }

    private IEnumerator ThisDestroy()
    {
        // 3秒後に処理を開始する.
        yield return  new WaitForSeconds(3.0f);
        // 3秒後に自身を削除する
        Destroy(gameObject);
    }
}
