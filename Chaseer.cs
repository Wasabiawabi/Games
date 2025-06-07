using UnityEngine;

public class Chaseer : MonoBehaviour
{
    // UnityがわでPlayerオブジェクトを入れられる変数を用意
    public GameObject player;
    public float speed = 2.0f; // 移動速度
    Vector3 movespeed = Vector3.zero;
    public float limit = 10.0f;
    public float  bounceFactor = 0.5f;//跳ね返り係数
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーが存在する場合
        if (player != null)
        {
            //プレイヤーに向かってmovespeed分動く.movespeedはspeedの蓄積
            //x,y座標ごとのmovespeedにspeedを加算
            movespeed.x += (player.transform.position.x - transform.position.x) * speed * Time.deltaTime;
            movespeed.y += (player.transform.position.y - transform.position.y) * speed * Time.deltaTime;
            transform.position += movespeed * Time.deltaTime;
            //movespeedの上限を超えたら上限値に変える
            if (movespeed.x > 0 && movespeed.x > limit)
            {
                movespeed.x = limit;
            }
            else if (movespeed.x < 0 && movespeed.x < -limit)
            {
                movespeed.x = -limit;
            }
            //y座標のmovespeedも同様に制限をかける
            if (movespeed.y > 0 && movespeed.y > limit)
            {
                movespeed.y = limit;
            }
            else if (movespeed.y < 0 && movespeed.y < -limit)
            {
                movespeed.y = -limit;
            }
        }
    }
    //何か当たったら跳ね返る
    void OnCollisionEnter2D(Collision2D collision)
    {
        //何かに当たったら跳ね返る タグがMovesのオブジェクトに当たった場合
        if (collision.gameObject.tag == "Moves")
        {
            // 当たったオブジェクトの法線ベクトルを取得
            Vector2 normal = collision.contacts[0].normal;
            // 法線ベクトルを使って跳ね返り方向を計算
            Vector2 bounceDirection = Vector2.Reflect(movespeed, normal);
            // 跳ね返り方向にバウンス係数を掛けてmovespeedを更新
            movespeed = bounceDirection * bounceFactor;
            //当たったオブジェクトの位置を取得して、そこに向かって移動する
            Vector3 hitPosition = collision.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, hitPosition, speed * Time.deltaTime);
        }
    }
}
