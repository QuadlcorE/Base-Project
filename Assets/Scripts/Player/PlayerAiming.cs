using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public Vector3 turn { get; private set; }

    public void Aim()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        turn = (mousePosition - transform.position).normalized;
        float zRot = Mathf.Atan2(turn.y, turn.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, zRot);
    }
}
