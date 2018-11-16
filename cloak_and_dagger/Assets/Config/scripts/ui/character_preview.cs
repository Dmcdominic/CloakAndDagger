using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_preview : MonoBehaviour {
	
	public player_int characters_chosen;
	public event_object update_preview_trigger;

	public Animator animator;
	public anim_piece anim_Piece;

	private static int palettes_per_char_type = 2;
	public int fixed_palette_range_index = -1;

	//private string my_name;
	private int current_palette;


	private void Awake() {
		// TODO - set my_name here, or read from a SO instead
		//my_name = "PLACEHOLDER";
		if (update_preview_trigger) {
			update_preview_trigger.e.AddListener(update_preview);
		}
		update_preview();
	}
	
	private void update_preview() {
		// TODO - characters_Chosen needs to be initialized ahead of time, then uncomment this:
		//int new_palette = characters_Chosen.char_by_player_name[my_name];
		int new_palette = 1;
		bool in_valid_range = check_palette_range(new_palette);
		if (!in_valid_range || new_palette == current_palette) {
			return;
		}
		current_palette = new_palette;
		anim_Piece.palette_index = new_palette;
		animator.SetTrigger("update_preview");
	}

	private bool check_palette_range(int new_palette) {
		if (fixed_palette_range_index == -1) {
			return true;
		}
		return fixed_palette_range_index <= new_palette &&
			new_palette < (fixed_palette_range_index + palettes_per_char_type);
	}
}
