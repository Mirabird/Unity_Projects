using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotate;

    private IEnumerator Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        while (true)
        {
            rb.transform.Rotate(_rotate * Time.deltaTime);
            yield return null;
        }
    }
}