using UnityEngine;

public class HazardKillPlayer : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player_Movement>() == null) return;

        GameManager.Instance.ResetBothPlayersFromHazard(respawnDelay);
    }
}
