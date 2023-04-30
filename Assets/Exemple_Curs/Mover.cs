using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] public float speed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed));
        }

        return;
        float deltaTime = Time.deltaTime;
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Transform tr = GetComponent<Transform>();
        tr.position += movement * speed * deltaTime;
    }
}
