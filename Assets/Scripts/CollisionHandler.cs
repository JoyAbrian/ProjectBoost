using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadLevelDelay = 1f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audio;
    bool isTransitioning = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;

        audio.Stop();
        audio.PlayOneShot(success);
        
        successParticles.Play();
        
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        
        audio.Stop();
        audio.PlayOneShot(crash);
        
        crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", reloadLevelDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
