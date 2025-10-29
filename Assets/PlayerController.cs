using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float xSensitivity = 15.0f;    //x축 이동감도로 클수록 더많은 범위를 움직이게된다.


    private void Update()
    {
        if (Input.GetMouseButton(0))            //마우스 왼쪽버튼(이나모바일 터치로)으로 플레이어의 x축위치를 제어한다.
        {
            MoveToX();
        }
    }

    private void MoveToX()
    {
        float x = 0.0f;
        Vector3 position = transform.position;

        if(Application.isMobilePlatform)        //현재 플랫폼이 모바일이면 다음루프실행
        {
            if (Input.touchCount > 0)
            {

            }
        }
    }







}
