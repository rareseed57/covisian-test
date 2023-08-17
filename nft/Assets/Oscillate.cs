using System;
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

    private Rigidbody _rigidbody;
    public float _smoother;
    
    // Start is called before the first frame update
    void Start()
    {
        _clickFollower = controller.GetComponent<ClickFollower>();
        _rigidbody = controller.GetComponent<Rigidbody>();
        _oscillation = _clickFollower.oscillation;
        _controllerTransform = controller.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(timer - 1) < 0.001) timer = 0;
        else
        {
            if(_clickFollower.moving) timer += Time.deltaTime * _clickFollower.oscillationFrequency;
        }
        if (timer > 1) timer = 1;

        if (!_clickFollower.moving && _smoother>0)
            _smoother -= Time.deltaTime;
        
        if (_clickFollower.moving && _smoother<1)
            _smoother += Time.deltaTime;

        var transform1 = transform;
        
        transform1.rotation = _controllerTransform.rotation;
        transform1.position = _controllerTransform.position +
                              _controllerTransform.up*_oscillation.Evaluate(timer)*_clickFollower.oscillationAmplitude*_smoother; //*_rigidbody.velocity.magnitude/1000;
    }
}
