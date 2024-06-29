using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    public void Move(Vector3 direction)
    {
        direction.Normalize();
    }

    public void MoveLocal(float horizontalInput, float verticalInput)
    {
        Vector3 temp = new Vector3(horizontalInput, verticalInput, 0);
        temp = temp.normalized * speed * Time.deltaTime;
        transform.position += temp;
    }
}
