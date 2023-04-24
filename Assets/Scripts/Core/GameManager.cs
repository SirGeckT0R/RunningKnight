using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameHasEnded = false;

    public float restartDelay = 2f;
    [SerializeField] private Score score;
    public GameObject GameOverUI;
    [SerializeField] private GameObject lightningCanvas;


    private AudioSource backgroundMusic;

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
            score.SaveBestScore();
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        backgroundMusic.Play();
    }

    public void PlayerDied()
    {
        backgroundMusic=GameObject.Find("MusicSource").GetComponent<AudioSource>();
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
        Invoke("EndGame", 2f);
    }

    public void PlayLightningAnim() {
        lightningCanvas.SetActive(true);
    }

    public void StopLightningAnim()
    {
        lightningCanvas.SetActive(false);
    }
}
