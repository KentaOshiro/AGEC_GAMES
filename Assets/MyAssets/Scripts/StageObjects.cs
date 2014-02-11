using UnityEngine;
using System.Collections;

public class StageObjects : MonoBehaviour {

    float timeToScale = 1.0f;
    public int m_iIndex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void ToTarget(Transform toTarget)
    {
        // 引数で渡されたオブジェクトに向かうベクトルを取得.
        Vector3 toPosition = toTarget.position - transform.position;
        // 取得したベクトルを自身のvelocityへと加えていく.
        gameObject.rigidbody.velocity += toPosition.normalized * (3.0f / Mathf.Max(1.0f, toPosition.magnitude));
    }

    void OnCollisionEnter(Collision col)
    {
        // もしぶつかったオブジェクトが「PlayerBullet」のタグを持つオブジェクトなら
        if (col.gameObject.tag == "PlayerBullet")
        {
            // 衝突先のオブジェクトに対しメッセージを自身のIndexを付与して送信する.
            col.gameObject.SendMessage("DestroyStageObject", m_iIndex);
            // 自身を削除する.
            Destroy(gameObject);
        }
    }
}
