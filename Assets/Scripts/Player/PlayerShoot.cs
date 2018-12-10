using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_leftCannons;
    [SerializeField]
    private Transform[] m_rightCannons;
    [SerializeField]
    private GameObject m_regularProjectile = null;
    [SerializeField]
    private float m_shootForce = 5f;

    public void ShootLeft()
    {
        for (int t_cannonIndex = 0; t_cannonIndex < m_leftCannons.Length; t_cannonIndex++)
        {
            GameObject t_newProjectile = Instantiate(m_regularProjectile, m_leftCannons[t_cannonIndex].position, m_leftCannons[t_cannonIndex].rotation);
            t_newProjectile.GetComponent<Rigidbody>().AddForce(m_leftCannons[t_cannonIndex].forward * m_shootForce);
        }
    }

    public void ShootRight()
    {

    }
}
