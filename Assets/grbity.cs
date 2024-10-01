using UnityEngine;
using System.Collections;

public class gravityController : MonoBehaviour
{

    public Vector3 localGravity;

    private Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        setLocalGravity();
    }

    void setLocalGravity()
    {
        rb.AddForce(localGravity, ForceMode.Acceleration);
    }

}
