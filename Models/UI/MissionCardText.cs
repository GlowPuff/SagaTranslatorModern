using Imperial_Commander_Editor;
using Newtonsoft.Json;

namespace Saga_Translator_V2
{
	public class MissionCardText
	{
		public string id { get; set; }
		public string name { get; set; }
		public string descriptionText { get; set; }
		public string bonusText { get; set; }
		public string heroText { get; set; }
		public string allyText { get; set; }
		public string villainText { get; set; }
		public string[] tagsText { get; set; }
		public string expansionText { get; set; }
		public string rebelRewardText { get; set; }
		public string imperialRewardText { get; set; }

		//ignored properties
		[JsonIgnore]
		public string hero { get; set; }
		[JsonIgnore]
		public MissionType[] missionType { get; set; }
		[JsonIgnore]
		public string[] ally { get; set; }
		[JsonIgnore]
		public string[] villain { get; set; }
		[JsonIgnore]
		public string[] tags { get; set; }
		[JsonIgnore]
		public int page { get; set; }
		[JsonIgnore]
		public int influenceCost { get; set; }
		[JsonIgnore]
		public int[] timePeriod { get; set; }
		[JsonIgnore]
		public Expansion expansion { get; set; }
	}
}
