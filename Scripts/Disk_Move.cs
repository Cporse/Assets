using UnityEngine;

public class Disk_Move : MonoBehaviour
{
    [SerializeField] private GameObject[] discs; //Özdeş 10 plaka
    [SerializeField] private GameObject particle_System_Splash;

    private Ball_Move ballMove;
    void Start()
    {
        ballMove = Ball_Move.Instance;

        int random = Random.Range(0, discs.Length);
        GameObject selectedDisc = discs[random];
        Color color = new Color(0.9098039f, 0.254902f, 0.09411765f);
        selectedDisc.GetComponent<Renderer>().material.color = color;
        selectedDisc.tag = "FailDisc";
    }
    void Update()
    {
        if (!ballMove.IsDead)
            OnMouseDrag();
    }
    void OnMouseDrag()
    {
        //particle_System_Splash.transform.Rotate(0, (-Input.GetAxis("Mouse X") * 1000f * Time.deltaTime), 0, Space.World);
        transform.Rotate(0, (-Input.GetAxis("Mouse X") * 250f * Time.deltaTime), 0, Space.World);
        //Mouseun hareketinde (X Ekseninde) plaka/diskleri X ekseninde döndürmek;
    }

    public void ChangeColor()
    {
        for (int i = 0; i < discs.Length; i++)
        {
            discs[i].GetComponent<Renderer>().material.color = Color.red;
        }
        
    }
}