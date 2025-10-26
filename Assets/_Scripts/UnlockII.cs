using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockII : MonoBehaviour
{
    public GameObject QuitMenu;

    public void ShowQuit()
    {
        QuitMenu.SetActive(true);
    }

    public void HideQuit()
    {
        QuitMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.LogWarning("Quitting Game!");
    }
}
