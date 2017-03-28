using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour
{
	
	public float viewAngle = 45f;
	public float increment = 1f;
	public float viewDistance = 10f;
	
	private MeshFilter mf;
	private MeshRenderer mr;

	void Start() {
		mf = GetComponent<MeshFilter>();
		mr = GetComponent<MeshRenderer>();
	}
	
	void Update() {

		/* Checks every update to see if the player is within it's Cone */
		bool playerSeen = false;
		
		int steps = (int) (viewAngle / increment);
		Vector3[] v = new Vector3[steps+1];
		Vector2[] u = new Vector2[steps+1];
		Vector3[] n = new Vector3[steps+1];
		int[] t = new int[3*(steps-1)];
		
		int index = 1;
		v[0] = transform.localPosition;
		u[0] = new Vector2(0,0);
		n[0] = new Vector3(0,0,-1);
		for (float a = -(viewAngle/2); a < viewAngle/2; a+=increment) {
			Vector3 dir = Quaternion.AngleAxis(a, transform.forward)*(-transform.up);
			
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
			Vector3 h = new Vector3(hit.point.x, hit.point.y, -1);
			Debug.DrawLine(transform.position, h, Color.white, 1, false);
			h = transform.worldToLocalMatrix.MultiplyPoint(h);

			Vector3 dist = h - v[0];
			dist.z = 0;
			if (dist.magnitude > viewDistance) {
				dist.Normalize();
				dist.Scale(new Vector3(viewDistance, viewDistance, 0));
				h = dist + v[0];
			} else if (hit.collider != null) {
				if (hit.collider.tag == "Player") playerSeen = true;
			}

			v[index] = h;
			u[index] = new Vector2(1,1);
			n[index] = new Vector3(0,0,-1);
			
			index++;
		}
		
		for (int i = 0; i < (t.Length/3); i++) {
			t[3*i + 0] = 0;
			t[3*i + 1] = i+1;
			t[3*i + 2] = i+2;
		}

		/* Set the mesh data */
		mf.mesh.vertices = v;
		mf.mesh.triangles = t;
		mf.mesh.normals = n;
		mf.mesh.uv = u;
		mf.mesh.RecalculateBounds();
		mf.mesh.Optimize();

		/* Colour the mesh */
		Color newCol = new Color();
		if (playerSeen) {
			newCol = Color.red;
			newCol.a = 0.25f;
			mr.material.color = newCol;
			EnemyScript ens = transform.parent.GetComponent<EnemyScript>();
			ens.state = 1;
		} else {
			newCol = Color.white;
			newCol.a = 0.25f;
			mr.material.color = newCol;
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
	}
}
