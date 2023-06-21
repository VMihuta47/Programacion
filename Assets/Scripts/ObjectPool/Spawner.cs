using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Prefab del enemigo a generar
    public GameObject Enemy;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gamePaused)
        {
            //Los enemigos saldrán si el número aleatorio entre 0 y 100 sale menor que 5
            if (Random.Range(0, 100) < 5)
            {

                //Referencia al objeto de la lista de objetos creados mediante el Pool
                GameObject a = PoolObjects.singleton.Get("Enemy");
                //Si el objeto que recibido no está vacío
                if (a != null)
                {
                    //Se le envía una posición a ese enemigo concreto
                    a.transform.position = this.transform.position + new Vector3(Random.Range(-8, 8), 0, 0);
                    //Se activa
                    a.SetActive(true);
                }
            }
        }
        

    }
}
