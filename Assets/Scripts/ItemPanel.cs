using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPanel : MonoBehaviour
{
    //public string fase;
    public GameObject itemPanel;
     void OnTriggerEnter2D(Collider2D collider)
    {
        //se jogador encostar nos itens, é exibido o Panel
        if(collider.gameObject.tag == "Player"){

            GameController.instancia.HabilitaItemPanel();
        }
    }


}
