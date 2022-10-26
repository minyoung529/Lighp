using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 파티클 오브젝트에 붙이는 함수
/// 외부에서 파티클의 요소를 쉽게 수정할 수 있게 하기 위함
/// </summary>

[RequireComponent(typeof(ParticleSystem))]
public class Particle : Poolable
{
    new private ParticleSystem particleSystem;
    public ParticleSystem ParticleSystem { get => particleSystem; }

    private ParticleSystem.MainModule main;
    public ParticleSystem.MainModule MainModule { get => main; }

    private float timer = 0f;

    public FollowTarget Follow { get; private set; }

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        Follow = GetComponent<FollowTarget>();
        main = particleSystem.main;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!main.loop && timer > main.duration)
        {
            GameManager.Instance.Pool.Push(this);
        }
    }

    public void SetStartColor(Color32 color)
    {
        main.startColor = (Color)color;
    }

    public void SetStartColorAlpha(float alpha)
    {
        Color c = main.startColor.color;
        c.a = alpha;

        main.startColor = c;
    }

    public void SetLifeTime(float lifeTime)
    {
        main.startLifetime = lifeTime;
    }

    public void SetStartSizeY(float y)
    {
        main.startSizeY = y;
    }

    public void Play()
    {
        particleSystem.Play();
    }

    public void Stop()
    {
        particleSystem.Stop();
    }

    public override void ResetData()
    {
        timer = 0f;
        Stop();
    }
}
