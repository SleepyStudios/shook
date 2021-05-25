using UnityEngine;

/// <summary>
/// Drag a Rigidbody2D by selecting one of its colliders by pressing the mouse down.
/// When the collider is selected, add a TargetJoint2D.
/// While the mouse is moving, continually set the target to the mouse position.
/// When the mouse is released, the TargetJoint2D is deleted.`
/// </summary>
/// https://github.com/Unity-Technologies/PhysicsExamples2D
public class DragTarget : MonoBehaviour {
	[Range(0.0f, 100.0f)]
	public float m_Damping = 1.0f;

	[Range(0.0f, 100.0f)]
	public float m_Frequency = 5.0f;

	public bool m_DrawDragLine = true;
	public Color m_Color = Color.cyan;

	private TargetJoint2D m_TargetJoint;

    void Start() {
		var body = GetComponent<Rigidbody2D>();

		// Add a target joint to the Rigidbody2D GameObject.
		m_TargetJoint = body.gameObject.AddComponent<TargetJoint2D>();
		m_TargetJoint.dampingRatio = m_Damping;
		m_TargetJoint.frequency = m_Frequency;

		m_TargetJoint.anchor = m_TargetJoint.transform.InverseTransformPoint(transform.position);
	}

	void Update() {
		// Calculate the world position for the mouse.
		var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		// Update the joint target.
		if (m_TargetJoint) {
			m_TargetJoint.target = new Vector2(worldPos.x, Camera.main.ScreenToWorldPoint(Vector3.zero).y);
			m_TargetJoint.dampingRatio = m_Damping;
			m_TargetJoint.frequency = m_Frequency;

			// Draw the line between the target and the joint anchor.
			if (m_DrawDragLine)
				Debug.DrawLine(m_TargetJoint.transform.TransformPoint(m_TargetJoint.anchor), worldPos, m_Color);
		}
	}
}
