using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audio;

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;

    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audio.Stop();
        mainEngineParticles.Stop();
    }

    void handleRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    private void RotateLeft()
    {
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
        handleRotation(-rotationThrust);
    }

    private void RotateRight()
    {
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
        handleRotation(rotationThrust);
    }

    private void StopRotation()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }
}
