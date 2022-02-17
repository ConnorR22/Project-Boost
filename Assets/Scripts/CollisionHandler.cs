using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    float loadDelay = 1.5f;
    AudioSource aSource;

    [SerializeField] bool isAlive = true;

    [SerializeField] AudioClip crashShip;
    [SerializeField] AudioClip finishLevel;

    // Prevent additional SFX from playing in a current attempt
    bool isTransitioning = false;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    void ReloadLevel()
    {
        Respawn();
        int current_SceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_SceneIndex);
    }

    void LoadNextLevel()
    {

        StopPlayerMovement();
        Respawn();

        int next_SceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (next_SceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }

        else
        {
            SceneManager.LoadScene(next_SceneIndex);
        }
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Okay Collision");
                break;
            case "Finish":
                LevelSuccess();
                Debug.Log("Yay you are done");
                break;
            case "Fuel":
                Debug.Log("You attained more fuel");
                break;
            default:
                Crash();
                break;
        }
    }

    private void LevelSuccess()
    {
        isTransitioning = true;

        // Cease thruster sound, play level success audio
        aSource.Stop();
        aSource.PlayOneShot(finishLevel);
        
        StopPlayerMovement();
        Invoke("LoadNextLevel", loadDelay);
    }

    void Respawn()
    {
        isAlive = true;
    }

    void StopPlayerMovement()
    {
        gameObject.GetComponent<Movement>().enabled = false;
    }

    void Crash()
    {
        isTransitioning = true;

        aSource.Stop();
        aSource.PlayOneShot(crashShip);
        
        isAlive = false;
        
        StopPlayerMovement();
        Invoke("ReloadLevel", loadDelay);
    }

}
