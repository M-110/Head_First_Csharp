using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Rigidbody cueBall;
    [SerializeField] float shotStrength;
    
    
    public void LaunchBall()
    {
        cueBall.AddForce(Vector3.right * shotStrength);
    }
}
