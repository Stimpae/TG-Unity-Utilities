﻿using UnityEngine;

namespace TG.Utilities {
    /// <summary>
    /// A persistent singleton class for Unity components.
    /// Ensures that only one instance of the component exists in the scene and persists across scene loads.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component {
        protected static T instance;

        public static bool HasInstance => instance != null;
        public static T TryGetInstance() => instance;

        public static T Instance {
            get {
                if (instance == null) {
                    instance = FindAnyObjectByType<T>() ?? CreateNewInstance();
                }
                return instance;
            }
        }

        private static T CreateNewInstance() {
            var go = new GameObject(typeof(T).Name + " Auto-Generated");
            return go.AddComponent<T>();
        }

        protected virtual void Awake() {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton() {
            if (!Application.isPlaying) return;
            if (instance == null) {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            } else if (instance != this) {
                Destroy(gameObject);
            }
        }
    }
}