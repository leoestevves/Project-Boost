using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem sucessParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisabled) { return; } //Se a transição for verdadeira, ele ignora o resto do void

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;

            case "Finish":
                StartSucessSequence();
                break;            

            default:
                StartCrashSequence();                
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true; //Com isso os sons nao se repetem

        audioSource.Stop(); //Corta todos os sons que estavam ativos
        audioSource.PlayOneShot(crash);
        crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSucessSequence()
    {
        isTransitioning = true; //Com isso os sons nao se repetem

        audioSource.Stop(); //Corta todos os sons que estavam ativos
        audioSource.PlayOneShot(success);
        sucessParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Forma mais limpa de se escrever
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // liga ou desliga (true or false) o collisionDisabled
        }        
    }
}
