using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //main menu
    public void StartGame()
    {
        //PlayerScript.lifes = 3;
        SceneManager.LoadScene("Thiago Scene 2");
    }

    public void Rules()
    {
        //SceneManager.LoadScene("RulesScreen");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Options()
    {
        //SceneManager.LoadScene("OptionsScreen");
    }

    //options
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
