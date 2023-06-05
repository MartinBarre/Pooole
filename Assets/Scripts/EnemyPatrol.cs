using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    public SpriteRenderer graphics;
    public AudioSource source;
    public bool autoFlip;
    
    private Transform _target;
    private int _destPoint;

    private void Start()
    {
        _target = waypoints[0];
    }

    private void Update()
    {
        var dir = _target.position - transform.position;
        dir = speed * Time.deltaTime * dir.normalized;
        
        if (Vector3.Distance(transform.position, _target.position) > dir.magnitude)
        {
            transform.Translate(dir);
        }
        else
        {
            var newDir = transform.position - _target.position;
            newDir = (speed - dir.magnitude) * Time.deltaTime * newDir.normalized;
            transform.position = _target.position;
            transform.Translate(newDir);
            
            _destPoint = (_destPoint + 1) % waypoints.Length;
            _target = waypoints[_destPoint];
            
            if (autoFlip)
            {
                graphics.flipX = !graphics.flipX;
            }

            if (source)
            {
                source.Play();
            }
        }
    }
}