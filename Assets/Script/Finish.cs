using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource next_LeverSound;
    private void Start()
    {
        next_LeverSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name =="Player")
        {
            next_LeverSound.Play();
            Invoke("CompleteLever", 2f);
        }
    }

    private void CompleteLever()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (nextSceneIndex < sceneCount)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Đã hoàn thành cấp độ cuối cùng");
        }    
    }
}
