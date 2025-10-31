using System.Collections;
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

    private IEnumerator Start()     //�������̽��̹Ƿ� yield��ȯ���ʼ�!,����Ƽ�̺�Ʈ�ξ�������� start���ƴѴٸ��޼ҵ�����ιٲ����!!
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

    private IEnumerator MoveLoop()      //���� �÷��̾ ��ԵǴ� ���ǰ� ���� ������
                                        //z����ġ�� ����Ͽ� yield��ȯ���� MoveToYZ()�޼ҵ��� �ڷ�ƾ �̺�Ʈ�� �����ϰ�
                                        //
    {
        while(true)
        {
            platformIndex++;

            float current = (platformIndex - 1) * platformSpawner.zDistance;
            float next = platformIndex * platformSpawner.zDistance;

            //�÷��̾��� y,z���� ��ġ�� �����ϴ� �ڷ�ƾ �̺�Ʈ(yield return ��ȯ������ ��������� �Ͻ�����,
            //���ο� �ڷ�ƾ�� �����Ű�� ���������� yield���� �Ͻ� �����ϰ� ������ yield���� �� ����,���� ��쿡�� �����Ƿ�
            //�׻� ���� while���� ������ Ȯ���ϰ� �ٽ� ù �ٺ��� ����
            yield return StartCoroutine(MoveToYZ(current, next));
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

    private IEnumerator MoveToYZ(float start, float end)        //y,z��ǥ �̵��� ���õ� �޼ҵ�
    {
        float current = 0;              //������ ���� �ӵ��� �������ֱ����� time.deltatime�� ���ؼ� �־��� �ӽ� ���� ����
        float percent = 0;              //�ð� �����Ҷ� �� �ۼ�Ʈ 
        float v0 = -gravity;            //�߷�(gravity)�� �׻� �Ʒ������� �ۿ��ϹǷ� ������ ���������

        while(percent<1)        //0~1������ �Ǽ���, ���൵��0~1�����̸� �Ʒ���������
        {
            current += Time.deltaTime;
            percent = current / moveTime;       // ��������ɸ��ð� / ��ü �ɷ����ϴ½ð�=������� �ǹ�

            //�ð� ����� ���� ������Ʈ�� y����ġ�� �ٲ��ش�
            //y���� ���� �������� ���Ǿ�(��ü)������Ʈ�� ������ �̵��� �ϵ��� �����.
            //�Ǽ�y=������ � �������� ������� == ������ġ+�ʱ�ӵ�*�ð�+�߷�*�ð��� �������� �����Ѵ�.
            float y = minPositionY + (v0 * percent) + (gravity * percent * percent);

            playerObject.position = new Vector3(playerObject.position.x, y, playerObject.position.z);

            //�ð����(0~1����)������ ������Ʈ�� z����ġ���ٲ��ش�.
            float z=Mathf.Lerp(start,end, percent);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);


            yield return null;  //IEnumerator �������̽��� �ݵ�� �ϳ��̻��� yield return�����־���ϰ� tield return�������� �����������������ʰ��Ͻ�����
                                //�� ����Ƽ�� ����� ��û�� ���� ������ ���
        }
    }





}
