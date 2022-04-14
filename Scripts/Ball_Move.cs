using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Ball_Move : MonoBehaviour
{
    //Game_Manager gameManager;
    [SerializeField] private GameObject particle_System_Splash;
    [SerializeField] private GameObject cylinder;
    [SerializeField] private AudioSource Sound_Jump;
    [SerializeField] private AudioSource Sound_Space_Slice;
    [SerializeField] private AudioSource Sound_Game_Over;


    [SerializeField] private ObjectManager objectManager;
    //[SerializeField] private GameObject particle_System_Splash_Cylinder;

    public static Ball_Move Instance = null;
    private Rigidbody rb;

    private bool isDead = false;
    private int combo = 0;
    [SerializeField] private bool isJump;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectManager = ObjectManager.Instance;
        //gameManager = Game_Manager.Instance;
    }

    void Update()   //FPS
    {
        Vector3 v3 = new Vector3(Input.GetAxis("Vertical"), 0, 0);  //Vector3 X, Y, Z koordinatlarında yön belirtiriyoruz.
        rb.AddForce(v3 * 5);    //5 * Kuvvet uyguluyoruz.
    }
    void FixedUpdate()  //1 saniyede 50 defa çağırıyoruz.
    {
        if (rb.IsSleeping())    //rb uyuyorsa tetiklenmiyorsa
            rb.WakeUp();    //rb yi kaldır.

        if (isJump)
        {
            isJump = false;
            gameObject.GetComponent<Rigidbody>().drag = 1f;
            rb.AddForce(0, 7f, 0, ForceMode.Impulse);   //Sürekli ve Sabit olarak zıplatma için gerekli parametreler.
            jump_Play();

            Invoke("isJumpMakeTrue", 0.02f);
        }
    }
    void isJumpMakeTrue()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)   //ÇARPIŞMA Durumu
    {
        Transform parent = collision.gameObject.transform.parent;
        if (combo >= 3)
        {
            parent.GetComponent<Disk_Move>().ChangeColor();
            Destroy(collision.transform.parent.gameObject, 0.05f);

            Instantiate(plakalar, new Vector3(0, plaka_mesafesi, 0), Quaternion.Euler(new Vector3(0, Random.Range(1, 360), 0)));
            Destroy(parent.gameObject, 3f);  //Geçilen plakayı sil.

            gameObject.GetComponent<Rigidbody>().drag -= 0.33f;
            plaka_mesafesi -= 2;
            slice_Play();

            //cylinder.transform.localScale += new Vector3(0, 2, 0);  //Her geçmede silindirin Y eksenine 2 birim uzaklık veriyorum.
            cylinder.transform.position += new Vector3(0, -2, 0);       //Her geçmede silindirin Y ekseninde ki pozisyonunu 2 birim aşşağıya veriyorum.

            Canvas_Manager.Instance.UpdateScore();
        }
        else
        {
            if (collision.gameObject.CompareTag("FailDisc"))
            {
                IsDead = true;
                Canvas_Manager.Instance.GameOver();
                game_over_Play();
                Invoke("Restart", 3f);
            }
            if (collision.gameObject.CompareTag("plaka"))   //Mevcut nesne(Ball/Top) "plaka" ya çarparsa rb ye Force uyguluyoruz.
            {
                isJump = true;
                GameObject particle = Instantiate(particle_System_Splash, transform.position, Quaternion.identity);
                particle.transform.parent = collision.transform;
                Destroy(particle, 1f);

                this.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
        if (collision != null)
            combo = 0;
        //}
        //if (collision.gameObject.CompareTag("plaka_double"))   //Mevcut nesne(Ball/Top) "plaka" ya çarparsa rb ye Force uyguluyoruz.
        //{
        //    isJump = true;
        //    GameObject particle = Instantiate(particle_System_Splash, transform.position, Quaternion.identity);
        //    particle.transform.parent = collision.transform;
        //    Destroy(particle, 1f);

        //    this.gameObject.GetComponent<Collider>().enabled = false;
        //}
        //}
        //if (!collision.gameObject.CompareTag("plaka_one") && collision.gameObject.CompareTag("plaka_double"))   //Mevcut nesne(Ball/Top) "plaka" ya çarparsa rb ye Force uyguluyoruz.
        //{
        //    isJump = true;
        //    GameObject particle = Instantiate(particle_System_Splash, transform.position, Quaternion.identity);
        //    particle.transform.parent = collision.transform;
        //    Destroy(particle, 1f);
        //}
        //if (collision.gameObject.CompareTag("plaka_one") && collision.gameObject.CompareTag("plaka_double"))
        //{
        //    isJump = true;
        //    GameObject particle = Instantiate(particle_System_Splash, transform.position, Quaternion.identity);
        //    particle.transform.parent = collision.transform;
        //    Destroy(particle, 1f);
        //}
    }

    [SerializeField] private GameObject plakalar;
    private float plaka_mesafesi = 0f;

    private void OnTriggerEnter(Collider other) //GÖRÜNMEYEN Boş dilim içerisinde temas ederse (İçinden geçiyorsa)
    {
        if (other.gameObject.CompareTag("Space_Slice"))
        {
            Instantiate(plakalar, new Vector3(0, plaka_mesafesi, 0), Quaternion.Euler(new Vector3(0, Random.Range(1, 360), 0)));

            Transform parent = other.gameObject.transform.parent;
            for (int i = 0; parent.childCount > 0; i++)
            {
                bangAndDestroy(parent.GetChild(0).gameObject, 0.01f, 1500f);
            }

            Destroy(parent.gameObject, 3f);  //Geçilen plakayı sil.
            gameObject.GetComponent<Rigidbody>().drag -= 0.33f;
            combo += 1;
            Debug.Log(combo.ToString());
            plaka_mesafesi -= 2;
            slice_Play();

            //cylinder.transform.localScale += new Vector3(0, 2, 0);  //Her geçmede silindirin Y eksenine 2 birim uzaklık veriyorum.
            cylinder.transform.position += new Vector3(0, -2, 0);       //Her geçmede silindirin Y ekseninde ki pozisyonunu 2 birim aşşağıya veriyorum.


            Canvas_Manager.Instance.UpdateScore();

            //if (combo >= 2)
            //{
            //    parent.GetComponent<Disk_Move>().ChangeColor();
            //selectedDisc.GetComponent<Renderer>().material.color = Color.green;
            //Debug.Log("Combo sayısı = " + combo);
            //for (int i = 0; i < discs.Length; i++)
            //{
            //    //discs[i].GetComponent<Renderer>().material.color = Color.red;
            //    //other.gameObject.transform.parent.GetComponent<Renderer>().material.color = Color.red;
            //}
            ////other.gameObject.transform.parent.GetComponent<Renderer>().material.color = Color.yellow;
            //}
        }

    }
    //END LINE.

    [SerializeField] private GameObject[] discs; //Özdeş 10 plaka

    public bool IsDead { get => isDead; set => isDead = value; }

    void bangAndDestroy(GameObject diskParticleList, float y_state, float force)
    {
        diskParticleList.transform.parent = null;
        Rigidbody rb = diskParticleList.AddComponent<Rigidbody>();
        rb.gameObject.GetComponent<MeshCollider>().enabled = false;
        Vector3 dir = (diskParticleList.transform.position - objectManager.cylinder.transform.position).normalized;
        dir.y = y_state;
        rb.AddForce(dir * force);
        StartCoroutine(SetParent(diskParticleList));
    }
    void disc_Create(Collider other)
    {
        
    }
    void Restart()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator SetParent(GameObject diskParticleList)
    {
        yield return new WaitForSeconds(0.1f);
        diskParticleList.transform.parent = null;
        yield return new WaitForSeconds(3);
        Destroy(diskParticleList);

    }
    private void jump_Play()
    {
        Sound_Jump.Play();
    }
    private void slice_Play()
    {
        Sound_Space_Slice.Play();
    }
    private void game_over_Play()
    {
        Sound_Game_Over.Play();
    }
}