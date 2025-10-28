using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformSpawner _platformSpawner;
    private Camera mainCamera;
    private float yMoveTime = 0.5f;       //재배치되는 플랫폼이 내려오는 이동 시간

    public void Setup(PlatformSpawner platformSpawner)
    {
        _platformSpawner = platformSpawner;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //플랫폼이 카메라 뒤로 가서 보이지 않게 되면 플랫폼을 재배치한다
        if (mainCamera.transform.position.z - transform.position.z > 0)
        {
            _platformSpawner.ResetPlatform(_platformSpawner.transform);         //플랫폼의 위치 재설정

            StartCoroutine(MoveY(10, 0));   
            //새로 등장할 때에는 위에서 떨어지는 효과를 적용한다.

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
            //시간경과(최대 1)에 따라 오브젝트의 y좌표 위치를 바꿔준다.
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            yield rerurn null;
        }
    }
    

    
    



}
