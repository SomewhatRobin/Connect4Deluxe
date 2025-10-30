using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockII : MonoBehaviour
{
    public InvisHand hiddenHand;
    public GameObject QuitMenu;

    public void ShowQuit()
    {
        //Showing Quit Menu pauses the game
        hiddenHand.isPause = true;
        QuitMenu.SetActive(true);
    }

    public void HideQuit()
    {
        //Hiding Quit Menu unpauses the game
        hiddenHand.isPause = false;
        QuitMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.LogWarning("Quitting Game!");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
