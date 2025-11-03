using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private GameController gameController;
    [SerializeField] private Transform playerObject;        //보여지는 오브젝트(y 이동)
    [SerializeField] private float xSensitivity = 15.0f;    //x축 이동감도로 클수록 더많은 범위를 움직이게된다.
    [SerializeField] private float moveTime=1.0f; 
    [SerializeField] private float minPositionY = 0.55f;
    private float gravity = -9.81f;
    private int platformIndex = 0;


    private RaycastHit hit;     //광선에 부딪친 오브젝트 정보를 저장
    private IEnumerator Start()     //인터페이스이므로 yield반환문필수!,유니티이벤트로쓰고싶으면 start가아닌다른메소드명으로바꿔야함!!
    {
        while(true)     //마우스 왼쪽 버튼을 누르기 전까지 시작하지 않고 대기하는 코드
        {
            if (Input.GetMouseButtonDown(0)) //마우스 왼쪽버튼을 누르면
            {
                //게임시작
                gameController.GameStart();

                //플레이어의 y,z축의 이동을 제어함
                StartCoroutine("MoveLoop");         //코루틴이벤트??

                //플레이어의 이동 시간 감소
                StartCoroutine("DecreaseMoveTime");

                break;

            }

            yield return null;
        }
    }

    private IEnumerator MoveLoop()      
    {
        while (true) 
        {
            platformIndex++;                                //현재 플레이어가 밟게되는 발판과 다음 발판의
                                                            //z축위치를 계산하여 yield반환문의 MoveToYZ()메소드의 코루틴 이벤트에 전달하고
                                                            //

            float current = (platformIndex - 1) * platformSpawner.zDistance;
            float next = platformIndex * platformSpawner.zDistance;

            //플레이어의 y,z축의 위치를 제어하는 코루틴 이벤트(yield return 반환문으로 무브루프를 일시정지,
            //새로운 코루틴을 실행시키고 끝날때까지 yield에서 일시 정지하고 끝나면 yield다음 줄 실행,지금 경우에는 없으므로
            //항상 참인 while문의 조건을 확인하고 다시 첫 줄부터 실행
            yield return StartCoroutine(MoveToYZ(current, next));

            //플레이어가 다음 플랫폼에 도착했을때 플레이어의도착위치가 플랫폼이면
            if (hit.transform != null)
            {
                //Debug.Log("Hit");
                gameController.IncreaseScore();
            }

            //플레이어의 도착 위치가 낭떠러지이면
            else
            {
                //Debug.Log("GameOver");      //콘솔에 디버그용으로쓰는로그
                gameController.GameOver();
                break;
            }
        }
    }




    private void Update()
    {

        //아래쪽 방향으로 광선을(1.0만큼) 발사해 광선에 부디치는 발판 정보를 hit에 저장함
        Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f);


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
        position.x = Mathf.Lerp(position.x, x * xSensitivity, xSensitivity * Time.deltaTime);    //0~1까지 얼마나 진행되었는지 진행률을 나타낸 보간 비율(시작점~목표점)
        //사용자의 프레임에 따라 목표에 도달하는 속도가 달라지지 않기 위해 time.deltatime을 곱해야한다
        //곱하지 않으면 사용자의 프레임에 따라 속도가 달라지는데
        //60프래임이면 더빠르게 도달하고 30프래임이면 더 느리게 도달하는 등 사용자의 환경에 따라
        //도착시간이 달라진다!! 즉 사용자의 환경에 따라 속도가 달라지지 않고
        //일정하게 유지해주기위해 time.deltaTime을 곱해주는것!
        transform.position = position;
    }

    private IEnumerator MoveToYZ(float start, float end)        //y,z좌표 이동과 관련된 메소드
    {
        float current = 0;              //일정한 도달 속도를 유지해주기위해 time.deltatime을 더해서 넣어줄 임시 저장 변수
        float percent = 0;              //시간 연산할때 쓸 퍼센트 
        float v0 = -gravity;            //중력(gravity)은 항상 아랫쪽으로 작용하므로 음수로 적어줘야함

        while (percent < 1)         //0~1사이의 실수값, 진행도가0~1사이이면 아래루프실행
        {
            current += Time.deltaTime;
            percent = current / moveTime;       // 현재까지걸린시간 / 전체 걸려야하는시간=진행률을 의미

            //시간 경과에 따라 오브젝트의 y축위치를 바꿔준다
            //y축은 실제 보여지는 스피어(구체)오브젝트가 포물선 이동을 하도록 만든다.
            //실수y=포물선 운동 공식으로 포물선운동 == 시작위치+초기속도*시간+중력*시간의 제곱으로 구현한다.
            float y = minPositionY + (v0 * percent) + (gravity * percent * percent);

            playerObject.position = new Vector3(playerObject.position.x, y, playerObject.position.z);

            //시간경과(0~1사이)에따라 오브젝트의 z축위치를바꿔준다.
            float z = Mathf.Lerp(start, end, percent);
            
            transform.position = new Vector3(transform.position.x, y,z);


            yield return null;  //IEnumerator 인터페이스는 반드시 하나이상의 yield return문이있어야하고 tield return을만나면 다음으로진행하지않고일시중지
                                //후 유니티의 재시작 요청이 잇을 때까지 대기
        }
    }

    private IEnumerator DecreaseMoveTime()      //ienumerator 인터페이스,이동 시간 감소(이동 속도 증가)메소드
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);      //1.0초 동안 기다린 후에 유니티가 다음 줄을 실행하도록 하는
                                                        //특별한 코루틴 이벤트

            //플레이어의 y,z축 이동시간을감소시킴(점점 빠르게 이동하도록)
            moveTime -= 0.02f;

            //이동시간이 0.2f 이하이면 더이상 줄이지 않기

            if(moveTime <= 0.2f)
            {
                break;
            }
        }
    }



}
