using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_palette_controller : MonoBehaviour {

	public anim_palettes_bundle anim_Palettes_Bundle;

	public int set_animation {
		set { animation_index = value; update_sprite(); }
	}
	public int set_palette {
		set { palette_index = value; update_sprite(); }
	}
	public int current_sprite {
		set { sprite_index = value; update_sprite(); }
	}

	private int animation_index;
	private int palette_index;
	private int sprite_index;
	private SpriteRenderer sprite_renderer;


	private void Awake() {
		sprite_renderer = GetComponent<SpriteRenderer>();
	}

	private void update_sprite() {
		sprite_renderer.sprite = anim_Palettes_Bundle.sets[animation_index].palettes[palette_index].sprites[sprite_index];
	}

}
