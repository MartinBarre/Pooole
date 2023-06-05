using UnityEngine;

public class EndLifeDestroy : MonoBehaviour
{
    public float lifeTime;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
