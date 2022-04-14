using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;

    public GameObject cylinder;

    private void Awake()    //
    {
        if (Instance == null)
            Instance = this;
    }
}
