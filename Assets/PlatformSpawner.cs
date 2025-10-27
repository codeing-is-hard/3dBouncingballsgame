using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    [SerializeField] private GameObject platformPrefab;         //������ ��������� ������
    [SerializeField] private int spawnPlatformCountStart = 12;      //���� ���۽� ���� �����Ǵ� �����ǰ���
    [SerializeField] private float xRange = 10;     //������ ��ġ�ɼ��ִ�x��ǥ�ǹ���(+10~-10range��)
    [SerializeField] private float zDistance = 3;   //���� ������ �Ÿ�(z��)
    private int platformIndex = 0;      //���� �ε����� ��ġ�Ǵ� ������ z�� ��ġ�� ������ �� �����

    private void Awake()
    {
        for(int i=0;i<spawnPlatformCountStart;++i)      //spawnPlatformCountStart�� ����� ������ŭ ���� �÷����� ��������
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
