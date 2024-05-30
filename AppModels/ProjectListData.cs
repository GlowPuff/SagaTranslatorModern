using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Imperial_Commander_Editor;

namespace Saga_Translator_V2
{
	public class ProjectListData : ObservableObject
	{
		public string version { get => Utils.appVersionString; }

		public ObservableCollection<ProjectListItem> projectListItems { get; set; }

		public ProjectListData()
		{
			projectListItems = new( new[] {
				new ProjectListItem() { Name = "User Interface", Description="ui.json", translationType= TranslationType.UI },
				new ProjectListItem() { Name = "Bonus Effects", Description="bonuseffects.json" , translationType= TranslationType.BonusEffects},
				new ProjectListItem() { Name = "Enemy Instructions", Description="instructions.json", translationType= TranslationType.EnemyInstructions },
				new ProjectListItem() { Name = "Campaign Items", Description="items.json", translationType= TranslationType.CampaignItems },
				new ProjectListItem() { Name = "Campaign Rewards", Description="rewards.json", translationType= TranslationType.CampaignRewards },
				new ProjectListItem() { Name = "Campaign Skills", Description="skills.json", translationType= TranslationType.CampaignSkills },
				new ProjectListItem() { Name = "Campaign Info", Description="BespinInfo.txt, LotalInfo.txt, etc", translationType= TranslationType.CampaignInfo },
				new ProjectListItem() { Name = "Deployment Card Text", Description="allies.json, enemies.json, etc", translationType= TranslationType.DeploymentCardText },
				new ProjectListItem() { Name = "Mission Card Text", Description="bespin.json, jabba.json, etc", translationType= TranslationType.MissionCardText },
				//new ProjectListItem() { Name = "Mission Info / Rules", Description="core1info.txt, hoth7rules.txt, etc", translationType= TranslationType.MissionInfoRules },
				new ProjectListItem() { Name = "Mission", Description="CORE1.json, HOTH4.json, etc", translationType= TranslationType.Mission },
				new ProjectListItem() { Name = "Tutorials", Description="TUTORIAL01.json, TUTORIAL02.json, etc", translationType= TranslationType.Tutorials },
				new ProjectListItem() { Name = "Help Overlays", Description="help.json", translationType= TranslationType.HelpOverlays },
			} );
		}
	}

	public class ProjectListItem
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public TranslationType translationType { get; set; }
	}
}
