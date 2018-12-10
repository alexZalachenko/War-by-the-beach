using System.Collections;
using UnityEngine;

public class RudderController : MonoBehaviour
{
    [SerializeField]
    // Percentage of the screen that needs to be dragged to rotate the rudder at its maximum
    private float m_dragScreenPercentage = 0.5f;
    [SerializeField]
    // How much force the rudder applies to the rotation force when rotated at its maximum
    private float m_maxRudderForce = 50f;
    [SerializeField]
    private float m_automaticDecreasePercentage = 0.1f;
    private float m_automaticDecreasePixels;
    [SerializeField]
    private RudderView m_rudderView = null;
    private float m_maxDragPixels;
    private float m_rudderInput = 0f; // Range [-1, 1]
    private float m_lastDragPosition;
    private float m_totalDrag = 0; // In pixels
    private int m_dragTouchID;
    private Coroutine m_reduceRudder = null;

    private void Start()
    {
        m_maxDragPixels = Screen.width * m_dragScreenPercentage;
        m_automaticDecreasePixels = Screen.width * m_automaticDecreasePercentage;
    }
    
    public float GetRudderRotation()
    {
        return m_maxRudderForce * m_rudderInput;
    }

#if UNITY_STANDALONE
    private void Update()
    {
        m_rudderInput = Input.GetAxis("Horizontal");
    }
#endif

    public void OnRudderBeginDrag()
    {
        if (m_reduceRudder != null)
        {
            StopAllCoroutines();
            m_reduceRudder = null;
        }
        Touch t_touch = Input.touches[Input.touchCount - 1];
        m_lastDragPosition = t_touch.position.x;
        m_dragTouchID = t_touch.fingerId;
    }

    public void OnRudderDrag()
    {
        Touch t_dragTouch = Input.GetTouch(m_dragTouchID);

        // In pixels
        float t_thisFrameDrag = t_dragTouch.position.x - m_lastDragPosition;
        m_totalDrag += t_thisFrameDrag;

        float t_totalDragCopy = m_totalDrag;
        m_totalDrag = Mathf.Clamp(m_totalDrag, -m_maxDragPixels, m_maxDragPixels);
        // If over turning the rudder, the UI rudder shouldn't rotate
        if (!Mathf.Approximately(t_totalDragCopy, m_totalDrag))
        {
            t_thisFrameDrag = 0;
        }

        OnRudderRotationChange(t_thisFrameDrag / m_maxDragPixels, m_totalDrag / m_maxDragPixels);
        m_lastDragPosition = t_dragTouch.position.x;
    }

    public void OnRudderEnd()
    {
        m_reduceRudder = StartCoroutine("ReduceRudder");
    }

    private IEnumerator ReduceRudder()
    {
        float t_decrease = m_automaticDecreasePixels * Mathf.Sign(m_totalDrag);

        while (m_totalDrag != 0)
        {
            float t_oldTotalDrag = m_totalDrag;
            m_totalDrag -= t_decrease * Time.deltaTime;


            if (t_oldTotalDrag < 0 )
            {
                if (m_totalDrag > 0)
                {
                    m_totalDrag = 0;
                }
            }
            else
            {
                if (m_totalDrag < 0)
                {
                    m_totalDrag = 0;
                }
            }

            OnRudderRotationChange(Mathf.Abs(t_oldTotalDrag - m_totalDrag) / m_maxDragPixels, m_totalDrag / m_maxDragPixels);
            yield return null;
        }
        m_reduceRudder = null;
        yield return null;
    }

    // Normalized values [-1, 1]
    private void OnRudderRotationChange(float p_thisFrameDrag, float p_totalDrag)
    {
        m_rudderInput = p_totalDrag;
        m_rudderView.OnRudderDrag(p_thisFrameDrag, p_totalDrag);
    }
}
