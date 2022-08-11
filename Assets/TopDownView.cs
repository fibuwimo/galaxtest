using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ターゲットの真上（惑星→ターゲットの直線上）に移動、角度も合わせて回転させる
public class TopDownView : MonoBehaviour
{
    public float height = 10f;
    public Transform target;
    public Transform planet;
    Vector3 preVec;//移動前の惑星→プレイヤー間のベクトル

    void FixedUpdate()
    {
        //プレイヤーの真上にカメラ移動(計算:プレイヤーx座標 + 惑星→プレイヤーの方向 × (指定した高さ + (惑星のx軸上の座標 - プレイヤーの座標)のベクトルの大きさ) 
        /*
        transform.position =
        new Vector3(target.position.x, 0, 0)
        - (new Vector3(target.position.x, planet.position.y, planet.position.z) - target.position).normalized
        * (height + (new Vector3(target.position.x, planet.position.y, planet.position.z) - target.position).magnitude);
        */

        transform.position = (target.position - planet.position) * 2f;

        //移動後の惑星→プレイヤー間ベクトル
        Vector3 next = CalcVecCameraFromPlanet();

        //惑星上での移動角度
        float moveAngle = CalcPlaneAngle(preVec, next);
        float moveAngle2 = CalcPlaneAngle2(preVec, next);

        //プレイヤーの動きに合わせてカメラを回転
        transform.Rotate(moveAngle, 0, 0);

        //デバッグ用 正面と上方に飛ばすレイ
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
        Debug.DrawRay(transform.position, transform.up * 10f, Color.blue);

        //移動前の惑星→プレイヤー間ベクトル保存
        preVec = CalcVecCameraFromPlanet();

        //ここから下は試行錯誤した残骸
        //カメラをプレイヤーと同じ向きにした後、真下に向く
        // transform.rotation = player.rotation;
        // transform.rotation = new Quaternion(player.rotation.x, 0, player.rotation.z, player.rotation.w);

        //カメラ移動処理(難しい・・・)
        //問題点：下方向に進むとカメラの向きが進行方向とは逆回転してしまう
        // transform.position = player.position + player.up * 10f;
        // transform.position = player.position - transform.forward * 10f;

        //カメラの回転処理(真下向く) x軸回転だけさせる
        // transform.rotation = new Quaternion(player.rotation.x, 0, 0, player.rotation.w);
        // transform.Rotate(Vector3.Angle(transform.forward, player.position - transform.position), 0, 0);

    }

    //惑星→カメラのベクトル返す（x座標はプレイヤーの位置固定）
    Vector3 CalcVecCameraFromPlanet()
    {
        return target.position - new Vector3(target.position.x, planet.position.y, planet.position.z);

    }

    //作ったけど要らない子
    // float CalcMoveAngle(Vector3 pre, Vector3 next)
    // {
    //     return Vector3.SignedAngle(pre, next, Vector3.up);
    // }

    //二方向のベクトルを指定した平面内のベクトルに変換後,角度を返す
    float CalcPlaneAngle(Vector3 pre, Vector3 next)
    {
        Vector3 planeFrom = Vector3.ProjectOnPlane(pre, Vector3.right);
        Vector3 planeTo = Vector3.ProjectOnPlane(next, Vector3.right);
        return Vector3.SignedAngle(planeFrom, planeTo, Vector3.right);
    }
    float CalcPlaneAngle2(Vector3 pre, Vector3 next)
    {
        Vector3 planeFrom = Vector3.ProjectOnPlane(pre, Vector3.forward);
        Vector3 planeTo = Vector3.ProjectOnPlane(next, Vector3.forward);
        return Vector3.SignedAngle(planeFrom, planeTo, Vector3.forward);
    }
}