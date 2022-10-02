using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPingPong : MonoBehaviour
{
    [SerializeField] private Vector3 movementAxis;
    [SerializeField] private float movementMultiplier = 1f;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private AnimationCurve animationCurve;

    private Vector3 initialposition;
    private float finalScale;

    void Start()
    {
        initialposition = transform.localPosition;
    }

    void Update()
    {
        finalScale = Mathf.Abs(Mathf.Sin(Time.time * movementSpeed) * movementMultiplier);
        transform.localPosition = initialposition + movementAxis * finalScale;
    }
}
