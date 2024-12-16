

using UnityEngine;
using UnityEngine.AI;

public class PlayerRandomizePosition : MonoBehaviour
{
    [SerializeField] private Vector2 _bounds;

    private void Awake()
    {
        Vector3 pos;
        NavMeshHit hit;
        do
        {
            pos = new Vector2(
                Random.Range(-_bounds.x, _bounds.x),
                Random.Range(-_bounds.y, _bounds.y)
            );
        } while (!NavMesh.SamplePosition(pos, out hit, .3f, NavMesh.AllAreas));

        transform.position = new Vector3(
            hit.position.x,
            hit.position.y,
            transform.position.z);
        
        Debug.Log("pos rand");
        Camera.main.GetComponent<CameraFollow>().ForceLockTarget();
    }
}
