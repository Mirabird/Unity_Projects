using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private Vector3 _start;

    [SerializeField]
    private Vector3 _end;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _delay;

    private IEnumerator Start()
    {
        Vector3 currentTarget = _end;
        bool movingToEnd = true;

        while (true)
        {
            Vector3 target = movingToEnd ? _end : _start;
            yield return MoveToTarget(target);
            movingToEnd = !movingToEnd;
        }
    }

    private IEnumerator MoveToTarget(Vector3 target)
    {
        while (transform.position != target)
        {
            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            yield return null;
        }

        yield return new WaitForSeconds(_delay);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_start, 0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_end, 0.5f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(_start, _end);
    }
}