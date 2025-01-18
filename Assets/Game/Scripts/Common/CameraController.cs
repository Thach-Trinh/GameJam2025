using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public Camera cam;
    public float defaulSize = 5;

    [Space(10), Header("Following Setting")]
    public float smoothTime = 0.3f;
    public Vector2 offset;
    private Vector2 velocity;


    [Space(10) ,Header("Zooming Setting")]
    public float zoomDuration;
    public AnimationCurve curve;
    public float minSize;
    public float maxSize;

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
        cam.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato).OnComplete(() => isShaking = false);
    }

    public void ZoomIn() => Zoom(minSize);
    public void ZoomOut() => Zoom(maxSize);
    public void ZoomDefault()=>Zoom(defaulSize);

    public void Follow(Vector3 target)
    {
        Vector2 requiredPos = (Vector2)target + offset;
        transform.position = Vector2.SmoothDamp(transform.position, requiredPos, ref velocity, smoothTime);
    }

    private void Zoom(float target)
    {
        if (zoomRoutine != null)
            StopCoroutine(zoomRoutine);
        zoomRoutine = TweenHelper.ChangeFloatValue(cam.orthographicSize, target, zoomDuration, curve, (x) => cam.orthographicSize = x);
        StartCoroutine(zoomRoutine);
    }
}
