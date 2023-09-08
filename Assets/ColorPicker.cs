using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] Transform indicator;
    [SerializeField] float centerRadius;
    [SerializeField] Vector2 rAngleRange;
    [SerializeField] Vector2 rgAngleRange;
    [SerializeField] Vector2 gAngleRange;
    [SerializeField] Vector2 gbAngleRange;
    [SerializeField] Vector2 bAngleRange;
    [SerializeField] Vector2 brAngleRange;

    float indicatorAngle;
    ColorChannels channels;

    public Transform Indicator { get { return indicator; } }

    private void Awake()
    {
        channels = GetComponent<ColorChannels>();
    }

    private void Start()
    {
        CalculateIndicatorAngle();
        enabled = false;
    }

    public void PickColor(Vector2 pointerPosition)
    {
        Vector2 relativePointerPosition = pointerPosition - new Vector2(transform.position.x, transform.position.y); // get pointer position relative to the color picker

        if (Vector2.Distance(relativePointerPosition, Vector2.zero) < centerRadius)
        {
            PickDefault();
        }
        else
        {
            RotateIndicator(Vector2.SignedAngle(Indicator.localPosition, relativePointerPosition));
        }
    }

    void RotateIndicator(float angle)
    {
        indicator.RotateAround(transform.position, Vector3.forward, angle);
        CalculateIndicatorAngle();
        PickColor();
    }

    void CalculateIndicatorAngle()
    {
        float angle = Vector2.Angle(Vector2.right, indicator.localPosition);
        if (indicator.localPosition.y < 0)
        {
            angle = 180f + (180f - angle);
        }
        indicatorAngle = angle;
    }

    public void PickDefault()
    {
        RotateIndicator(Vector2.SignedAngle(indicator.localPosition, Vector2.down));
        CalculateIndicatorAngle();
        channels.ActivateRGB();
    }

    void PickColor()
    {
        if (indicatorAngle >= rAngleRange.x &&  indicatorAngle < rAngleRange.y)
        {
            channels.ActivateR();
        } else if (indicatorAngle >= rgAngleRange.x && indicatorAngle < rgAngleRange.y)
        {
            channels.ActivateRG();
        } else if (indicatorAngle >= gAngleRange.x && indicatorAngle < gAngleRange.y)
        {
            channels.ActivateG();
        } else if (indicatorAngle >= gbAngleRange.x && indicatorAngle < gbAngleRange.y)
        {
            channels.ActivateGB();
        } else if (indicatorAngle >= bAngleRange.x && indicatorAngle < bAngleRange.y)
        {
            channels.ActivateB();
        } else if (indicatorAngle >= brAngleRange.x && indicatorAngle < brAngleRange.y)
        {
            channels.ActivateBR();
        } else
        {
            channels.ActivateRGB();
        }
    }

}
