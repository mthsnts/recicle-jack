using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProximoNivel : MonoBehaviour
{
    //public string fase;
    public GameObject faseConcluidaPanel;
    void OnCollisionEnter2D(Collision2D colisao)
    {
        //se jogador encostar no objeto da próxima fase, é iniciada a nova scene da próxima fase 
        if(colisao.gameObject.tag == "Player"){
           //SceneManager.LoadScene(fase);
           

            GameController.instancia.HabilitaProximaFasePanel();
        }
    }


}
