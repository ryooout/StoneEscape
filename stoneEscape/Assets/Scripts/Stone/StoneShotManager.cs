using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>Œã‚ë‚©‚ç”­ŽË‚³‚ê‚éŠâ</summary>
public class StoneShotManager : MonoBehaviour
{
    [SerializeField]
    GameObject _stonePrefab = default;
    [SerializeField]
    int _shotSpeed;
    void Start()
    {
        StartCoroutine(StoneShot());
    }
    private IEnumerator StoneShot()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            Spawn();
            yield return new WaitForSeconds(5.0f);
            Spawn();
        }

    }
    private void Spawn()
    {
        var stone = Instantiate(_stonePrefab, transform);
        var rb = stone.GetComponent<Rigidbody2D>();
        rb.MovePosition(transform.position + Vector3.right*Time.deltaTime*_shotSpeed);
    }
}
