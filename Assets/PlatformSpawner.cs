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
        for(int i=0;i<spawnPlatformCountStart;++i)      //spawnPlatformCountStart에 저장된 갯수만큼 최초 플랫폼을 생성해줌
        {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform()
    {
        GameObject 
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
