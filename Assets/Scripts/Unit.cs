using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Unit Properties")]
    public Vector3 inputMovement = new Vector3();
    public float moveSpeed = 5f;

    public Rigidbody2D _rb;
    

    private void FixedUpdate()
    {
        HandleMovements();
    }

    private void HandleMovements()
    {
        Vector3 position = transform.position;
        transform.position = position + inputMovement.normalized * (moveSpeed * Time.fixedDeltaTime);
        // _rb.MovePosition(_rb.position + );
    }
}
