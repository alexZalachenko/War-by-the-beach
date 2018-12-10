using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private RudderController m_rudderController = null;
    [SerializeField]
    private WindManager m_windManager = null;
    [SerializeField]
    private Transform m_sail = null;
    [SerializeField]
    private float m_speed = 1f;
    [SerializeField]
    private float m_minSailAngleToUseRows = 0.25f;
    [SerializeField]
    private float m_rowSpeed = 3f;
    [SerializeField]
    // How much the steering force reduces it's power in relation to sail-wind angle
    private float m_steeringForceReduction = 2f;
    
    private void Update()
    {
        // Calculate how wind impulses the ship
        float t_sailWindAngle = Vector2.Dot(m_windManager.Wind.Direction, new Vector2(m_sail.forward.x, m_sail.forward.z));
        float t_computedWindStrength = m_windManager.Wind.Strength * t_sailWindAngle * m_speed;
        if (t_sailWindAngle < m_minSailAngleToUseRows)
        {
            //t_computedWindStrength += m_rowSpeed; With this, the effect of the sail still applies and when sailing counter-wind will slows you
            t_computedWindStrength = m_rowSpeed;
        }
        Vector3 t_newPosition = transform.position + transform.forward * t_computedWindStrength * Time.deltaTime;

        // Calculate how the ship rotates due to the rudder
        Vector3 t_rudderDirection = Quaternion.Euler(0, m_rudderController.GetRudderRotation(), 0) * transform.forward;
        Vector3 t_steering = t_rudderDirection - transform.forward;
        t_steering *= Mathf.Pow(t_computedWindStrength, 2) * m_steeringForceReduction * Time.deltaTime;
        t_newPosition += t_steering;

        //Debug.Log("Speed: " + (t_newPosition - transform.position).magnitude * 100);
        //Debug.Log("WindStrength: " + t_computedWindStrength);
        //Debug.Log("SailAngle: " + t_sailWindAngle);

        // Set the new position and rotation
        transform.rotation = Quaternion.LookRotation(t_newPosition - transform.position);
        transform.position = t_newPosition;
    }
}
