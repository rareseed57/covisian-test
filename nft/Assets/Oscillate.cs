using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    public float timer;
    public GameObject controller;
    private ClickFollower _clickFollower;

    private AnimationCurve _oscillation;
    private Transform _controllerTransform;
    // Start is called before the first frame update
    void Start()
    {
        _clickFollower = controller.GetComponent<ClickFollower>();
        _oscillation = _clickFollower.oscillation;
        _controllerTransform = controller.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(_clickFollower.moving) timer += Time.deltaTime;
        if (timer > 1) timer = 0;

        var transform1 = transform;
        transform1.position = _controllerTransform.position;
        transform1.rotation = _controllerTransform.rotation;

        transform1.localPosition += new Vector3(0,
                                                _oscillation.Evaluate(timer*_clickFollower.oscillationFrequency)*_clickFollower.oscillationAmplitude,
                                                0);
    }
}
