using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    [SerializeField] float spdOfSpin = 1F;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, spdOfSpin*Time.deltaTime);
    }
}
