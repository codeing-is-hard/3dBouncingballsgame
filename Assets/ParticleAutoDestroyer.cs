using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroyer : MonoBehaviour
{
    private ParticleSystem particlesystem;
    private void Awake()
    {
        particlesystem = GetComponent<ParticleSystem>();        //컴퍼넌트중에 파티클시스템클래스를가져와서 파티클변수에넣기

    }

    private void Update()
    {
        //파티클이 재생중이 아니면 삭제(파괴)
        if(particlesystem.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
