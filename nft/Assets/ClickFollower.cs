using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFollower : MonoBehaviour
{
    public Transform target;

    public float turnDuration;
    public float maxForce;
    public float forceIntensity;

    public float steeringVelocity;
    
    public float oscillationAmplitude = 1;
    public float oscillationFrequency = 1;
    
    public bool moving = false;
    
    public AnimationCurve oscillation;
    public AnimationCurve turn;

    private Rigidbody _rigidbody;

    private Quaternion _lookrotation;
    
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _rigidbody);
    }

    // Update is called once per frame
    void Update()
    {
        //find the vector pointing from our position to the target
        var direction = (target.position - transform.position).normalized;
            
        //create the rotation we need to be in to look at the target
        _lookrotation = Quaternion.LookRotation(direction);

        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(Turn());
            StopCoroutine(Movement());
            moving = false;
            StartCoroutine(Turn());
        }
    }

    IEnumerator Turn()
    {
        float turnTimer = 0;
        while (turnTimer < turnDuration)
        {
            turnTimer += Time.deltaTime;

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookrotation, turn.Evaluate(turnTimer/turnDuration*(1-Quaternion.Dot(transform.rotation, _lookrotation))));

            // This line of code reduces the force when the hook is close to target to avoid oscillation and make the movement converge
            if(_rigidbody.drag<3)
                _rigidbody.drag += 0.5f*Time.deltaTime;
            yield return null;
        }
        
        moving = true;
        _rigidbody.drag = 0;
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        if (_rigidbody == null)
        {
            Debug.Log("Rigid body not found.");
        }
        
        while (moving)
        {
            var position = transform.position;
            var targetPosition = target.position;

            //rotate and go towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookrotation, steeringVelocity * Time.deltaTime);
            _rigidbody.AddForceAtPosition(Vector3.ClampMagnitude(transform.forward, maxForce) * forceIntensity, position, ForceMode.Force);

            // This line of code reduces the force when the hook is close to target to avoid oscillation and make the movement converge
            _rigidbody.drag = 50 / Vector3.Distance(targetPosition, position);

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
 
        Gizmos.DrawWireCube(Vector3.zero, Vector3.zero);
    }
}
