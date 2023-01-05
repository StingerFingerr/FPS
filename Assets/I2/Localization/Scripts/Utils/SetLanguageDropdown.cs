using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace I2.Loc
{
	[AddComponentMenu("I2/Localization/SetLanguage Dropdown")]
	public class SetLanguageDropdown : MonoBehaviour 
	{
        #if UNITY_5_2 || UNITY_5_3 || UNITY_5_4_OR_NEWER

		public List<Sprite> flagsSprites;
		
        void OnEnable()
		{
			var dropdown = GetComponent<TMP_Dropdown>();
			if (dropdown==null)
				return;

			var currentLanguage = LocalizationManager.CurrentLanguage;
			if (LocalizationManager.Sources.Count==0) LocalizationManager.UpdateSources();
			var languages = LocalizationManager.GetAllLanguages();

			var options = new List<TMP_Dropdown.OptionData>(languages.Count);
			foreach (var language in languages)
			{
				var option = new TMP_Dropdown.OptionData()
				{
					image = flagsSprites.FirstOrDefault(s =>
						s.name.Equals(LocalizationManager.GetLanguageCode(language))),
					text = language
				};
				options.Add(option);
			}
			
			
			// Fill the dropdown elements
			dropdown.ClearOptions();
			dropdown.AddOptions( options );

			dropdown.value = languages.IndexOf( currentLanguage );
			dropdown.onValueChanged.RemoveListener( OnValueChanged );
			dropdown.onValueChanged.AddListener( OnValueChanged );
		}

		
		void OnValueChanged( int index )
		{
			var dropdown = GetComponent<TMP_Dropdown>();
			if (index<0)
			{
				index = 0;
				dropdown.value = index;
			}

			LocalizationManager.CurrentLanguage = dropdown.options[index].text;
        }
        #endif
    }
}