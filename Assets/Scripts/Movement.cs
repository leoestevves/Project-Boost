using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rocketRigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotating();
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

    void ProcessRotating()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) //Se apertar "A" e "D" ao mesmo tempo, não acontece nada
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) //Utilizar esse else if me permite utilizar o else da linha 75
        {
            RotateRight();
        }

        else //Para as particulas quando as teclas não estiverem pressionadas
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rocketRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying) //Só toca a música se ela já não estiver tocando
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }    

    void ApplyRotation(float rotationThisFrame) //Forma mais efetiva de expandir o código
    {
        rocketRigidbody.freezeRotation = true; // Com isso da para rotacionar o foguete mesmo quando atingir um obstáculo
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRigidbody.freezeRotation = false; // Retirando o freeze rotation
    }
}
