using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class FlickingLigth : MonoBehaviour
{
    Light2D tocha;

    public float timeFlicking;
    public float minLigthFlickers;
    public float maxLigthFlickers;

    // Start is called before the first frame update
    void Start()
    {
        tocha = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float intensity = Mathf.Lerp(minLigthFlickers, maxLigthFlickers, Mathf.PingPong(Time.time * timeFlicking, 1f));
        tocha.pointLightOuterRadius = Random.Range(1.5f, intensity);
        tocha.shapeLightFalloffSize = Random.Range(1, intensity);


    }


}