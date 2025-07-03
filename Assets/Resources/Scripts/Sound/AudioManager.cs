using System.Collections.Generic;
    using UnityEngine;
    
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public Sound[] musicSound, sfxSound;
        public AudioSource masterSoucre, musicSource, sfxSource;
    
        private Dictionary<string, Sound> musicDictionary;
        private Dictionary<string, Sound> sfxDictionary;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
    
                // Khởi tạo từ điển để tìm kiếm âm thanh nhanh hơn
                InitializeSoundDictionaries();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        private void InitializeSoundDictionaries()
        {
            musicDictionary = new Dictionary<string, Sound>();
            sfxDictionary = new Dictionary<string, Sound>();
    
            foreach (var sound in musicSound)
            {
                if (sound != null && !string.IsNullOrEmpty(sound.name))
                {
                    musicDictionary[sound.name] = sound;
                }
            }
    
            foreach (var sound in sfxSound)
            {
                if (sound != null && !string.IsNullOrEmpty(sound.name))
                {
                    sfxDictionary[sound.name] = sound;
                }
            }
        }
    
        public void PlayMusic(string name)
        {
            StopMusic();
    
            if (musicDictionary.TryGetValue(name, out Sound s))
            {
                musicSource.clip = s.clip;
                musicSource.Play();
            }
            else
            {
                Debug.Log("Không tìm thấy nhạc: " + name);
            }
        }
    
        public void PlaySFX(string name)
        {
            // Loại bỏ StopMusic()
            
            if (sfxDictionary.TryGetValue(name, out Sound s))
            {
                // Chỉ sử dụng PlayOneShot
                sfxSource.PlayOneShot(s.clip);
            }
            else
            {
                Debug.Log("Không tìm thấy hiệu ứng âm thanh: " + name);
            }
        }
    
        public void StopMusic()
        {
            musicSource.Stop();
        }
    
        public void StopAllAudio()
        {
            sfxSource.Stop();
            musicSource.Stop();
            masterSoucre.Stop();
        }
    
        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }
    
        public void ToggleSFX()
        {
            sfxSource.mute = !sfxSource.mute;
        }
    
        public void MusicVolume(float volume)
        {
            musicSource.volume = volume;
        }
    
        public void SFXVolume(float volume)
        {
            sfxSource.volume = volume;
        }
    }