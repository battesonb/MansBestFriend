using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class InverseKinematics2D : MonoBehaviour
{
    // TODO implement limits.
    public int chainLength = 2;
    [Range(0.001f, 0.5f)]
    public float laziness = 0.25f;
    public Transform endTransform;
    public bool invert = false; // Cheeky way of implementing limits.

    void OnValidate()
    {
        chainLength = Mathf.Max(chainLength, 1);
    }

    void LateUpdate()
    {
        if (chainLength == 1)
        {
            PointToTarget(endTransform.parent);
        }
        else
        {
            // Assume chain length is 2, I'm not going to code the nth case here, as I don't need it.
            CalculateIK();
        }
    }

    void CalculateIK()
    {
        // base case for 2, will improve this at home at some point
        Transform armature = endTransform.parent;
        Transform armatureParent = armature.parent;

        float parentRestingAngle = GetRestingAngle(armature); // relative to armatureParent

        // (From a)
        Vector2 targetDisplacement = transform.position - armatureParent.position;

        // Named according to Law of Cosines notation.
        float a = (armatureParent.position - armature.position).magnitude;
        float c = (armature.position - endTransform.position).magnitude;
        float b = targetDisplacement.magnitude;
        if (a + c < b)
        {
            // Just make everything point to target. Starting from the parent.
            PointToTarget(endTransform.parent.parent);
            PointToTarget(endTransform.parent);
            return;
        }
        float denom = (2 * a * b);
        float angle;
        if (denom != 0)
            angle = Mathf.Acos((a * a + b * b - c * c) / denom);
        else
            angle = 0;

        // And Rotate
        Vector2 displacement = transform.position - armatureParent.position;
        float targetAngle = Mathf.Atan2(displacement.y, displacement.x);

        bool modelInverted = (int)Mathf.Sign(transform.root.localScale.x) == -1;
        if (modelInverted)
        {
            targetAngle *= -1;
            targetAngle += Mathf.PI;
        }

        if (invert)
        {
            armatureParent.rotation = Quaternion.Euler(0, 0, (targetAngle + angle - parentRestingAngle) * Mathf.Rad2Deg);
        }
        else
        {
            armatureParent.rotation = Quaternion.Euler(0, 0, (targetAngle - angle - parentRestingAngle) * Mathf.Rad2Deg);
        }

        PointToTarget(endTransform.parent);
    }

    void PointToTarget(Transform armature)
    {
        float restingAngle = GetRestingAngle(armature.GetChild(0));

        Vector2 displacement = transform.position - armature.position;
        float angle = Mathf.Atan2(displacement.y, displacement.x);

        if ((int)Mathf.Sign(transform.root.localScale.x) == -1)
        {
            angle *= -1;
            angle -= Mathf.PI;
        }
        armature.rotation = Quaternion.Euler(0, 0, (angle - restingAngle) * Mathf.Rad2Deg);
    }

    float GetRestingAngle(Transform trans)
    {
        Vector2 displacement = trans.localPosition;
        return Mathf.Atan2(displacement.y, displacement.x);
    }
}
