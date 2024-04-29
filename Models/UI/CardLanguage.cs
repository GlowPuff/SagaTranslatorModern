using Imperial_Commander_Editor;

namespace Saga_Translator_V2
{
	public class CardLanguage
	{
		public string id { get; set; }
		public string name { get; set; }
		public string subname { get; set; }
		public string ignored { get; set; }
		public string[] traits { get; set; }
		public string[] surges { get; set; }
		public string[] keywords { get; set; }
		public GroupAbility[] abilities { get; set; }
	}
}
