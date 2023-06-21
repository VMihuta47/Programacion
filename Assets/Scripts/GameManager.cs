using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Instancia estática de GameManager. SINGLETON PATTERN
    public UIManager uimanager; // Referencia al UIManager

    private List<GameObject> observers = new List<GameObject>(); // Lista de observadores. OBSERVER PATTERN
    private DifficultState currentState; // Variable para almacenar la clase de States que controlan la dificultad actual. STATE PATTERN

    private int currentDifficulty = 1; // Dificultad actual
    private int currentPoints; // Puntos actuales
    public bool gamePaused = true; // Variable para indicar si el juego está pausado

    private void Awake()
    {
        // Si el juego está pausado, se establece el tiempo de juego en cero
        if (gamePaused)
        {
            Time.timeScale = 0;
        }

        // Singleton pattern: asegura que solo haya una instancia de GameManager en el juego
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        // Encuentra el objeto UIObserver en la escena y agrega su GameObject como observador
        UIObserver uiObserver = FindObjectOfType<UIObserver>();
        AddObserver(uiObserver.gameObject);
        // Establece el estado de dificultad inicial y ajusta la velocidad de los enemigos
        currentState = new DifficultState();
        currentState.VelocidadEnemigos();
    }

    private void Update()
    {
        currentPoints = UIManager.uinstance.score; // Obtiene los puntos actuales del UIManager
        // Comprueba si se alcanzó cierta cantidad de puntos para aumentar la dificultad
        if (currentPoints == 10 && currentDifficulty == 1)
        {
            currentState = new SecondDifficultyState(); // Cambia al estado de dificultad 1
            currentState.VelocidadEnemigos(); // Ajusta la velocidad de los enemigos
            currentDifficulty = 2; // Actualiza la dificultad actual
        }
        else if (currentPoints == 20 && currentDifficulty == 2)
        {
            currentState = new ThirdDifficultyState();
            currentState.VelocidadEnemigos();
            currentDifficulty = 3;
        }
    }

    public void AddObserver(GameObject observer)
    {
        observers.Add(observer); // Agrega un GameObject como observador a la lista
    }

    public void RemoveObserver(GameObject observer)
    {
        observers.Remove(observer); // Remueve un GameObject de la lista de observadores
    }

    public void NotifyObservers(string eventType)
    {
        // Envía un mensaje a cada observador con el evento especificado
        foreach (GameObject observer in observers)
        {
            observer.SendMessage("OnNotify", eventType, SendMessageOptions.RequireReceiver);
        }
    }

    // Función para rRECARGAR la escena actual
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
