using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�ォ��~���Ă����������N���X</summary>
public class StoneManager : MonoBehaviour
{
    [SerializeField] GameObject _target;
    [SerializeField] string _groundTag;
    Rigidbody2D _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }
    private void Update()
    {
        float dir = Vector2.Distance(_target.transform.position, this.transform.position);
        Debug.Log(dir);
        if(dir<=8.5)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(_groundTag))
        {
            Destroy(this.gameObject);
        }
    }
}
