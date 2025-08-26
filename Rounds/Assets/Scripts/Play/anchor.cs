using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(DistanceJoint2D))]
public class anchor : MonoBehaviour
{
    public DistanceJoint2D joint;     // 연결된 Joint
    public float zOffset = 0f;        // 로프 Z깊이(렌더 우선순위 조정용)

    [Header("옵션: 곡선/쳐짐")]
    [Min(2)] public int segments = 2; // 2면 직선. 6~12면 살짝 곡선
    public float sag = 0f;            // 0이면 직선, 값↑일수록 중력 방향으로 쳐짐

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

    // 물리 후 시각 업데이트 → 떨림 감소
    void LateUpdate()
    {
        if (!joint) return;

        // A(자기쪽) 앵커: 로컬 → 월드변환
        Vector3 a = rbA.transform.TransformPoint((Vector3)joint.anchor);

        // B(반대쪽) 앵커: 연결 대상이 있으면 로컬→월드, 없으면 이미 월드 좌표
        Vector3 b = joint.connectedBody
            ? joint.connectedBody.transform.TransformPoint((Vector3)joint.connectedAnchor)
            : (Vector3)joint.connectedAnchor;

        a.z = zOffset; b.z = zOffset;

        if (segments <= 2 || sag <= 0f)
        {
            // 직선
            if (lr.positionCount != 2) lr.positionCount = 2;
            lr.SetPosition(0, a);
            lr.SetPosition(1, b);
            return;
        }

        // 간단한 곡선(중력 방향으로 부드러운 처짐)
        if (lr.positionCount != segments) lr.positionCount = segments;
        for (int i = 0; i < segments; i++)
        {
            float t = i / (segments - 1f);
            Vector3 p = Vector3.Lerp(a, b, t);
            // 사인 곡선으로 가운데가 가장 많이 처지게
            p += Vector3.down * Mathf.Sin(Mathf.PI * t) * sag;
            lr.SetPosition(i, p);
        }
    }
}


