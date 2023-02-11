using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>横に発射される岩を管理するクラス</summary>
public class StoneManager : MonoBehaviour
{
    [SerializeField] GameObject _stone = default;
    [SerializeField] float angle = 0;
    [SerializeField] float _x;
    [SerializeField] float _stoneShotForce = 10;
    [SerializeField] float _stoneShotSpan = 4.0f;
    Vector3 vel;
    private void Awake()
    {
        // 角度をラジアンに変換
        float rad = angle * Mathf.Deg2Rad;//1°
        // ラジアンから進行方向を設定
        Vector3 direction = new Vector3(Mathf.Cos(rad), 0, 0);
        // 方向に速度を掛け合わせて移動ベクトルを求める
        vel = direction * _stoneShotForce * Time.deltaTime;
    }
    private void Start()
    {
        StartCoroutine(StoneCoroutine());
    }
    private IEnumerator StoneCoroutine()
    {
        yield return new WaitForSeconds(_stoneShotSpan);
        StoneShot();
        yield return null;
    }
    /// <summary>岩が横方向に飛ぶ関数</summary>
    private void StoneShot()
    {
        Instantiate(_stone, transform);
        _stone.transform.position -= vel;
    }
}
