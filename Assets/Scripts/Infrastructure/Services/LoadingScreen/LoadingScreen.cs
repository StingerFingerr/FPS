using System;
using UnityEngine;
using UnityEngine.UI;

namespace LoadingScreen
{
    public class LoadingScreen: MonoBehaviour, ILoadingScreen
    {
        public Slider progressSlider;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);

        public void UpdateLoadingProgress(float progress) => 
            progressSlider.value = progress;
    }
}