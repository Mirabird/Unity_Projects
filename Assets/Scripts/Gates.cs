using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField]
    private int _score = 0;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        _score++;
        Debug.Log("Current score: " + _score);
    }
}