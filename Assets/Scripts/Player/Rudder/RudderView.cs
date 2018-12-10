using UnityEngine;

public class RudderView : MonoBehaviour
{
    [SerializeField]
    private float m_rudderRotationRate = 0.1f;
    [SerializeField]
    private RectTransform m_rudder = null;
    [SerializeField]
    private RectTransform m_leftFill = null;
    [SerializeField]
    private RectTransform m_rightFill = null;

    // Values are normalized [-1, 1]
    public void OnRudderDrag(float p_thisFrameDrag, float p_totalDrag)
    {
        m_rudder.eulerAngles += new Vector3(0, 0, -p_thisFrameDrag * m_rudderRotationRate);
        if (p_totalDrag > 0)
        {
            m_rightFill.localScale = new Vector3(p_totalDrag, 1, 1);
        }
        else
        {
            m_leftFill.localScale = new Vector3(-p_totalDrag, 1, 1);
        }
    }
}
