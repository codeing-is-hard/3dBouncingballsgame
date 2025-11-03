using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform target;      //카메라가 추적하고있는대상

    private void LateUpdate()
    {
        //target이 존재하지않으면 실행되지않음
        if(target == null)
        {
            return;
        }

        //카메라의 위치 정보 갱신
        Vector3 position = transform.position;
        position.z = target.position.z - 10;
        transform.position = position;
    }
}
