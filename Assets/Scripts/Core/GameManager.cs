using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameHasEnded = false;

    public float restartDelay = 2f;
    [SerializeField] private Score score;
    public GameObject GameOverUI;

    public void CompleteLevel()
    {
        GameOverUI.SetActive(true);
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            GameOverUI.SetActive(true); 
            Debug.Log("GAME OVER");
            score.SaveBestScore();
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
