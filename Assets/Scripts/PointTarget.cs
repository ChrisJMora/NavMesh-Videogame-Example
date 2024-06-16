using UnityEngine;

public class PointTarget : MonoBehaviour
{
    public Camera MainCamera;
    public LayerMask Targets;

    public delegate void OnTargetChanged(Vector3 target);
    public delegate void OnTargetHit(Target target);

    public static event OnTargetChanged PositionChanged;
    public static event OnTargetHit TargetHit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

            if (Physics.Raycast(ray, out RaycastHit hit, 100, Targets))
            {
                if (hit.collider.TryGetComponent(out Target target))
                {
                    TargetHit?.Invoke(target);
                }
                else
                { 
                    PositionChanged?.Invoke(hit.point);
                }
            }
        }
    }
}
