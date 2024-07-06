using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audio;

    bool isAlive;

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;

    [SerializeField] AudioClip mainEngine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audio.isPlaying)
            {
                audio.PlayOneShot(mainEngine);
            }
        } else
        {
            audio.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            handleRotation(-rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            handleRotation(rotationThrust);
        }
    }

    void handleRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
