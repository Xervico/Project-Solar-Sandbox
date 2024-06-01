using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public Canvas can;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        can.enabled = false;
    }

    // Update is called once per frame
    int control = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            can.enabled = !can.enabled;

            
        }
    }
    public void Rotation()
    {
        
        SceneManager.LoadScene(2);
        TogglePause();

    }
    public void SunSize()
    {
        
        SceneManager.LoadScene(0);
        TogglePause();
    }
    public void SunDistance()
    {
        
        SceneManager.LoadScene(1);
        TogglePause();
    }
    public void Quit()
    {
        
        Application.Quit();
        TogglePause();
    }
    void TogglePause()
    {
        // If the game is not paused, pause it. Otherwise, unpause it.
        if (!isPaused)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    void Pause()
    {
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    void Unpause()
    {
        Time.timeScale = 1f; // Unpause the game
        isPaused = false;
    }
}
