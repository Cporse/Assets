using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] private Transform target;
    void FixedUpdate()   //0.02 Saniye, saniyede 50 defa
    {
        //Top Fizik tabanlý düþüþ yaþadýðý için fizik tabanlý (FixedUpdate) çalýþtýrýyoruz.
        transform.position = Vector3.Slerp(transform.position, (new Vector3(target.position.x, target.position.y, target.position.z) + new Vector3(0f, 1f, -4f)), 1f);
    }
}