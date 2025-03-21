using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;

    private Vector3 _velocity;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref _velocity, timeOffset);
    }

    public void GhostInteraction()
    {
        transform.parent.gameObject.GetComponent<PlayerInteraction>().InteractGhost();
    }
}
