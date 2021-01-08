using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BolaControlador : MonoBehaviour
{
    [SerializeField]
    private float vel = 1f, limiteVel = 2.5f;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    public static bool gameOver = false;

    [SerializeField]
    private int moedasNum = 0;

    [SerializeField]
    private Text txtMoedas;

    [SerializeField]
    private GameObject Particulas;

    [SerializeField]
    private Text txtBtn, txtGO;

    [SerializeField]
    private Image imgBtn, imgFundo;

    [SerializeField]
    private bool show;


    void Carrega(Scene ContextMenuItemAttribute, LoadSceneMode modo)
    {
        gameOver = false;
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += Carrega;
    }

    // Start is called before the first frame update
    void Start()
    {
        moedasNum = PlayerPrefs.GetInt("MoedasGame");
        txtMoedas.text = moedasNum.ToString();

        rb = GetComponent<Rigidbody>();

        //Definindo a velocidade do objeto
        rb.velocity = new Vector3(vel, 0, 0);

        txtBtn = GameObject.FindWithTag("txtBtn").GetComponent<Text>();
        txtGO = GameObject.FindWithTag("txtGO").GetComponent<Text>();
        imgBtn = GameObject.FindWithTag("imgBtn").GetComponent<Image>();
        imgFundo = GameObject.FindWithTag("imgFundo").GetComponent<Image>();

        show = true;
        txtBtn.enabled = false;
        txtGO.enabled = false;
        imgBtn.enabled = false;
        imgFundo.enabled = false;

        StartCoroutine(AjustaVel());
    }

    // Update is called once per frame
    void Update()
    {
        //Para realizar o comando, verificar se a tecla espaço foi pressionada
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            BolaMov();
        }

        //Método para verificar se a bolinha está caindo, se sim, gameOver = true
        if (!Physics.Raycast(transform.position, Vector3.down, 1))
        {
            gameOver = true;
            rb.useGravity = true;
        }

        if (gameOver && show)
        {
            //Salvando o jogo
            PlayerPrefs.SetInt("MoedasGame", moedasNum);

            txtBtn.enabled = true;
            txtGO.enabled = true;
            imgBtn.enabled = true;
            imgFundo.enabled = true;
            show = false;
        }
    }

    //Método para alterar o eixo de movimento do x para y ou para z
    void BolaMov()
    {
        if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, vel);
        }
        else if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(vel, 0, 0);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //Verifica se a bolinha colide com a moeda para coletar
        if (col.gameObject.CompareTag("Moeda"))
        {
            Destroy(col.gameObject);
            moedasNum++;
            txtMoedas.text = moedasNum.ToString();
            Instantiate(Particulas, transform.position, Quaternion.identity);
        }
    }

    IEnumerator AjustaVel()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(2);
            if (vel <= limiteVel)
            {
                vel += 0.2f;
            }
        }
    }

    public void JogarNovamente()
    {
        SceneManager.LoadScene(0);
    }
}
