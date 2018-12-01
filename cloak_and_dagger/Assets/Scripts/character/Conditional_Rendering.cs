using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

[RequireComponent(typeof(network_id))]
public class Conditional_Rendering : MonoBehaviour {

    [SerializeField]
    int_var local_id;

	public Material local_mat;

	public Material non_local_mat;

	//[SerializeField]
	//float_event_object reveal_trigger;

	[SerializeField]
	bool_var spectator_reveal;

    [SerializeField]
    player_bool is_king;

    [SerializeField]
    player_bool bump_reveal;
    

	private IEnumerable srs;
	private network_id network_Id;

    LineRenderer king_circle;

	// Use this for initialization
	void Start () {
		//srs = GetComponentsInChildren<SpriteRenderer>().Where((sr) => !sr.CompareTag("Payload"));
		srs = GetComponentsInChildren<SpriteRenderer>();
		network_Id = GetComponent<network_id>();
		update_material();
        king_circle = GetComponent<LineRenderer>();
        if(king_circle) king_circle.enabled = false;
	}

	private void Update() {
		update_material();
	}

	private void update_material() {
		if (srs == null) {
			//lol
			//srs = GetComponentsInChildren<SpriteRenderer>().Where((sr) => !sr.CompareTag("Payload"));
			srs = GetComponentsInChildren<SpriteRenderer>();
		}
        int id = network_Id.val;
        if (is_king[id])
        {
            if(king_circle) king_circle.enabled = true;
        }
        else
        {
            if(king_circle) king_circle.enabled = false;
        }
        if (id == local_id.val || is_king[id] || bump_reveal[id] || spectator_reveal.val) {
			foreach (SpriteRenderer sr in srs) {
				sr.material = local_mat;
			}
		} else {
			foreach (SpriteRenderer sr in srs) {
				sr.material = non_local_mat;
			}
		}
	}

}
