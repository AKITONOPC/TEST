using UnityEngine;
// transformを使った移動
public class OriginalMove : MonoBehaviour
{
    // 障害物
    [SerializeField]
    GameObject obstacle;
    // 自分の大きさ。Xが幅でYが高さ
    float myWidth;
    // 障害物の幅
    float obstacleWidth;
    // 自分の右端の座標(四角形で角度もないので右辺になる)
    float myRightPosition;
    // 自分の左端の座標(四角形で角度もないので左になる)
    float obstacleLeftPosition;
    // 自分の右端の座標(四角形で角度もないので右辺になる)
    float obstacleRightPosition;
    // 動く速度
    Vector3 moveValue;
    void Start()
    {
        // 移動変数の初期化
        moveValue = new(5.0f, 0.0f, 0.0f);
        // 自分の幅。スケールのX(X軸の大きさ)
        myWidth = transform.lossyScale.x;
        // 障害物の大きさ
        obstacleWidth = obstacle.transform.lossyScale.x;
        // 最初に1回辺に情報を与える
        PositionUpdate();
    }
    // オブジェクトが動くので，辺の位置情報を更新する
    // これを忘れるとオブジェクトは動いているのに情報が置き去りになる
    void PositionUpdate()
    {
        // 右辺の座標は，自分の中心座標 + 自分の幅 ÷ 2
        myRightPosition = transform.position.x + (myWidth * 0.5f);
        // 上に同じ。左辺は引き算になることに注意。右がプラスで左はマイナス
        obstacleRightPosition = obstacle.transform.position.x + (obstacleWidth * 0.5f);
        obstacleLeftPosition = obstacle.transform.position.x - (obstacleWidth * 0.5f);
    }
    // 当たり判定を行う
    bool CollisionCheck()
    {
        // 自分の右辺の座標が障害物の左辺より大きい位置にあるかどうか と
        // 自分の右辺の座標が障害物の左辺より小さい位置にあるかどうか の両方を満たしているときだけ処理に入る
        // つまりは，自分の右辺が障害物にめり込んじゃっているかを見てる
        if (myRightPosition >= obstacleLeftPosition &&
        myRightPosition <= obstacleRightPosition)
            // めり込んでいたらtrueを返す
            return true;
        // めり込んでなければfalse
        return false;
    }
    void Update()
    {
        // 何かにぶつかっていたら進んでほしくないので移動前の座標を覚えておく
        Vector3 beforePosition = transform.position;
        // 一時変数
        float tmpMoveValue;
        // 時間の経過を計算
        tmpMoveValue = moveValue.x * Time.deltaTime;
        // 実際の移動処理
        // 今いる位置からX座標へtmpSpeedの数値分離れたところへ行ってくださいという命令
        transform.Translate(tmpMoveValue, moveValue.y, moveValue.z);
        // 当たり判定を行う
        if (CollisionCheck())
        {
            // ぶつかっていたら移動はせずに移動前の座標を代入
            transform.position = beforePosition;
            // 移動しないので終了
            return;
        }
        // オブジェクトの移動が完了したので，情報も更新してあげる
        PositionUpdate();
    }
}