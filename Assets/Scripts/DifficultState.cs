using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase base para los diferentes estados de dificultad
public class DifficultState
{
    // Método virtual para actualizar la velocidad de los enemigos
    public virtual void VelocidadEnemigos()
    {
        // La velocidad actual de los enemigos es 0 (sin cambios) ya que es la primera dificultad al empezar el juego   
        float currentEnemySpeed = 0f;
        EnemyController.enemySpeed += currentEnemySpeed;
    }
}

// Primer estado de dificultad
public class SecondDifficultyState : DifficultState
{
    // Sobrescribe el método para establecer la velocidad de los enemigos
    public override void VelocidadEnemigos()
    {
        // Incrementa la velocidad de los enemigos en 1
        float currentEnemySpeed = 1f;
        EnemyController.enemySpeed += currentEnemySpeed;
    }
}

// Segundo estado de dificultad
public class ThirdDifficultyState : DifficultState
{

    public override void VelocidadEnemigos()
    {
        // Incrementa la velocidad de los enemigos en 1
        float currentEnemySpeed = 1f;
        EnemyController.enemySpeed += currentEnemySpeed;
    }
}
