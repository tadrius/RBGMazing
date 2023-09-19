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
    ColorFilter filter;

    public Transform Indicator { get { return indicator; } }

    private void Awake()
    {
        filter = GetComponent<ColorFilter>();
    }

    private void Start()
    {
        CalculateIndicatorAngle();
        enabled = false;
    }

    public void Reset()
    {
        PickDefaultColor();
    }

    public void PickColor(Vector2 pointerPosition)
    {
        Vector2 relativePointerPosition = pointerPosition - new Vector2(transform.position.x, transform.position.y); // get pointer position relative to the color picker

        if (Vector2.Distance(relativePointerPosition, Vector2.zero) < (centerRadius * transform.lossyScale.x))
        {
            PickDefaultColor();
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

    public void PickDefaultColor()
    {
        RotateIndicator(Vector2.SignedAngle(indicator.localPosition, Vector2.down));
        CalculateIndicatorAngle();
        filter.ActivateRGB();
    }

    void PickColor()
    {
        if (indicatorAngle >= rAngleRange.x &&  indicatorAngle < rAngleRange.y)
        {
            filter.ActivateR();
        } else if (indicatorAngle >= rgAngleRange.x && indicatorAngle < rgAngleRange.y)
        {
            filter.ActivateRG();
        } else if (indicatorAngle >= gAngleRange.x && indicatorAngle < gAngleRange.y)
        {
            filter.ActivateG();
        } else if (indicatorAngle >= gbAngleRange.x && indicatorAngle < gbAngleRange.y)
        {
            filter.ActivateGB();
        } else if (indicatorAngle >= bAngleRange.x && indicatorAngle < bAngleRange.y)
        {
            filter.ActivateB();
        } else if (indicatorAngle >= brAngleRange.x && indicatorAngle < brAngleRange.y)
        {
            filter.ActivateBR();
        } else
        {
            filter.ActivateRGB();
        }
    }

}
