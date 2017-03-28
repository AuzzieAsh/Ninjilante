using UnityEngine;
using System.Collections;

/**
 * Uses raycasting to create a mesh which represents the enemy's
 * cone of vision. View distance and cone angle can be set in
 * the Inspector.
 * Will only work with nice round viewAngles and increments.
 */
public class EnemyVision : MonoBehaviour
{
	
	public float viewAngle = 45f;
	public float increment = 1f;
	public float viewDistance = 10f;
	public LayerMask layerMask;
	public Material coneMaterial;
	
	private MeshFilter mf;
	private MeshRenderer mr;
	private EnemyScript ens;

	void Start() {
		mf = GetComponent<MeshFilter>();
		mr = GetComponent<MeshRenderer>();
		mr.material = coneMaterial;
		ens = GetComponentInParent<EnemyScript>();
	}
	
	void Update() {
		mr.material = coneMaterial;
		if (ens.state < 2) {
			/* Checks every update to see if the player is within it's Cone */
			bool playerSeen = false;
			bool bloodSeen = false;
			bool corpseSeen = false;

			Vector3 bloodPos = new Vector3();
			
			int steps = (int) (viewAngle / increment);
			Vector3[] v = new Vector3[steps+1];
			Vector2[] u = new Vector2[steps+1];
			Vector3[] n = new Vector3[steps+1];
			int[] t = new int[3*(steps-1)];
			
			int index = 1;
			Vector3 startPos = transform.localPosition;
			startPos.z = transform.position.z;
			v[0] = startPos;
			u[0] = new Vector2(0,0);
			n[0] = new Vector3(0,0,-1);

			// Iterate through the angles casting rays
			for (float a = -(viewAngle/2); a < viewAngle/2; a+=increment) {
				Vector3 dir = Quaternion.AngleAxis(a, transform.forward)*(-transform.up);
				dir.z = startPos.z;
				RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
				Vector3 h = new Vector3(hit.point.x, hit.point.y, startPos.z);
				h = transform.worldToLocalMatrix.MultiplyPoint(h);
				h.z = startPos.z;
				Vector3 dist = h - v[0];
				dist.z = startPos.z;

				// If the ray lands too far away, set the Vector to match the viewDistance
				if (dist.magnitude > viewDistance) {
					dist.Normalize();
					dist.Scale(new Vector3(viewDistance, viewDistance, 0));
					h = dist + v[0];
				} else if (hit.collider != null) {
					if (hit.collider.tag == "Player") playerSeen = true; // Sees player
					else if (hit.collider.tag == "Blood") { // Sees blood
						/* Project the ray through the blood, and make the guard investigate */
						dist.Normalize();
						dist.Scale(new Vector3(viewDistance, viewDistance, 0));
						h = dist + v[0];
						bloodSeen = true;
						bloodPos = hit.point;
						hit.collider.gameObject.layer = 2; // Ignore the blood from now on (2 = IgnoreRayCast layer)
					} else if (hit.collider.tag == "Corpse") {
						corpseSeen = true; // Sees corpse
					}
				}
	
				v[index] = h;
				u[index] = new Vector2(1,1);
				n[index] = new Vector3(0,0,-1);
				
				index++;
			}

			/* Create the triangle data for the mesh */
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
	
			/* Change the state of the enemy based on whatever it saw with the rays */
			if (playerSeen) {
				ens.state = 2;
			} else if (corpseSeen && ens.state < 2) {
				ens.state = 2;
			} else if (bloodSeen && ens.state == 0) {
				/* Set the source waypoint of the investigation */
				GameObject newWp = new GameObject();
				newWp.transform.position = bloodPos;
				ens.GetComponent<InvestigateAI>().sourceWaypoint = newWp;
				ens.state = 1;
			}
		} else {
			// Reset the mesh
			mf.mesh.Clear();
		}
	}
	
}
