using UnityEngine;

namespace I2.Loc
{
	public static class ScriptLocalization
	{

		public static string MyText 		{ get{ return LocalizationManager.GetTranslation ("MyText"); } }
		public static string Term2 		{ get{ return LocalizationManager.GetTranslation ("Term2"); } }
	}

    public static class ScriptTerms
	{

		public const string MyText = "MyText";
		public const string Term2 = "Term2";
	}
}