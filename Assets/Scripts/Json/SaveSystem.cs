using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{

    List<int> highScoresSave; // Lista de puntuaciones altas para guardar
    public GameStatus gameStatus; // Referencia al estado del juego

    //Se agrega un menú al script para probar la función de guardar con un nuevo botón desplegado
    [ContextMenu("Save")]
    public void SaveToJson()
    {
        highScoresSave = HighScore.instance.GetScores(); // Obtiene las puntuaciones altas del HighScore
        gameStatus.gameData = highScoresSave; // Actualiza los datos del futuro archivo json con la tabla de sqlite

        string jsonSave = JsonUtility.ToJson(gameStatus);// Convierte el estado del juego a formato JSON

        string filePath = Application.dataPath + "/gamedata.json"; // Ruta del archivo de guardado
        File.WriteAllText(filePath, jsonSave); // Guarda el archivo JSON en la ruta especificada
        Debug.Log(jsonSave); // Imprime el JSON guardado en la consola
        Debug.Log(filePath); // Imprime la ruta del archivo guardado en la consola
        //GameManager.instance.ReloadScene(); // Recarga la escena actual
    }

    //Se agrega un menú al script para probar la función de cargar con un nuevo botón desplegado
    [ContextMenu("Load")]
    public void LoadFromJson()
    {
        string filePath = Application.dataPath + "/gamedata.json"; // Ruta del archivo de guardado
        string gameDataJson = File.ReadAllText(filePath); // Lee el contenido del archivo JSON
        gameStatus = JsonUtility.FromJson<GameStatus>(gameDataJson); // Convierte el JSON en un objeto de tipo GameStatus
        HighScore.instance.ReplaceScores(gameStatus.gameData); // Reemplaza las puntuaciones altas de la base de datos de sqlite con los datos cargados

    }

}
