
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject helpPanel;
    bool heeelp = false;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
    public void Help()
    {
        if(heeelp == false)
        {
            helpPanel.SetActive(true);
            heeelp = true;
        }
        else{
            helpPanel.SetActive(false);
            heeelp = false;
        }
        
    }

}
