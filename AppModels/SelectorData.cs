using CommunityToolkit.Mvvm.ComponentModel;

namespace Saga_Translator_V2
{
	public class SelectorData : ObservableObject
	{
		string _combo1Label, _combo2Label;
		ComboBoxMeta[] _comboList2;

		public string combo1Label { get => _combo1Label; set => SetProperty( ref _combo1Label, value ); }
		public string combo2Label { get => _combo2Label; set => SetProperty( ref _combo2Label, value ); }

		public ComboBoxMeta[] comboList1 { get; set; }
		public ComboBoxMeta[] comboList2 { get => _comboList2; set => SetProperty( ref _comboList2, value ); }

		public SelectorData( TranslationType ttype )
		{
			if ( ttype == TranslationType.CampaignInfo )
			{
				comboList1 = [
					new( "Core", "CampaignInfo.CoreInfo.txt" ),
					new( "Twin Shadows", "CampaignInfo.TwinInfo.txt" ),
					new( "Return to Hoth", "CampaignInfo.HothInfo.txt" ),
					new( "The Bespin Gambit", "CampaignInfo.BespinInfo.txt" ),
					new( "Jabba's Realm", "CampaignInfo.JabbaInfo.txt" ),
					new( "Heart of the Empire", "CampaignInfo.EmpireInfo.txt" ),
					new( "Tyrants of Lothal", "CampaignInfo.LothalInfo.txt" ),
				];
			}
			else if ( ttype == TranslationType.DeploymentCardText )
			{
				comboList1 = [
					new( "Allies", "DeploymentGroups.allies.json" ),
					new( "Enemies", "DeploymentGroups.enemies.json" ),
					new( "Heroes", "DeploymentGroups.heroes.json" ),
					new( "Villains", "DeploymentGroups.villains.json" ),
				];
			}
			else if ( ttype == TranslationType.MissionCardText )
			{
				comboList1 = [
					new( "Core", "MissionCardText.core.json" ),
					new( "Twin Shadows", "MissionCardText.twin.json" ),
					new( "Return to Hoth", "MissionCardText.hoth.json" ),
					new( "The Bespin Gambit", "MissionCardText.bespin.json" ),
					new( "Jabba's Realm", "MissionCardText.jabba.json" ),
					new( "Heart of the Empire", "MissionCardText.empire.json" ),
					new( "Tyrants of Lothal", "MissionCardText.lothal.json" ),
					new( "Other", "MissionCardText.other.json" )
				];
			}
			else if ( ttype == TranslationType.MissionInfoRules )
			{
				comboList1 = [
					new( "Core", "MissionText.core" ),
					new( "Twin Shadows", "MissionText.twin" ),
					new( "Return to Hoth", "MissionText.hoth" ),
					new( "The Bespin Gambit", "MissionText.bespin" ),
					new( "Jabba's Realm", "MissionText.jabba" ),
					new( "Heart of the Empire", "MissionText.empire" ),
					new( "Tyrants of Lothal", "MissionText.lothal" ),
					new( "Other", "MissionText.other" )
				];
			}
			else if ( ttype == TranslationType.Mission )
			{
				comboList1 = [
					new( "Core", "Core" ),
					new( "Twin Shadows", "Twin" ),
					new( "Return to Hoth", "Hoth" ),
					new( "The Bespin Gambit", "Bespin" ),
					new( "Jabba's Realm", "Jabba" ),
					new( "Heart of the Empire", "Empire" ),
					new( "Tyrants of Lothal", "Lothal" ),
					new( "Other", "Other" ),
					new( "Custom Mission", "" )//#8
				];
			}
			else if ( ttype == TranslationType.Tutorials )
			{
				comboList1 = [
					new( "Tutorial 1", "TUTORIAL01_EN.json" ),
					new( "Tutorial 2", "TUTORIAL02_EN.json" ),
					new( "Tutorial 3", "TUTORIAL03_EN.json" ),
				];
			}
		}
	}
}
