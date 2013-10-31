using System;
using UnityEngine;
using System.Collections;
using ZeroSignal.Engine;

namespace ZeroSignal.Engine.Audio { 

// Responsible for playing sounds.
	public class AudioManager : Singleton<AudioManager> {

	// Plays a specific sound clip at a specific position. 
	// NOTE: this only applies to 3d sounds.
		public static AudioSource Play( AudioClip clip, Transform position ) 	{
			return Play( clip, position.position, 1f, 1f );
		}

	// Actually PLAY the sound clip.
		public static AudioSource Play(AudioClip clip, Vector3 point, float
			volume, float pitch) 	{
		// Create the game object to store the sound.
			GameObject go = new GameObject(String.Format(Registry.Prefixes.AudioSoundPrefix, clip.name));
			go.transform.position = point;

		// Set up the file.
			AudioSource source = go.AddComponent<AudioSource>();
			source.clip = clip;
			source.volume = volume;
			source.pitch = pitch;
			source.Play();
			Destroy(go, clip.length);

			return source;
		}

	// Attaches a given audioclip to a game object.
		public static AudioSource Attach(AudioClip clip, GameObject obj, float
			volume, float pitch) {
			AudioSource source = obj.AddComponent<AudioSource>();
			source.clip = clip;
			source.volume = volume;
			source.pitch = pitch;
			source.Play();

			return source;
		}
	}

}