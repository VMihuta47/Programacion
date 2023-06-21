using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Variables p�blicas
    public Text scoreText; // Texto para mostrar la puntuaci�n actual
    public Text highScoreText; // Texto para mostrar la puntuaci�n m�xima
    public GameObject startButton; // Bot�n para empezar la partida
    public GameObject deleteButton; // Bot�n para borrar las puntuaciones
    public Text highScores; // Texto para mostrar las puntuaciones m�s altas
    public int score; // Puntuaci�n actual del jugador
    public static UIManager uinstance; // Instancia �nica del UIManager

    private void Awake()
    {
        // Obtener la puntuaci�n m�xima guardada en PlayerPrefs
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();

        // Verificar si ya hay una instancia del UIManager y destruir este objeto
        if (uinstance == null)
        {
            uinstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Se muestra una peque�a tabla con las 10 puntuaciones m�s altas
        ShowHighScores();
        //Se declara la puntuaci�n como 0 al darle a jugar
        score = 0;
    }

    private void Update()
    {
        //Se pasa el int de la puntuaci�n al texto durante todo momento para ir actualizando la puntuaci�n
        scoreText.text = score.ToString();
    }

    //A�ade puntos
    public void AddPoints()
    {
        score += 1;
        scoreText.text = score.ToString();
        CheckHighScore();
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Verificar si la puntuaci�n actual supera la puntuaci�n m�xima guardada
    public void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    // Mostrar las puntuaciones m�s altas en el texto del men�
    public void ShowHighScores()
    {
        List<int> highScoresList = HighScore.instance.GetScores();
        highScores.text = "High Scores:\n";
        // Recorrer la lista de puntuaciones m�s altas y agregarlas al texto
        for (int i = 0; i < highScoresList.Count; i++)
        {
            highScores.text += highScoresList[i].ToString() + "\n";
        }
    }

    // Borrar todas las puntuaciones guardadas y reiniciar la puntuaci�n m�xima
    public void DeleteAllScores()
    {
        HighScore.instance.DeleteScore();
        PlayerPrefs.SetInt("HighScore", score);
        GameManager.instance.ReloadScene();
    }

    // Continuar el juego despu�s de pausarlo
    public void ResumeGame()
    {
        if (GameManager.instance.gamePaused)
        {
            GameManager.instance.gamePaused = false;
            Time.timeScale = 1;
            HighScore.instance.GetScores();
            startButton.SetActive(false);
            highScores.gameObject.SetActive(false);
            deleteButton.SetActive(false);
        }

    }

}
