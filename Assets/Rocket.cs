using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
[SerializeField] float rcsThrust =100f;
[SerializeField] float mainThrust=100f;
    Rigidbody rigidBody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
    
        rigidBody=GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       Thrust();
       Rotate();
    }
void OnCollisionEnter(Collision collision)
{
switch (collision.gameObject.tag)
{
    case "Friendly":
    {
        print("OK");
        break;
    }
    case "Fuel":
    print("Fuel");
    break;

    default:
    print("Dead");
    break;
}
}
    void Thrust()
    {
         if (Input.GetKey(KeyCode.W))
        {
rigidBody.AddRelativeForce(Vector3.up*mainThrust); 
if(!audioSource.isPlaying) 
{
audioSource.Play();
}
        }
else
{
audioSource.Stop();
}
    }

    void Rotate ()
    {
   
    rigidBody.freezeRotation = true;
                float rotaionThisFrame=rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
//float rotaionThisFrame=rcsThrust * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotaionThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
                    //    float rotaionThisFrame=rcsThrust * Time.deltaTime;

            transform.Rotate(-Vector3.forward*rotaionThisFrame);
        }
        rigidBody.freezeRotation = false;
    }
}