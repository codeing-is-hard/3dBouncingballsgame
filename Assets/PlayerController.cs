using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float xSensitivity = 15.0f;    //x�� �̵������� Ŭ���� ������ ������ �����̰Եȴ�.


    private void Update()
    {
        if (Input.GetMouseButton(0))            //���콺 ���ʹ�ư(�̳������ ��ġ��)���� �÷��̾��� x����ġ�� �����Ѵ�.
        {
            MoveToX();
        }
    }

    private void MoveToX()
    {
        float x = 0.0f;
        Vector3 position = transform.position;

        if(Application.isMobilePlatform)        //���� �÷����� ������̸� ������������
        {
            if (Input.touchCount > 0)
            {

            }
        }
    }







}
