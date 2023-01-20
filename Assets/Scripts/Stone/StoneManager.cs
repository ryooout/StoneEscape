using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>è„Ç©ÇÁç~Ç¡ÇƒÇ≠ÇÈä‚ÇàµÇ§ÉNÉâÉX</summary>
public class StoneManager : MonoBehaviour
{
    [SerializeField] GameObject _target;
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
}
