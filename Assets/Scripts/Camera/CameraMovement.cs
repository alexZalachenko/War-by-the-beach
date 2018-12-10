using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform m_playerShip = null;
    [SerializeField]
    private Transform m_enemyShip = null;
    private float m_horizontalFOVTan = 0f;
    private float m_verticalFOVTan = 0f;
    private Camera m_camera = null;
    [SerializeField]
    private float m_cameraOffset = 3.5f;

    private void Start()
    {
        m_camera = Camera.main;
        float t_horizontalFOV = 2 * Mathf.Atan(Mathf.Tan(m_camera.fieldOfView * Mathf.Deg2Rad / 2f) * m_camera.aspect);
        m_horizontalFOVTan = Mathf.Tan(t_horizontalFOV / 2f);
        m_verticalFOVTan = Mathf.Tan(m_camera.fieldOfView / 2 * Mathf.Deg2Rad);
    }

    private void Update()
    {
        //Vector3 t_cameraPosition = transform.position;

        //// Set the camera position to be just in the middle of the two ships
        //Vector2 t_playerToEnemy = new Vector2(m_enemyShip.position.x + m_playerShip.position.x, m_enemyShip.position.z + m_playerShip.position.z);
        //Vector2 t_midPoint = t_playerToEnemy / 2f;
        //t_cameraPosition.x = t_midPoint.x;
        //t_cameraPosition.z = t_midPoint.y;

        //// Set the height of the camera to keep both ships on the screen
        //float t_horizontalHeight = (Mathf.Abs(t_playerToEnemy.x) / 2f) / m_horizontalFOVTan;
        //float t_verticalHeight = (Mathf.Abs(t_playerToEnemy.y) / 2f) / m_verticalFOVTan;
        //t_cameraPosition.y = t_horizontalHeight > t_verticalHeight ? t_horizontalHeight : t_verticalHeight;
        //t_cameraPosition.y += m_cameraOffset;

        //transform.position = t_cameraPosition;



        // Set the camera position to be just in the middle of the two ships
        Vector2 t_playerToEnemy = new Vector2(m_enemyShip.position.x - m_playerShip.position.x, m_enemyShip.position.z - m_playerShip.position.z);
        Vector2 t_midPoint = t_playerToEnemy * 0.5f;
        Vector3 t_cameraPosition = new Vector3(m_playerShip.position.x + t_midPoint.x, 0, m_playerShip.position.z + t_midPoint.y);

        // Set the height of the camera to keep both ships on the screen
        float t_horizontalHeight = (Mathf.Abs(t_playerToEnemy.x) / 2f) / m_horizontalFOVTan;
        float t_verticalHeight = (Mathf.Abs(t_playerToEnemy.y) / 2f) / m_verticalFOVTan;
        t_cameraPosition.y = t_horizontalHeight > t_verticalHeight ? t_horizontalHeight : t_verticalHeight;
        t_cameraPosition.y += m_cameraOffset;

        transform.position = t_cameraPosition;
    }
}
