using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Game.Game_UI
{
    public class UIGrenades: MonoBehaviour
    {
        public TextMeshProUGUI grenadesText;

        private void Start()
        {
            StartCoroutine(Test());
        }

        private IEnumerator Test()
        {
            int grenades = 0;
            while (true)
            {
                yield return new WaitForSeconds(2);
                grenades++;
                UpdateGrenades(grenades);
            }
        }

        public void UpdateGrenades(int grenades)
        {
            grenadesText.text = $"{grenades}";
            grenadesText.transform
                .DOPunchScale(Vector3.one * .5f, .3f, 0, 1);
        }

    }
}