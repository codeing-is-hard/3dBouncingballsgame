using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    [SerializeField] private GameObject platformPrefab;         //������ ��������� ������
    [SerializeField] private int SpawnPlatformCountStart = 10;      //���� ���۽� ���� �����Ǵ� �����ǰ���
    [SerializeField] private float xRange = 10;     //������ ��ġ�ɼ��ִ�x��ǥ�ǹ���(+10~-10range��)
    [SerializeField] public float zDistance = 3;   //���� ������ �Ÿ�(z��)

    [SerializeField] private int platformIndex = 0;      //���� �ε����� ��ġ�Ǵ� ������ z�� ��ġ�� ������ �� �����



    private void Awake()
    {
        for (int i = 0; i < SpawnPlatformCountStart; ++i)       //spawnPlatformCountStart�� ����� ������ŭ ���� �÷����� ��������
        {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform()
    {
        GameObject clone = Instantiate(platformPrefab);     //���ο� ���� ��������

        clone.GetComponent<Platform>().Setup(this);         //�� ������ ������Ʈ�� ���ʸ� ������ �÷����� �¾� �޼ҵ� �ȿ� �մ� �÷��� �����ʿ��� �����ͼ�
                                                            //(this==�÷���������{�ڱ��ڽ���}������Ų��.)++ �̺κп־ȵǴ�����..

        ResetPlatform(clone.transform);     //������ ��ġ�� �����Ѵ�      //�ָ��ߴ�����..
    }


    public void ResetPlatform(Transform transform, float y = 0)
    {
        platformIndex++;
        float x = Random.Range(-xRange, xRange);        //������ ��ġ�Ǵ� x����ġ�� -xRange ~ xRange ���̷� ����

        transform.position = new Vector3(x, y, platformIndex * zDistance);      //������ ��ġ�Ǵ� ��ġ ��������
                                                                                //(z���� ���� ������ �ε�����*zDistance�����Ѱ�,���� ������ �Ÿ�=zDistance)
    }
}
