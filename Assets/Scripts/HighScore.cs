using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

// Descripción: Gestiona el puntaje alto del juego y su almacenamiento en una base de datos.
public class HighScore : MonoBehaviour
{
    public static HighScore instance; // Instancia única de la clase HighScore
    private string connectionString; // Cadena de conexión a la base de datos

    private void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Establece la cadena de conexión a la base de datos
        connectionString = "URI=file:" + Application.dataPath + "/HighScoreDB.db";
    }

    // Inserta un nuevo puntaje en la base de datos de sqlite
    public void InsertScore(int newScore)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open(); // Abre la conexión a la base de datos
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = string.Format("INSERT INTO Scores(Score) VALUES (\"{0}\")", newScore); //Inserta en la primera columna la última puntuación obtenida

                // Establece la consulta SQL
                dbCmd.CommandText = sqlQuery;
                // Ejecuta la consulta y obtiene el valor devuelto (si lo hay)
                dbCmd.ExecuteScalar();
                // Cierra la conexión a la base de datos, muy importante
                dbConnection.Close();
            }
        }
    }

    // Obtiene los puntajes más altos de la base de datos
    public List<int> GetScores()
    {
        List<int> scores = new List<int>();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT Score FROM Scores ORDER BY Score DESC LIMIT 10"; //Se ordenan los 10 valores más altos con un límite de 10

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Obtiene el puntaje de la columna "Score" en el resultado de la consulta
                        int score = reader.GetInt32(0);
                        scores.Add(score);
                    }

                    // Cierra la conexión a la base de datos y el lector
                    dbConnection.Close();
                    reader.Close();

                }
            }
        }
        return scores;

    }

    // Elimina todos los puntajes de la base de datos
    public void DeleteScore()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "DELETE FROM Scores";

                dbCmd.CommandText = sqlQuery;
                // Ejecuta la consulta sin obtener ningún valor devuelto
                dbCmd.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
    }

    // Reemplaza los puntajes existentes en la base de datos con una nueva lista de puntajes para cuando se cargue partida
    public void ReplaceScores(List<int> newScores)
    {
        // Eliminar todos los elementos existentes en la tabla
        DeleteScore();

        // Insertar los nuevos elementos en la tabla por cada elemento score de la lista newScores que se le aporte a la función+
        //Será siempre la cargada al cargar partida
        foreach (int score in newScores)
        {
            InsertScore(score);
        }
    }

}
