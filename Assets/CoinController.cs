using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private GameObject coinEffectPrefab;
    [SerializeField] private float rotateSpeed = 100.0f;        //회전속도

    private void Update()
    {
        //코인 오브젝트를 회전한다
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);


    }

    private void OnTriggerEnter(Collider other)
    {
        //코인 오브젝트 획득 효과(coinEffectPrefab)을 생성한다
        GameObject clone=Instantiate(coinEffectPrefab);
        clone.transform.position=transform.position;

        //코인 오브젝트의 비활성화
        gameObject.SetActive(false);
    }
}
