using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public Camera cam;



    [Space(10) ,Header("Zooming Setting")]
    public float zoomDuration;
    public AnimationCurve curve;
    public float minSize;
    public float maxSize;
    public float defaulSize = 5;
    [Space(10), Header("Shaking Setting")]
    public float shakeDuration;
    public float shakeStrength;
    public int shakeVibrato;

    private bool isShaking;


    private IEnumerator zoomRoutine;



    private void Awake() => Instance = this;

    public void Shake()
    {
        if (isShaking)
            return;
        isShaking = true;
        transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato).OnComplete(() => isShaking = false);
    }

    public void ZoomIn() => Zoom(minSize);
    public void ZoomOut() => Zoom(maxSize);
    public void ZoomDefault()=>Zoom(defaulSize);


    private void Zoom(float target)
    {
        if (zoomRoutine != null)
            StopCoroutine(zoomRoutine);
        zoomRoutine = TweenHelper.ChangeFloatValue(cam.orthographicSize, target, zoomDuration, curve, (x) => cam.orthographicSize = x);
        StartCoroutine(zoomRoutine);
    }




    public void Follow(Vector3 target)
    {
        
    }


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //        Zoom(minSize);
    //    if (Input.GetKeyDown(KeyCode.S))
    //        Shake();
    //}





}
