using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector= new Vector3(10f, 10f, 10f);
    // Start is called before the first frame update
   [SerializeField] float period = 2f;
//    [Range(0,1)]
//    [SerializeField]  
float movementFactor;
   Vector3 startingPos;
   
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <=Mathf.Epsilon) {return;}
        float cycles =Time.time / period;
        const float tau = Mathf.PI * 2f;
        float rawSinwave = Mathf.Sin(cycles* tau);

        movementFactor=rawSinwave/2f +0.5f;
        Vector3 offset= movementFactor * movementVector;
        transform.position = startingPos + offset;
    }
}
