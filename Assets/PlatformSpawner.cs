using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    [SerializeField] private GameObject platformPrefab;         //발판을 만들기위한 프리팹
    [SerializeField] private int spawnPlatformCountStart = 12;      //게임 시작시 최초 생성되는 발판의갯수
    [SerializeField] private float xRange = 10;     //발판이 배치될수있는x좌표의범위(+10~-10range값)
    [SerializeField] private float zDistance = 3;   //발판 사이의 거리(z축)
    private int platformIndex = 0;      //발판 인덱스로 배치되는 발판의 z축 위치를 연산할 때 사용함

    private void Awake()
    {
        for (int i = 0; i < spawnPlatformCountStart; ++ i)       //spawnPlatformCountStart에 저장된 갯수만큼 최초 플랫폼을 생성해줌
        {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform()
    {
        GameObject clone = Instantiate(platformPrefab);     //새로운 발판 생성과정

        clone.GetComponent<Platform>().Setup(this);         //그 발판의 컴포넌트를 제너릭 상태의 플랫폼의 셋업 메소드 안에 잇는 플랫폼 스포너에서 가져와서
                                                            //(this==플랫폼스포너{자기자신을}를가리킨다.)++ 이부분왜안되는지모름..

        ResetPlatform(clone.transform);     //발판의 위치를 설정한다      //왜멈추는지모름..
    }


    public void ResetPlatform(Transform transform,float y=0)
    {
        platformIndex++;
        float x = Random.Range(-xRange, xRange);        //발판이 배치되는 x축위치를 -xRange ~ xRange 사이로 설정

        transform.position = new Vector3(x, y, platformIndex * zDistance);      //발판이 배치되는 위치 설정과정
                                                                                //(z축은 현재 발판의 인덱스값*zDistance를곱한값,발판 사이의 거리=zDistance)
    }
}
