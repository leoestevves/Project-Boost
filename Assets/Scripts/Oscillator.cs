using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;        
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } /* Evita o erro caso o periodo fique em 0, comparações com float podem dar problemas
                                                  Mathf.Epsilon é o número próximo de 0 */

        float cycles = Time.time / period; /* Cada ciclo vai ter o tempo dividido pelo periodo, quanto maior o periodo, menor a velocidade
                                            do ciclo */

        const float tau = Mathf.PI * 2;   //tau vai ter o valor de 2pi
        float rawSinWave = Mathf.Sin(cycles * tau);    //indo de -1 até +1, 2pi representa a "onda" inteira

        movementFactor = (rawSinWave + 1f) / 2f; /* dessa forma, ao inves de ir -1 até 1, vai de 0 até 1 (rawSinWave + 1 = 0 até 2,
                                                 dividido por 2 fica rawSinWave = 0 até 1) */

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
