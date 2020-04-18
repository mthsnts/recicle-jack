using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public int qtdeLixo;
    public bool jogoPausado;
    public Text txtQtdeLixo;
    public static GameController instancia;
    public GameObject proximaFase;
    public GameObject pausePanel;

    public GameObject faseConcluidaPanel;
    
    public GameObject ultimaFasePanel;

    public GameObject comoJogarPanel;

    public GameObject itemPanel;

    public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        instancia = this;
        inicializaQtdeLixoFase();
        verificaPausarJogoFaseFinal();
    }
    void Update()
    {
        if((gameOver == null || !gameOver.activeInHierarchy)
            && (faseConcluidaPanel == null || !faseConcluidaPanel.activeInHierarchy )
            && (ultimaFasePanel == null || !ultimaFasePanel.activeInHierarchy)
            && (itemPanel == null || !itemPanel.activeInHierarchy)) {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                pausarJogo();
            }
        }    
    }

    public void verificaPausarJogoFaseFinal(){
        var scene = SceneManager.GetActiveScene();

        if(scene.name == "FaseFinal") {
            Time.timeScale = 0f;
        }
    }

    public void atualizaLixoRestante(){
        qtdeLixo--;
        txtQtdeLixo.text = "Lixo restante: " + qtdeLixo;
        if(qtdeLixo == 0){
            HabilitaProximaFase();   
        }
    }

    public void chamaGameOver(){
        FindObjectOfType<AudioManager>().musicaFase.Stop();
        FindObjectOfType<AudioManager>().somGameOver.Play(); 
        gameOver.SetActive(true);
    }

    public void iniciarJogo(){
        FindObjectOfType<AudioManager>().musicaMenu.Stop();
        SceneManager.LoadScene("Fase1");
        
    }

    public void reiniciarJogo(string fase){
        Time.timeScale = 1f;
        SceneManager.LoadScene(fase);
    }

    void HabilitaProximaFase(){
        proximaFase.SetActive(true);
    }

    void inicializaQtdeLixoFase(){
        if(txtQtdeLixo != null) {
            //valor usado para inicialização da variável no método, o valor é definido em cada cena para evitar código desnecessário
            var lixos = GameObject.FindGameObjectsWithTag("Lixo");
            qtdeLixo = lixos.Length;
            txtQtdeLixo.text = "Lixo restante: " + qtdeLixo;
        }
    }

    public void sairDoJogo(){
        Debug.Log("clicou em SAIR");
        Application.Quit();
    }

    public void chamaComoJogar(){
        comoJogarPanel.SetActive(true);
    }

    public void sairComoJogar(){
        comoJogarPanel.SetActive(false);
    }

    public void pausarJogo(){
        if(jogoPausado){
            Time.timeScale = 1f;
            jogoPausado = false;
            pausePanel.SetActive(false);
            FindObjectOfType<AudioManager>().musicaFase.Play();
        } else {
            Time.timeScale = 0f;
            jogoPausado = true;
            pausePanel.SetActive(true);
            FindObjectOfType<AudioManager>().musicaFase.Pause();
        }
    }

    public void fechaFaseFinalPanel(){
        ultimaFasePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void HabilitaProximaFasePanel(){
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().musicaFase.Pause();
        FindObjectOfType<AudioManager>().musicaFaseConcluida.Play();
        faseConcluidaPanel.SetActive(true);
    }

    public void HabilitaItemPanel(){
        Time.timeScale = 0f;
        itemPanel.SetActive(true);
    }

    public void fechaItemPanel(){
        Time.timeScale = 1f;
        itemPanel.SetActive(false);
    }

}
