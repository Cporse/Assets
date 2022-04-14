using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] private Transform target;
    void FixedUpdate()   //0.02 Saniye, saniyede 50 defa
    {
        //Top Fizik tabanl� d���� ya�ad��� i�in fizik tabanl� (FixedUpdate) �al��t�r�yoruz.
        transform.position = Vector3.Slerp(transform.position, (new Vector3(target.position.x, target.position.y, target.position.z) + new Vector3(0f, 1f, -4f)), 1f);
    }
}