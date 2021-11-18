using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Rocket : MonoBehaviour
{
[SerializeField] float rcsThrust =100f;
[SerializeField] float mainThrust=100f;
[SerializeField] float levelLoadDelay=2f;

[SerializeField] AudioClip mainEngine;
[SerializeField] AudioClip success;
[SerializeField] AudioClip death;

[SerializeField] ParticleSystem mainEngineParticles;
[SerializeField] ParticleSystem successParticels;
[SerializeField] ParticleSystem deathParticles;


    Rigidbody rigidBody;

    AudioSource audioSource;
    enum State{ Alive, Dying, Transcending}
    State state = State.Alive;

    bool collisionsDisabled = false;
    // Start is called before the first frame update
    void Start()
    {
    
        rigidBody=GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state== State.Alive)
        {
       RespondToThrustInput();
       RespondToRotateInput();
        }
        if(Debug.isDebugBuild)
        {
        RespondToDebugKeys();
        }
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
collisionsDisabled=!collisionsDisabled;
        }
    }
void OnCollisionEnter(Collision collision)
{
    if (state != State.Alive || collisionsDisabled)
    {
        return;
    }
switch (collision.gameObject.tag)
{
    case "Friendly":
    {
  //      print("OK");
        break;
    }
    case "Finish":
   StartSuccessSequence();
    break;

    default:
   // print("hit something");
   StartDeathSequence();
    break;
}
}
void StartSuccessSequence()
{
     state=State.Transcending;
    //print("Hit Finish");
        audioSource.Stop();
          audioSource.PlayOneShot(success);
successParticels.Play();

Invoke("LoadNextLevel", levelLoadDelay);
}
void StartDeathSequence()
{
     state= State.Dying;
    audioSource.Stop();
  //  print("Dead");
  //  SceneManager.LoadScene(0);
  audioSource.PlayOneShot(death);
  deathParticles.Play();
  Invoke("LoadFirstLevel", levelLoadDelay);
 
}

void LoadNextLevel()
{
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
   // print(currentSceneIndex);
    int nextSceneIndex = currentSceneIndex+1;
    if(nextSceneIndex== SceneManager.sceneCountInBuildSettings)
      {
 nextSceneIndex=0;

      }

      SceneManager.LoadScene(nextSceneIndex);

}
void LoadFirstLevel()
{
     int scene = SceneManager.GetActiveScene().buildIndex;
         SceneManager.LoadScene(scene, LoadSceneMode.Single);
//SceneManager.LoadScene(0);

}
    void RespondToThrustInput()
    {
         if (Input.GetKey(KeyCode.W))
        {
ApplyThrust();
        }
else
{
audioSource.Stop();
mainEngineParticles.Stop();
}
    }
void ApplyThrust()
{
    rigidBody.AddRelativeForce(Vector3.up*mainThrust * Time.deltaTime); 
if(!audioSource.isPlaying) 
{
audioSource.PlayOneShot(mainEngine);
}
mainEngineParticles.Play();
}
    void RespondToRotateInput ()
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