using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformSpawner _platformSpawner;
    private Camera mainCamera;
    private float yMoveTime = 0.5f;       //���ġ�Ǵ� �÷����� �������� �̵� �ð�

    public void Setup(PlatformSpawner platformSpawner)
    {
        _platformSpawner = platformSpawner;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //�÷����� ī�޶� �ڷ� ���� ������ �ʰ� �Ǹ� �÷����� ���ġ�Ѵ�
        if (mainCamera.transform.position.z - transform.position.z > 0)
        {
            _platformSpawner.ResetPlatform(_platformSpawner.transform);         //�÷����� ��ġ �缳��

            StartCoroutine(MoveY(10, 0));   
            //���� ������ ������ ������ �������� ȿ���� �����Ѵ�.

        }
    }

    private IEnumerator MoveY(float start,float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / yMoveTime;

            float y = Mathf.Lerp(start, end, percent);
            //�ð����(�ִ� 1)�� ���� ������Ʈ�� y��ǥ ��ġ�� �ٲ��ش�.
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            yield rerurn null;
        }
    }
    

    
    



}
