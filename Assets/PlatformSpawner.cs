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
        GameObject clone = Instantiate(platformPrefab);     //���ο� ���� ��������

        clone.GetComponent<Platform>().Setup(spawner);

        ResetPlatform(clone.transform);     //������ ��ġ�� ������
    }


    public void ResetPlatform(Transform transform,float y=0)
    {
        float x = Random.Range(-xRange, xRange);        //������ ��ġ�Ǵ� x����ġ�� -xRange ~ xRange ���̷� ����

        transform.position = new Vector3(x, y, platformIndex * zDistance);      //������ ��ġ�Ǵ� ��ġ ��������
                                                                                //(z���� ���� ������ �ε�����*zDistance�����Ѱ�,���� ������ �Ÿ�=zDistance)
    }
}
