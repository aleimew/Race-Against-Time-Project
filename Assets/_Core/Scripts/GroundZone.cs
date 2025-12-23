using UnityEngine;

public class GroundZone : MonoBehaviour
{
    public enum ZoneType
    {
        Speed,
        Slow
    }
    public ZoneType zoneType = ZoneType.Speed;
    public float speedMultiplier = 1.5f; // 1.5 = 50% faster
    public float slowMultiplier = 0.5f;  // 0.5 = 50% slower

  
    private void OnTriggerEnter2D(Collider2D other)
    {
          Player_Movement player = other.GetComponent<Player_Movement>();
        if (player == null)
            return;

        if (zoneType == ZoneType.Speed)
        {
            player.ChangeSpeed(speedMultiplier);
        }
        
        else if (zoneType == ZoneType.Slow)
        {
            player.ChangeSpeed(slowMultiplier);
        }
    }

  
    private void OnTriggerExit2D(Collider2D other)
    {
        Player_Movement player = other.GetComponent<Player_Movement>();

        if (player == null)
            return;

        // Reset ONLY the player who left the zone
        player.ResetSpeed();
    }
}
