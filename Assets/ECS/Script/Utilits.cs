using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class Utilits
{
    public static List<Collider> GetAllColliders(this GameObject gameObject)
    {
        if (gameObject!=null)
        {
            Collider[] massivCollider = gameObject.GetComponents<Collider>();
            List<Collider> tempList = new List<Collider>(massivCollider);
            return tempList;
        }
        else
        {
            return null;
        }
    }

    private static float3 Abs(float3 argument)
    {
        return new float3(Mathf.Abs(argument.x),Mathf.Abs(argument.y),Mathf.Abs(argument.z));
    }

    private static float Max(float3 argument)
    {
        return Mathf.Max(argument.x,Mathf.Max(argument.y,argument.z));
    }
    public static void ToWorldSpaceBox(this BoxCollider box, out float3 center, out float3 halfExtens, out quaternion orientation)
    {
        Transform transform = box.transform;
        orientation = transform.rotation;
        center = transform.TransformPoint(box.center);

        float3 lossyScale = transform.lossyScale;
        float3 scale = Abs(lossyScale);
        halfExtens = Vector3.Scale(scale, box.size) * 0.5f;
    }

    public static void ToWorldSpaceCapsule(this CapsuleCollider capsule, out float3 point0, out float3 point1, out float radius)
    {
        Transform transform = capsule.transform;
        float3 center = transform.TransformPoint(capsule.center);
        radius = 0f;
        float heigt = 0f;
        float3 lossyScale = Abs(transform.lossyScale);
        float3 dir = float3.zero;
        switch (capsule.direction)
        {
            case 0://x
                radius = Mathf.Max(lossyScale.y, lossyScale.z) * capsule.radius;
                heigt = lossyScale.x * capsule.height;
                dir = capsule.transform.TransformDirection(Vector3.right);
                break;
            case 1://y
                radius = Mathf.Max(lossyScale.x, lossyScale.z) * capsule.radius;
                heigt = lossyScale.y * capsule.height;
                dir = capsule.transform.TransformDirection(Vector3.up);
                break;
            case 2://z
                radius = Mathf.Max(lossyScale.x, lossyScale.y) * capsule.radius;
                heigt = lossyScale.z * capsule.height;
                dir = capsule.transform.TransformDirection(Vector3.forward);
                break;
            default:
                break;
        }

        if (heigt<radius*2f)
        {
            dir = Vector3.zero;
        }

        point0 = center + dir * (heigt * 0.5f - radius);
        point1 = center - dir * (heigt * 0.5f - radius);
    }

    public static void ToWorldSpaceSphere(this SphereCollider sphere, out float3 center, out float radius)
    {
        Transform transform = sphere.transform;
        center = transform.TransformPoint(sphere.center);
        radius = sphere.radius*Max(Abs(transform.lossyScale));
    }
}
