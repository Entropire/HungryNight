using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenButtons : MonoBehaviour
{
   public void StartButton()
    {
        SceneManager.LoadScene("Mainscene"); 
    }

   public void ExitButton()
    {
        Application.Quit();
    }
}
