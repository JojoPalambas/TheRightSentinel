using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform currentTransform;

    public Transform mainMenuTransform;
    public Transform selectMenuTransform;
    public Transform scoresMenuTransform;
    public Transform gameTransform;

    [Range(0, 1)]
    public float bias;

    // Start is called before the first frame update
    void Start()
    {
        GeneralLinker.cameraManager = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentTransform.position, bias);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentTransform.rotation, bias);
    }

    public void Shake(float intensity)
    {
        transform.Translate(0, 0, intensity);
    }
}
