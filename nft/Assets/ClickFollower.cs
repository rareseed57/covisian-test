using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFollower : MonoBehaviour
{
    public Transform target;

    public float turnDuration;
    public float maxForce;
    public float forceIntensity;
    
    public float oscillationAmplitude = 1;
    public float oscillationFrequency = 1;
    
    public bool moving = false;
    
    public AnimationCurve oscillation;
    public AnimationCurve turn;

    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _rigidbody);
        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {
                    
        //find the vector pointing from our position to the target
        var direction = (target.position - transform.position).normalized;
            
        //create the rotation we need to be in to look at the target
        var lookRotation = Quaternion.LookRotation(direction);
        
        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(Turn(Quaternion.identity));
            StopCoroutine(Movement());
            moving = false;
            StartCoroutine(Turn(lookRotation));
        }
        
        if(moving)
            transform.rotation = lookRotation;
    }

    IEnumerator Turn(Quaternion lookRotation)
    {
        float turnTimer = 0;
        while (turnTimer < turnDuration)
        {
            turnTimer += Time.deltaTime;

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turn.Evaluate(turnTimer/turnDuration));
            yield return null;
        }
        
        moving = true;
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

            // Force from hook to target
            _rigidbody.AddForceAtPosition(Vector3.ClampMagnitude(targetPosition - position, maxForce) * forceIntensity, position, ForceMode.Force);

            // This line of code reduces the force when the hook is close to target to avoid oscillation and make the movement converge
            _rigidbody.drag = 3 / Vector3.Distance(targetPosition, position);

            yield return null;
        }
    }
}
