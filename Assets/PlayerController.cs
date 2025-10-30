using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private Transform playerObject;        //�������� ������Ʈ(y �̵�)
    [SerializeField] private float xSensitivity = 15.0f;    //x�� �̵������� Ŭ���� ������ ������ �����̰Եȴ�.
    [SerializeField] private float moveTime=1.0f; 
    [SerializeField] private float minPositionY = 0.55f;
    private float gravity = -9.81f;
    private int platformIndex = 0;

    private IEnumerator Start()     //�������̽��̹Ƿ� yield��ȯ���ʼ�!
    {
        while(true)     //���콺 ���� ��ư�� ������ ������ �������� �ʰ� ����ϴ� �ڵ�
        {
            if(Input.GetMouseButtonDown(0))//���콺 ���ʹ�ư�� ������
            {
                //�÷��̾��� y,z���� �̵��� ������
                StartCoroutine("MoveLoop");         //�ڷ�ƾ�̺�Ʈ??

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
                Touch touch= Input.GetTouch(0);

                x = (touch.position.x / Screen.width) - 0.5f;   
                //0.0f~1.0f�� ������ ����� (ȭ����ü���� ��ġ��ũ����x����������) �׵ڿ�0.5f
                //��ŭ ���ֱ⶧���� �������� -0.5f~0.5f���������� ������ ���´�.
            }
        }
        //�����÷����� pc�����̸� ������������
        else
        {
            //0.0f~1.0f������ ������ ����� -0.5f�� �ϱ� ������ -0.5f~0.5f������ ���̳��´�.
            x = (Input.mousePosition.x / Screen.width) - 0.5f;
        }
        //ȭ�� ���� ��ġ�ؼ� x���� -0.5f~0.5�� ������ �Ѿ���ʵ��� ���� ����
        //(x�� ��ġ �� �� �ִ� �ּڰ�~�ִ밪 ����)
        x = Mathf.Clamp(x, -0.5f, 0.5f);
        //�÷��̾��� x�� ��ġ ����
        position.x =Mathf.Lerp(position.x, x*xSensitivity, xSensitivity*Time.deltaTime);    //0~1���� �󸶳� ����Ǿ����� ������� ��Ÿ�� ���� ����(������~��ǥ��)
        //������� �����ӿ� ���� ��ǥ�� �����ϴ� �ӵ��� �޶����� �ʱ� ���� time.deltatime�� ���ؾ��Ѵ�
        //������ ������ ������� �����ӿ� ���� �ӵ��� �޶����µ�
        //60�������̸� �������� �����ϰ� 30�������̸� �� ������ �����ϴ� �� ������� ȯ�濡 ����
        //�����ð��� �޶�����!! �� ������� ȯ�濡 ���� �ӵ��� �޶����� �ʰ�
        //�����ϰ� �������ֱ����� time.deltaTime�� �����ִ°�!
        transform.position = position;
    }







}
