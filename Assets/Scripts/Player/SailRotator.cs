using UnityEngine;

public class SailRotator : MonoBehaviour
{
    [SerializeField]
    private Transform m_hull = null;
    [SerializeField]
    private WindManager m_windManager = null;
    [SerializeField]
    private float m_maxRotation = 75f;

    private void Update ()
    {
        float t_sailToWindAngle = Vector2.Angle(m_windManager.Wind.Direction, new Vector2(transform.position.x, transform.position.z));
        if (t_sailToWindAngle > Mathf.Epsilon)
        {
            float t_hullToWindAngle = Vector2.SignedAngle(m_windManager.Wind.Direction, new Vector2(m_hull.forward.x, m_hull.forward.z));
            float t_newRotation = -m_hull.eulerAngles.y;
            if (Mathf.Abs(t_hullToWindAngle) > m_maxRotation)
            {
                if (t_hullToWindAngle < 0)
                {
                    t_newRotation = -m_maxRotation;
                }
                else
                {
                    t_newRotation = m_maxRotation;
                }
            }
            transform.localEulerAngles = new Vector3(0, t_newRotation, 0);
        }
	}
}
