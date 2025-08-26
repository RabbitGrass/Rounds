using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(DistanceJoint2D))]
public class anchor : MonoBehaviour
{
    public DistanceJoint2D joint;     // ����� Joint
    public float zOffset = 0f;        // ���� Z����(���� �켱���� ������)

    [Header("�ɼ�: �/����")]
    [Min(2)] public int segments = 2; // 2�� ����. 6~12�� ��¦ �
    public float sag = 0f;            // 0�̸� ����, �����ϼ��� �߷� �������� ����

    LineRenderer lr;
    Rigidbody2D rbA;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        rbA = GetComponent<Rigidbody2D>();
        if (!joint) joint = GetComponent<DistanceJoint2D>();

        lr.useWorldSpace = true;
        lr.positionCount = Mathf.Max(2, segments);
    }

    // ���� �� �ð� ������Ʈ �� ���� ����
    void LateUpdate()
    {
        if (!joint) return;

        // A(�ڱ���) ��Ŀ: ���� �� ���庯ȯ
        Vector3 a = rbA.transform.TransformPoint((Vector3)joint.anchor);

        // B(�ݴ���) ��Ŀ: ���� ����� ������ ���á����, ������ �̹� ���� ��ǥ
        Vector3 b = joint.connectedBody
            ? joint.connectedBody.transform.TransformPoint((Vector3)joint.connectedAnchor)
            : (Vector3)joint.connectedAnchor;

        a.z = zOffset; b.z = zOffset;

        if (segments <= 2 || sag <= 0f)
        {
            // ����
            if (lr.positionCount != 2) lr.positionCount = 2;
            lr.SetPosition(0, a);
            lr.SetPosition(1, b);
            return;
        }

        // ������ �(�߷� �������� �ε巯�� ó��)
        if (lr.positionCount != segments) lr.positionCount = segments;
        for (int i = 0; i < segments; i++)
        {
            float t = i / (segments - 1f);
            Vector3 p = Vector3.Lerp(a, b, t);
            // ���� ����� ����� ���� ���� ó����
            p += Vector3.down * Mathf.Sin(Mathf.PI * t) * sag;
            lr.SetPosition(i, p);
        }
    }
}


