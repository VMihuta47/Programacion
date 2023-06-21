using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Variables públicas
    public Text scoreText; // Texto para mostrar la puntuación actual
    public Text highScoreText; // Texto para mostrar la puntuación máxima
    public GameObject startButton; // Botón para empezar la partida
    public GameObject deleteButton; // Botón para borrar las puntuaciones
    public Text highScores; // Texto para mostrar las puntuaciones más altas
    public int score; // Puntuación actual del jugador
    public static UIManager uinstance; // Instancia única del UIManager

    private void Awake()
    {
        // Obtener la puntuación máxima guardada en PlayerPrefs
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
        //Se muestra una pequeña tabla con las 10 puntuaciones más altas
        ShowHighScores();
        //Se declara la puntuación como 0 al darle a jugar
        score = 0;
    }

    private void Update()
    {
        //Se pasa el int de la puntuación al texto durante todo momento para ir actualizando la puntuación
        scoreText.text = score.ToString();
    }

    //Añade puntos
    public void AddPoints()
    {
        score += 1;
        scoreText.text = score.ToString();
        CheckHighScore();
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Verificar si la puntuación actual supera la puntuación máxima guardada
    public void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    // Mostrar las puntuaciones más altas en el texto del menú
    public void ShowHighScores()
    {
        List<int> highScoresList = HighScore.instance.GetScores();
        highScores.text = "High Scores:\n";
        // Recorrer la lista de puntuaciones más altas y agregarlas al texto
        for (int i = 0; i < highScoresList.Count; i++)
        {
            highScores.text += highScoresList[i].ToString() + "\n";
        }
    }

    // Borrar todas las puntuaciones guardadas y reiniciar la puntuación máxima
    public void DeleteAllScores()
    {
        HighScore.instance.DeleteScore();
        PlayerPrefs.SetInt("HighScore", score);
        GameManager.instance.ReloadScene();
    }

    // Continuar el juego después de pausarlo
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
