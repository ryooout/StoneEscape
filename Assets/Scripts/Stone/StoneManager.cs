using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>���ɔ��˂�������Ǘ�����N���X</summary>
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
        // �p�x�����W�A���ɕϊ�
        float rad = angle * Mathf.Deg2Rad;//1��
        // ���W�A������i�s������ݒ�
        Vector3 direction = new Vector3(Mathf.Cos(rad), 0, 0);
        // �����ɑ��x���|�����킹�Ĉړ��x�N�g�������߂�
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
    /// <summary>�₪�������ɔ�Ԋ֐�</summary>
    private void StoneShot()
    {
        Instantiate(_stone, transform);
        _stone.transform.position -= vel;
    }
}
