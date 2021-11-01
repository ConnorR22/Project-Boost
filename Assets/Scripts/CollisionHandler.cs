using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Okay Collision");
                break;
            case "Finish":
                Debug.Log("Yay you are done");
                break;
            case "Fuel":
                Debug.Log("You attained more fuel");
                break;
            default:
                Debug.Log("Oh god do not hit that");
                break;
        }
    }
}
