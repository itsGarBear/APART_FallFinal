using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FiringSolution : MonoBehaviour
{
    public Nullable<float> timeToTarget;
    public Nullable<Vector3> Calculate(Vector3 startPos, Vector3 endPos, float muzzleVel, Vector3 gravity)
    {
        timeToTarget = GetTimeToTarget(startPos, endPos, muzzleVel, gravity);

        if (!timeToTarget.HasValue)
            return null;

        //print("Time to target: " + timeToTarget);

        Vector3 deltaPos = endPos - startPos;

        //print("Vector to target: " + deltaPos);

        Vector3 n1 = deltaPos * 2f;
        Vector3 n2 = gravity * timeToTarget.Value * timeToTarget.Value;
        float d = muzzleVel * timeToTarget.Value * 2;
        Vector3 solution = (n1 - n2) / d;

        //print("Solution = " + n1 + " - " + n2 + " / " + d);
        //print("Solution = " + solution);

        return solution;
    }

    public Nullable<float> GetTimeToTarget(Vector3 startPos, Vector3 endPos, float muzzleVel, Vector3 gravity)
    {
        Vector3 deltaPos = startPos - endPos;

        float a = gravity.magnitude * gravity.magnitude;
        float b = -4 * (Vector3.Dot(gravity, deltaPos) + muzzleVel * muzzleVel);
        float c = 4 * deltaPos.magnitude * deltaPos.magnitude;

        float bSqrMinus4ac = (b * b) - (4 * a * c);

        if (bSqrMinus4ac < 0)
            return null;

        float firstTime = Mathf.Sqrt((-b + Mathf.Sqrt(bSqrMinus4ac)) / (2 * a));
        float secondTime = Mathf.Sqrt((-b - Mathf.Sqrt(bSqrMinus4ac)) / (2 * a));

        Nullable<float> timeToTarget;
        if(firstTime < 0)
        {
            if (secondTime < 0)
                return null;
            else
                timeToTarget = secondTime;
        }
        else if(secondTime < 0)
        {
            timeToTarget = firstTime;
        }
        else
        {
            timeToTarget = Mathf.Min(firstTime, secondTime);
        }

        return timeToTarget;
    }
}
