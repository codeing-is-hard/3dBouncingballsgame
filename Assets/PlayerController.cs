using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private Transform playerObject;        //보여지는 오브젝트(y 이동)
    [SerializeField] private float xSensitivity = 15.0f;    //x축 이동감도로 클수록 더많은 범위를 움직이게된다.
    [SerializeField] private float moveTime=1.0f; 
    [SerializeField] private float minPositionY = 0.55f;
    private float gravity = -9.81f;
    private int platformIndex = 0;

    private IEnumerator Start()     //인터페이스이므로 yield반환문필수!
    {
        while(true)     //마우스 왼쪽 버튼을 누르기 전까지 시작하지 않고 대기하는 코드
        {
            if(Input.GetMouseButtonDown(0))//마우스 왼쪽버튼을 누르면
            {
                //플레이어의 y,z축의 이동을 제어함
                StartCoroutine("MoveLoop");         //코루틴이벤트??

                break;

            }

            yield return null;
        }
    }

    private IEnumerator MoveLoop()
    {
        while(true)
        {
            platformIndex++;

            //float current=
        }
    }




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
                Touch touch= Input.GetTouch(0);

                x = (touch.position.x / Screen.width) - 0.5f;   
                //0.0f~1.0f의 값으로 만들고 (화면전체에서 터치스크린의x축을나누고) 그뒤에0.5f
                //만큼 빼주기때문에 실제값은 -0.5f~0.5f구간사이의 값으로 나온다.
            }
        }
        //현재플랫폼이 pc기준이면 다음루프실행
        else
        {
            //0.0f~1.0f사이의 값으로 만들고 -0.5f를 하기 때문에 -0.5f~0.5f사이의 값이나온다.
            x = (Input.mousePosition.x / Screen.width) - 0.5f;
        }
        //화면 밖을 터치해서 x값이 -0.5f~0.5의 범위를 넘어가지않도록 범위 지정
        //(x가 위치 할 수 있는 최솟값~최대값 지정)
        x = Mathf.Clamp(x, -0.5f, 0.5f);
        //플레이어의 x축 위치 설정
        position.x =Mathf.Lerp(position.x, x*xSensitivity, xSensitivity*Time.deltaTime);    //0~1까지 얼마나 진행되었는지 진행률을 나타낸 보간 비율(시작점~목표점)
        //사용자의 프레임에 따라 목표에 도달하는 속도가 달라지지 않기 위해 time.deltatime을 곱해야한다
        //곱하지 않으면 사용자의 프레임에 따라 속도가 달라지는데
        //60프래임이면 더빠르게 도달하고 30프래임이면 더 느리게 도달하는 등 사용자의 환경에 따라
        //도착시간이 달라진다!! 즉 사용자의 환경에 따라 속도가 달라지지 않고
        //일정하게 유지해주기위해 time.deltaTime을 곱해주는것!
        transform.position = position;
    }







}
