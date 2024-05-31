using CommunityToolkit.Mvvm.ComponentModel;

namespace Saga_Translator_V2
{
	public class UILanguage
	{
		public string languageID { get; set; }
		public UITitle uiTitle { get; set; }
		public UISetup uiSetup { get; set; }
		public SagaUISetup sagaUISetup { get; set; }
		public UIMainApp uiMainApp { get; set; }
		public UISettings uiSettings { get; set; }
		public UIExpansions uiExpansions { get; set; }
		public SagaMainApp sagaMainApp { get; set; }
		public UICampaign uiCampaign { get; set; }
		public UILogger uiLogger { get; set; }
	}

	public class UISettings : ObservableObject
	{
		private string _settingsHeading, _music, _sound, _bloom, _vignette, _quit, _returnBtn, _ok, _quickClose, _ambient, _zoomButtons, _topdownView, _roundLimitOn, _roundLimitOff, _roundLimitDangerous, _roundLimitLabel, _skipWarpIntroLabel;

		public string settingsHeading
		{
			get => _settingsHeading; set => SetProperty( ref _settingsHeading, value );
		}
		public string music
		{
			get => _music; set => SetProperty( ref _music, value );
		}
		public string sound
		{
			get => _sound; set => SetProperty( ref _sound, value );
		}
		public string bloom
		{
			get => _bloom; set => SetProperty( ref _bloom, value );
		}
		public string vignette
		{
			get => _vignette; set => SetProperty( ref _vignette, value );
		}
		public string quit
		{
			get => _quit; set => SetProperty( ref _quit, value );
		}
		public string returnBtn
		{
			get => _returnBtn; set => SetProperty( ref _returnBtn, value );
		}
		public string ok
		{
			get => _ok; set => SetProperty( ref _ok, value );
		}
		public string quickClose
		{
			get => _quickClose; set => SetProperty( ref _quickClose, value );
		}
		public string ambient
		{
			get => _ambient; set => SetProperty( ref _ambient, value );
		}
		public string zoomButtons
		{
			get => _zoomButtons; set => SetProperty( ref _zoomButtons, value );
		}
		public string topdownView
		{
			get => _topdownView; set => SetProperty( ref _topdownView, value );
		}
		public string roundLimitOn
		{
			get => _roundLimitOn; set => SetProperty( ref _roundLimitOn, value );
		}
		public string roundLimitOff
		{
			get => _roundLimitOff; set => SetProperty( ref _roundLimitOff, value );
		}
		public string roundLimitDangerous
		{
			get => _roundLimitDangerous; set => SetProperty( ref _roundLimitDangerous, value );
		}
		public string roundLimitLabel
		{
			get => _roundLimitLabel; set => SetProperty( ref _roundLimitLabel, value );
		}
		public string skipWarpIntroLabel
		{
			get => _skipWarpIntroLabel; set => SetProperty( ref _skipWarpIntroLabel, value );
		}
	}

	public class UITitle : ObservableObject
	{
		private string _menuHeading, _newGameBtn, _continueBtn, _campaignsBtn, _optionsBtn, _supportUC, _docsUC, _newCampaign, _loadCampaign, _confirmDelete, _delete, _expansions, _tutorialUC, _saga, _campaigns, _classic;

		public string menuHeading
		{
			get => _menuHeading; set => SetProperty( ref _menuHeading, value );
		}
		public string newGameBtn
		{
			get => _newGameBtn; set => SetProperty( ref _newGameBtn, value );
		}
		public string continueBtn
		{
			get => _continueBtn; set => SetProperty( ref _continueBtn, value );
		}
		public string campaignsBtn
		{
			get => _campaignsBtn; set => SetProperty( ref _campaignsBtn, value );
		}
		public string optionsBtn
		{
			get => _optionsBtn; set => SetProperty( ref _optionsBtn, value );
		}
		public string supportUC
		{
			get => _supportUC; set => SetProperty( ref _supportUC, value );
		}
		public string docsUC
		{
			get => _docsUC; set => SetProperty( ref _docsUC, value );
		}
		public string newCampaign
		{
			get => _newCampaign; set => SetProperty( ref _newCampaign, value );
		}
		public string loadCampaign
		{
			get => _loadCampaign; set => SetProperty( ref _loadCampaign, value );
		}
		public string confirmDelete
		{
			get => _confirmDelete; set => SetProperty( ref _confirmDelete, value );
		}
		public string delete
		{
			get => _delete; set => SetProperty( ref _delete, value );
		}
		public string expansions
		{
			get => _expansions; set => SetProperty( ref _expansions, value );
		}
		public string tutorialUC
		{
			get => _tutorialUC; set => SetProperty( ref _tutorialUC, value );
		}
		public string saga
		{
			get => _saga; set => SetProperty( ref _saga, value );
		}
		public string campaigns
		{
			get => _campaigns; set => SetProperty( ref _campaigns, value );
		}
		public string classic
		{
			get => _classic; set => SetProperty( ref _classic, value );
		}
	}

	public class UISetup : ObservableObject
	{
		private string _settingsHeading, _chooseMission, _viewCardBtn, _missionInfoBtn, _threatLevelHeading, _addtlThreatHeading, _deploymentHeading, _yes, _no, _back, _difficulty, _easy, _normal, _hard, _imperials, _mercenaries, _adaptive, _groupsHeading, _choose, _zoom, _initialHeading, _reservedHeading, _villainsHeading, _ignoredHeading, _addHero, _addAlly, _threatCostHeading, _cancel, _continueBtn, _saved, _loaded, _selected, _enemyChooser, _missionChooser, _heroAllyChooser, _adaptiveInfoUC, _chooseHeroesHeading;

		public string settingsHeading
		{
			get => _settingsHeading; set => SetProperty( ref _settingsHeading, value );
		}
		public string chooseMission
		{
			get => _chooseMission; set => SetProperty( ref _chooseMission, value );
		}
		public string viewCardBtn
		{
			get => _viewCardBtn; set => SetProperty( ref _viewCardBtn, value );
		}
		public string missionInfoBtn
		{
			get => _missionInfoBtn; set => SetProperty( ref _missionInfoBtn, value );
		}
		public string threatLevelHeading
		{
			get => _threatLevelHeading; set => SetProperty( ref _threatLevelHeading, value );
		}
		public string addtlThreatHeading
		{
			get => _addtlThreatHeading; set => SetProperty( ref _addtlThreatHeading, value );
		}
		public string deploymentHeading
		{
			get => _deploymentHeading; set => SetProperty( ref _deploymentHeading, value );
		}
		public string yes
		{
			get => _yes; set => SetProperty( ref _yes, value );
		}
		public string no
		{
			get => _no; set => SetProperty( ref _no, value );
		}
		public string back
		{
			get => _back; set => SetProperty( ref _back, value );
		}
		public string difficulty
		{
			get => _difficulty; set => SetProperty( ref _difficulty, value );
		}
		public string easy
		{
			get => _easy; set => SetProperty( ref _easy, value );
		}
		public string normal
		{
			get => _normal; set => SetProperty( ref _normal, value );
		}
		public string hard
		{
			get => _hard; set => SetProperty( ref _hard, value );
		}
		public string imperials
		{
			get => _imperials; set => SetProperty( ref _imperials, value );
		}
		public string mercenaries
		{
			get => _mercenaries; set => SetProperty( ref _mercenaries, value );
		}
		public string adaptive
		{
			get => _adaptive; set => SetProperty( ref _adaptive, value );
		}
		public string groupsHeading
		{
			get => _groupsHeading; set => SetProperty( ref _groupsHeading, value );
		}
		public string choose
		{
			get => _choose; set => SetProperty( ref _choose, value );
		}
		public string zoom
		{
			get => _zoom; set => SetProperty( ref _zoom, value );
		}
		public string initialHeading
		{
			get => _initialHeading; set => SetProperty( ref _initialHeading, value );
		}
		public string reservedHeading
		{
			get => _reservedHeading; set => SetProperty( ref _reservedHeading, value );
		}
		public string villainsHeading
		{
			get => _villainsHeading; set => SetProperty( ref _villainsHeading, value );
		}
		public string ignoredHeading
		{
			get => _ignoredHeading; set => SetProperty( ref _ignoredHeading, value );
		}
		public string addHero
		{
			get => _addHero; set => SetProperty( ref _addHero, value );
		}
		public string addAlly
		{
			get => _addAlly; set => SetProperty( ref _addAlly, value );
		}
		public string threatCostHeading
		{
			get => _threatCostHeading; set => SetProperty( ref _threatCostHeading, value );
		}
		public string cancel
		{
			get => _cancel; set => SetProperty( ref _cancel, value );
		}
		public string continueBtn
		{
			get => _continueBtn; set => SetProperty( ref _continueBtn, value );
		}
		public string saved
		{
			get => _saved; set => SetProperty( ref _saved, value );
		}
		public string loaded
		{
			get => _loaded; set => SetProperty( ref _loaded, value );
		}
		public string selected
		{
			get => _selected; set => SetProperty( ref _selected, value );
		}
		public string enemyChooser
		{
			get => _enemyChooser; set => SetProperty( ref _enemyChooser, value );
		}
		public string missionChooser
		{
			get => _missionChooser; set => SetProperty( ref _missionChooser, value );
		}
		public string heroAllyChooser
		{
			get => _heroAllyChooser; set => SetProperty( ref _heroAllyChooser, value );
		}
		public string adaptiveInfoUC
		{
			get => _adaptiveInfoUC; set => SetProperty( ref _adaptiveInfoUC, value );
		}
		public string chooseHeroesHeading
		{
			get => _chooseHeroesHeading; set => SetProperty( ref _chooseHeroesHeading, value );
		}
	}

	public class SagaUISetup : ObservableObject
	{
		private string _groupsText, _villainsBtn, _tilesBtn, _setupStartBtn, _officialBtn, _customBtn, _missionCardBtn, _campaignJournalUC, _importBtn;

		public string groupsText
		{
			get => _groupsText; set => SetProperty( ref _groupsText, value );
		}
		public string villainsBtn
		{
			get => _villainsBtn; set => SetProperty( ref _villainsBtn, value );
		}
		public string tilesBtn
		{
			get => _tilesBtn; set => SetProperty( ref _tilesBtn, value );
		}
		public string setupStartBtn
		{
			get => _setupStartBtn; set => SetProperty( ref _setupStartBtn, value );
		}
		public string officialBtn
		{
			get => _officialBtn; set => SetProperty( ref _officialBtn, value );
		}
		public string customBtn
		{
			get => _customBtn; set => SetProperty( ref _customBtn, value );
		}
		public string missionCardBtn
		{
			get => _missionCardBtn; set => SetProperty( ref _missionCardBtn, value );
		}
		public string campaignJournalUC
		{
			get => _campaignJournalUC; set => SetProperty( ref _campaignJournalUC, value );
		}
		public string importBtn
		{
			get => _importBtn; set => SetProperty( ref _importBtn, value );
		}
	}

	public class SagaMainApp : ObservableObject
	{
		private string _tooltipHideUIUC, _roundIncreasedUC, _endOfMissionUC, _deployMessageUC, _noDPWarningUC, _mmAddTilesUC, _mmRemoveTilesUC, _mmAddEntitiesUC, _groupsReadyUC, _groupsExhaustUC, _repositionTargetUC, _doorsUC, _cratesUC, _terminalsUC, _tokensUC, _woundUC, _withdrawUC, _exhaustUC, _defeatUC, _imperialMenu, _medpacInfoUC, _cannotDefeatUC, _missionLogTitle;

		public string tooltipHideUIUC
		{
			get => _tooltipHideUIUC; set => SetProperty( ref _tooltipHideUIUC, value );
		}
		public string exhaustUC
		{
			get => _exhaustUC; set => SetProperty( ref _exhaustUC, value );
		}
		public string defeatUC
		{
			get => _defeatUC; set => SetProperty( ref _defeatUC, value );
		}
		public string roundIncreasedUC
		{
			get => _roundIncreasedUC; set => SetProperty( ref _roundIncreasedUC, value );
		}
		public string endOfMissionUC
		{
			get => _endOfMissionUC; set => SetProperty( ref _endOfMissionUC, value );
		}
		public string deployMessageUC
		{
			get => _deployMessageUC; set => SetProperty( ref _deployMessageUC, value );
		}
		public string noDPWarningUC
		{
			get => _noDPWarningUC; set => SetProperty( ref _noDPWarningUC, value );
		}
		public string mmAddTilesUC
		{
			get => _mmAddTilesUC; set => SetProperty( ref _mmAddTilesUC, value );
		}
		public string mmRemoveTilesUC
		{
			get => _mmRemoveTilesUC; set => SetProperty( ref _mmRemoveTilesUC, value );
		}
		public string mmAddEntitiesUC
		{
			get => _mmAddEntitiesUC; set => SetProperty( ref _mmAddEntitiesUC, value );
		}
		public string groupsReadyUC
		{
			get => _groupsReadyUC; set => SetProperty( ref _groupsReadyUC, value );
		}
		public string groupsExhaustUC
		{
			get => _groupsExhaustUC; set => SetProperty( ref _groupsExhaustUC, value );
		}
		public string repositionTargetUC
		{
			get => _repositionTargetUC; set => SetProperty( ref _repositionTargetUC, value );
		}
		public string doorsUC
		{
			get => _doorsUC; set => SetProperty( ref _doorsUC, value );
		}
		public string cratesUC
		{
			get => _cratesUC; set => SetProperty( ref _cratesUC, value );
		}
		public string terminalsUC
		{
			get => _terminalsUC; set => SetProperty( ref _terminalsUC, value );
		}
		public string tokensUC
		{
			get => _tokensUC; set => SetProperty( ref _tokensUC, value );
		}
		public string woundUC
		{
			get => _woundUC; set => SetProperty( ref _woundUC, value );
		}
		public string withdrawUC
		{
			get => _withdrawUC; set => SetProperty( ref _withdrawUC, value );
		}
		public string imperialMenu
		{
			get => _imperialMenu; set => SetProperty( ref _imperialMenu, value );
		}
		public string medpacInfoUC
		{
			get => _medpacInfoUC; set => SetProperty( ref _medpacInfoUC, value );
		}
		public string cannotDefeatUC
		{
			get => _cannotDefeatUC; set => SetProperty( ref _cannotDefeatUC, value );
		}
		public string missionLogTitle
		{
			get => _missionLogTitle; set => SetProperty( ref _missionLogTitle, value );
		}
	}

	public class UIMainApp : ObservableObject
	{
		private string _eliteUpgradeMsgUC, _eliteDowngradeMsgUC, _restoredMsgUC, _restoreErrorMsgUC, _pauseDepMsgUC, _unPauseDepMsgUC, _pauseThreatMsgUC, _UnPauseThreatMsgUC, _deploymentHeading, _reservedBtn, _allyBtn, _enemyBtn, _randomBtn, _modThreatHeading, _applyBtn, _roundHeading, _depTypeHeading, _eventHeading, _randomHeading, _maxThreatHeading, _endRoundBtn, _fameHeading, _awardsHeading, _fame1UC, _fameItem1UC, _fameItem2UC, _fame2UC, _continueBtn, _debugThreatUC, _debugDepModUC, _debugDepHandUC, _tooltipRulesUC, _tooltipInfoUC, _tooltipPauseDepUC, _tooltipPauseThreatUC, _tooltipOpDepUC, _tooltipSettingsUC, _tooltipImpHandUC, _tooltipActivateUC, _tooltipFameUC, _tooltipDashboardUC, _confirm, _cancel, _deploy, _threatIncreasedUC, _reinforceWarningUC, _deploymentWarningUC, _calmMessageUC, _close, _deployModeCalm, _deployModeReinforcements, _deployModeLanding, _deployModeOnslaught, _fameIncreasedUC, _noRandomMatchesUC, _depCostUC, _noAbilitiesUC, _ignoredAbilitiesUC, _noKeywordsUC, _noneUC, _rewardUC, _pageUC, _optionalDeployment, _tooltipMedPacUC;

		public string eliteUpgradeMsgUC
		{
			get => _eliteUpgradeMsgUC; set => SetProperty( ref _eliteUpgradeMsgUC, value );
		}
		public string eliteDowngradeMsgUC
		{
			get => _eliteDowngradeMsgUC; set => SetProperty( ref _eliteDowngradeMsgUC, value );
		}
		public string restoredMsgUC
		{
			get => _restoredMsgUC; set => SetProperty( ref _restoredMsgUC, value );
		}
		public string restoreErrorMsgUC
		{
			get => _restoreErrorMsgUC; set => SetProperty( ref _restoreErrorMsgUC, value );
		}
		public string pauseDepMsgUC
		{
			get => _pauseDepMsgUC; set => SetProperty( ref _pauseDepMsgUC, value );
		}
		public string unPauseDepMsgUC
		{
			get => _unPauseDepMsgUC; set => SetProperty( ref _unPauseDepMsgUC, value );
		}
		public string pauseThreatMsgUC
		{
			get => _pauseThreatMsgUC; set => SetProperty( ref _pauseThreatMsgUC, value );
		}
		public string UnPauseThreatMsgUC
		{
			get => _UnPauseThreatMsgUC; set => SetProperty( ref _UnPauseThreatMsgUC, value );
		}
		public string deploymentHeading
		{
			get => _deploymentHeading; set => SetProperty( ref _deploymentHeading, value );
		}
		public string reservedBtn
		{
			get => _reservedBtn; set => SetProperty( ref _reservedBtn, value );
		}
		public string allyBtn
		{
			get => _allyBtn; set => SetProperty( ref _allyBtn, value );
		}
		public string enemyBtn
		{
			get => _enemyBtn; set => SetProperty( ref _enemyBtn, value );
		}
		public string randomBtn
		{
			get => _randomBtn; set => SetProperty( ref _randomBtn, value );
		}
		public string modThreatHeading
		{
			get => _modThreatHeading; set => SetProperty( ref _modThreatHeading, value );
		}
		public string applyBtn
		{
			get => _applyBtn; set => SetProperty( ref _applyBtn, value );
		}
		public string roundHeading
		{
			get => _roundHeading; set => SetProperty( ref _roundHeading, value );
		}
		public string depTypeHeading
		{
			get => _depTypeHeading; set => SetProperty( ref _depTypeHeading, value );
		}
		public string eventHeading
		{
			get => _eventHeading; set => SetProperty( ref _eventHeading, value );
		}
		public string randomHeading
		{
			get => _randomHeading; set => SetProperty( ref _randomHeading, value );
		}
		public string maxThreatHeading
		{
			get => _maxThreatHeading; set => SetProperty( ref _maxThreatHeading, value );
		}
		public string endRoundBtn
		{
			get => _endRoundBtn; set => SetProperty( ref _endRoundBtn, value );
		}
		public string fameHeading
		{
			get => _fameHeading; set => SetProperty( ref _fameHeading, value );
		}
		public string awardsHeading
		{
			get => _awardsHeading; set => SetProperty( ref _awardsHeading, value );
		}
		public string fame1UC
		{
			get => _fame1UC; set => SetProperty( ref _fame1UC, value );
		}
		public string fameItem1UC
		{
			get => _fameItem1UC; set => SetProperty( ref _fameItem1UC, value );
		}
		public string fameItem2UC
		{
			get => _fameItem2UC; set => SetProperty( ref _fameItem2UC, value );
		}
		public string fame2UC
		{
			get => _fame2UC; set => SetProperty( ref _fame2UC, value );
		}
		public string continueBtn
		{
			get => _continueBtn; set => SetProperty( ref _continueBtn, value );
		}
		public string debugThreatUC
		{
			get => _debugThreatUC; set => SetProperty( ref _debugThreatUC, value );
		}
		public string debugDepModUC
		{
			get => _debugDepModUC; set => SetProperty( ref _debugDepModUC, value );
		}
		public string debugDepHandUC
		{
			get => _debugDepHandUC; set => SetProperty( ref _debugDepHandUC, value );
		}
		public string tooltipRulesUC
		{
			get => _tooltipRulesUC; set => SetProperty( ref _tooltipRulesUC, value );
		}
		public string tooltipInfoUC
		{
			get => _tooltipInfoUC; set => SetProperty( ref _tooltipInfoUC, value );
		}
		public string tooltipPauseDepUC
		{
			get => _tooltipPauseDepUC; set => SetProperty( ref _tooltipPauseDepUC, value );
		}
		public string tooltipPauseThreatUC
		{
			get => _tooltipPauseThreatUC; set => SetProperty( ref _tooltipPauseThreatUC, value );
		}
		public string tooltipOpDepUC
		{
			get => _tooltipOpDepUC; set => SetProperty( ref _tooltipOpDepUC, value );
		}
		public string tooltipSettingsUC
		{
			get => _tooltipSettingsUC; set => SetProperty( ref _tooltipSettingsUC, value );
		}
		public string tooltipImpHandUC
		{
			get => _tooltipImpHandUC; set => SetProperty( ref _tooltipImpHandUC, value );
		}
		public string tooltipActivateUC
		{
			get => _tooltipActivateUC; set => SetProperty( ref _tooltipActivateUC, value );
		}
		public string tooltipFameUC
		{
			get => _tooltipFameUC; set => SetProperty( ref _tooltipFameUC, value );
		}
		public string tooltipDashboardUC
		{
			get => _tooltipDashboardUC; set => SetProperty( ref _tooltipDashboardUC, value );
		}
		public string tooltipMedPacUC
		{
			get => _tooltipMedPacUC; set => SetProperty( ref _tooltipMedPacUC, value );
		}
		public string confirm
		{
			get => _confirm; set => SetProperty( ref _confirm, value );
		}
		public string cancel
		{
			get => _cancel; set => SetProperty( ref _cancel, value );
		}
		public string deploy
		{
			get => _deploy; set => SetProperty( ref _deploy, value );
		}
		public string threatIncreasedUC
		{
			get => _threatIncreasedUC; set => SetProperty( ref _threatIncreasedUC, value );
		}
		public string reinforceWarningUC
		{
			get => _reinforceWarningUC; set => SetProperty( ref _reinforceWarningUC, value );
		}
		public string deploymentWarningUC
		{
			get => _deploymentWarningUC; set => SetProperty( ref _deploymentWarningUC, value );
		}
		public string calmMessageUC
		{
			get => _calmMessageUC; set => SetProperty( ref _calmMessageUC, value );
		}
		public string close
		{
			get => _close; set => SetProperty( ref _close, value );
		}
		public string deployModeCalm
		{
			get => _deployModeCalm; set => SetProperty( ref _deployModeCalm, value );
		}
		public string deployModeReinforcements
		{
			get => _deployModeReinforcements; set => SetProperty( ref _deployModeReinforcements, value );
		}
		public string deployModeLanding
		{
			get => _deployModeLanding; set => SetProperty( ref _deployModeLanding, value );
		}
		public string deployModeOnslaught
		{
			get => _deployModeOnslaught; set => SetProperty( ref _deployModeOnslaught, value );
		}
		public string fameIncreasedUC
		{
			get => _fameIncreasedUC; set => SetProperty( ref _fameIncreasedUC, value );
		}
		public string noRandomMatchesUC
		{
			get => _noRandomMatchesUC; set => SetProperty( ref _noRandomMatchesUC, value );
		}
		public string depCostUC
		{
			get => _depCostUC; set => SetProperty( ref _depCostUC, value );
		}
		public string noAbilitiesUC
		{
			get => _noAbilitiesUC; set => SetProperty( ref _noAbilitiesUC, value );
		}
		public string ignoredAbilitiesUC
		{
			get => _ignoredAbilitiesUC; set => SetProperty( ref _ignoredAbilitiesUC, value );
		}
		public string noKeywordsUC
		{
			get => _noKeywordsUC; set => SetProperty( ref _noKeywordsUC, value );
		}
		public string noneUC
		{
			get => _noneUC; set => SetProperty( ref _noneUC, value );
		}
		public string rewardUC
		{
			get => _rewardUC; set => SetProperty( ref _rewardUC, value );
		}
		public string pageUC
		{
			get => _pageUC; set => SetProperty( ref _pageUC, value );
		}
		public string optionalDeployment
		{
			get => _optionalDeployment; set => SetProperty( ref _optionalDeployment, value );
		}
	}

	public class UIExpansions : ObservableObject
	{
		private string _core, _twin, _hoth, _bespin, _jabba, _empire, _lothal, _other, _figurepacks;

		public string core
		{
			get => _core; set => SetProperty( ref _core, value );
		}
		public string twin
		{
			get => _twin; set => SetProperty( ref _twin, value );
		}
		public string hoth
		{
			get => _hoth; set => SetProperty( ref _hoth, value );
		}
		public string bespin
		{
			get => _bespin; set => SetProperty( ref _bespin, value );
		}
		public string jabba
		{
			get => _jabba; set => SetProperty( ref _jabba, value );
		}
		public string empire
		{
			get => _empire; set => SetProperty( ref _empire, value );
		}
		public string lothal
		{
			get => _lothal; set => SetProperty( ref _lothal, value );
		}
		public string other
		{
			get => _other; set => SetProperty( ref _other, value );
		}
		public string figurepacks
		{
			get => _figurepacks; set => SetProperty( ref _figurepacks, value );
		}
	}

	public class UICampaign : ObservableObject
	{
		private string _threatInfoUC, _modeIntroductionUC, _modeStoryUC, _modeSideUC, _forcedUC, _modeInterludeUC, _modeFinaleUC, _campaignNameUC, _customCampaign, _addForcedMissionUC, _tierUC, _selectMissionUC, _customUC, _creditsUC, _fameUC, _awardsUC, _campaignSetup, _itemsUC, _rewardsUC, _villainsUC, _alliesUC, _threat, _agendaUC, _modifyUC, _removeUC, _otherUC, _campaignUC, _generalUC, _heroUC, _personalUC, _campaignSetupUC, _campaignDescriptionUC, _sagaDescriptionUC, _classicDescriptionUC, _agendaMission, _agendaImperialUC, _agendaRebelUC, _xpUC, _costUC, _missionTypeMockupUC, _missionNameMockupUC, _itemMockupUC;

		public string threatInfoUC
		{
			get => _threatInfoUC; set => SetProperty( ref _threatInfoUC, value );
		}
		public string modeIntroductionUC
		{
			get => _modeIntroductionUC; set => SetProperty( ref _modeIntroductionUC, value );
		}
		public string modeStoryUC
		{
			get => _modeStoryUC; set => SetProperty( ref _modeStoryUC, value );
		}
		public string modeSideUC
		{
			get => _modeSideUC; set => SetProperty( ref _modeSideUC, value );
		}
		public string forcedUC
		{
			get => _forcedUC; set => SetProperty( ref _forcedUC, value );
		}
		public string modeInterludeUC
		{
			get => _modeInterludeUC; set => SetProperty( ref _modeInterludeUC, value );
		}
		public string modeFinaleUC
		{
			get => _modeFinaleUC; set => SetProperty( ref _modeFinaleUC, value );
		}
		public string campaignNameUC
		{
			get => _campaignNameUC; set => SetProperty( ref _campaignNameUC, value );
		}
		public string customCampaign
		{
			get => _customCampaign; set => SetProperty( ref _customCampaign, value );
		}
		public string addForcedMissionUC
		{
			get => _addForcedMissionUC; set => SetProperty( ref _addForcedMissionUC, value );
		}
		public string tierUC
		{
			get => _tierUC; set => SetProperty( ref _tierUC, value );
		}
		public string selectMissionUC
		{
			get => _selectMissionUC; set => SetProperty( ref _selectMissionUC, value );
		}
		public string customUC
		{
			get => _customUC; set => SetProperty( ref _customUC, value );
		}
		public string creditsUC
		{
			get => _creditsUC; set => SetProperty( ref _creditsUC, value );
		}
		public string fameUC
		{
			get => _fameUC; set => SetProperty( ref _fameUC, value );
		}
		public string awardsUC
		{
			get => _awardsUC; set => SetProperty( ref _awardsUC, value );
		}
		public string campaignSetup
		{
			get => _campaignSetup; set => SetProperty( ref _campaignSetup, value );
		}
		public string itemsUC
		{
			get => _itemsUC; set => SetProperty( ref _itemsUC, value );
		}
		public string rewardsUC
		{
			get => _rewardsUC; set => SetProperty( ref _rewardsUC, value );
		}
		public string villainsUC
		{
			get => _villainsUC; set => SetProperty( ref _villainsUC, value );
		}
		public string alliesUC
		{
			get => _alliesUC; set => SetProperty( ref _alliesUC, value );
		}
		public string threat
		{
			get => _threat; set => SetProperty( ref _threat, value );
		}
		public string agendaUC
		{
			get => _agendaUC; set => SetProperty( ref _agendaUC, value );
		}
		public string modifyUC
		{
			get => _modifyUC; set => SetProperty( ref _modifyUC, value );
		}
		public string removeUC
		{
			get => _removeUC; set => SetProperty( ref _removeUC, value );
		}
		public string otherUC
		{
			get => _otherUC; set => SetProperty( ref _otherUC, value );
		}
		public string campaignUC
		{
			get => _campaignUC; set => SetProperty( ref _campaignUC, value );
		}
		public string generalUC
		{
			get => _generalUC; set => SetProperty( ref _generalUC, value );
		}
		public string heroUC
		{
			get => _heroUC; set => SetProperty( ref _heroUC, value );
		}
		public string personalUC
		{
			get => _personalUC; set => SetProperty( ref _personalUC, value );
		}
		public string campaignSetupUC
		{
			get => _campaignSetupUC; set => SetProperty( ref _campaignSetupUC, value );
		}
		public string campaignDescriptionUC
		{
			get => _campaignDescriptionUC; set => SetProperty( ref _campaignDescriptionUC, value );
		}
		public string sagaDescriptionUC
		{
			get => _sagaDescriptionUC; set => SetProperty( ref _sagaDescriptionUC, value );
		}
		public string classicDescriptionUC
		{
			get => _classicDescriptionUC; set => SetProperty( ref _classicDescriptionUC, value );
		}
		public string agendaMission
		{
			get => _agendaMission; set => SetProperty( ref _agendaMission, value );
		}
		public string agendaImperialUC
		{
			get => _agendaImperialUC; set => SetProperty( ref _agendaImperialUC, value );
		}
		public string agendaRebelUC
		{
			get => _agendaRebelUC; set => SetProperty( ref _agendaRebelUC, value );
		}
		public string xpUC
		{
			get => _xpUC; set => SetProperty( ref _xpUC, value );
		}
		public string costUC
		{
			get => _costUC; set => SetProperty( ref _costUC, value );
		}
		public string missionTypeMockupUC
		{
			get => _missionTypeMockupUC; set => SetProperty( ref _missionTypeMockupUC, value );
		}
		public string missionNameMockupUC
		{
			get => _missionNameMockupUC; set => SetProperty( ref _missionNameMockupUC, value );
		}
		public string itemMockupUC
		{
			get => _itemMockupUC; set => SetProperty( ref _itemMockupUC, value );
		}
	}

	public class UILogger : ObservableObject
	{
		private string _textLabel, _inputPromptLabel, _selectionPromptLabel, _selectionLabel, _groupActivationLabel, _groupDeployedLabel, _groupRemovedLabel, _groupDefeatedLabel, _deploymentEventLabel, _inputValueLabel;

		public string textLabel
		{
			get => _textLabel; set => SetProperty( ref _textLabel, value );
		}

		public string inputPromptLabel
		{
			get => _inputPromptLabel; set => SetProperty( ref _inputPromptLabel, value );
		}

		public string selectionPromptLabel
		{
			get => _selectionPromptLabel; set => SetProperty( ref _selectionPromptLabel, value );
		}

		public string selectionLabel
		{
			get => _selectionLabel; set => SetProperty( ref _selectionLabel, value );
		}

		public string groupActivationLabel
		{
			get => _groupActivationLabel; set => SetProperty( ref _groupActivationLabel, value );
		}

		public string groupDeployedLabel
		{
			get => _groupDeployedLabel; set => SetProperty( ref _groupDeployedLabel, value );
		}

		public string groupRemovedLabel
		{
			get => _groupRemovedLabel; set => SetProperty( ref _groupRemovedLabel, value );
		}

		public string groupDefeatedLabel
		{
			get => _groupDefeatedLabel; set => SetProperty( ref _groupDefeatedLabel, value );
		}

		public string deploymentEventLabel
		{
			get => _deploymentEventLabel; set => SetProperty( ref _deploymentEventLabel, value );
		}
		public string inputValueLabel
		{
			get => _inputValueLabel; set => SetProperty( ref _inputValueLabel, value );
		}
	}

	public class UIHelpOverlay : ObservableObject
	{
		public HelpOverlayPanel[] helpOverlayPanels { get; set; }
	}

	public class HelpOverlayPanel : ObservableObject
	{
		private string _panelHelpID;
		private HelpOverlayItem[] _helpItems;

		public string panelHelpID { get => _panelHelpID; set { SetProperty( ref _panelHelpID, value ); } }
		public HelpOverlayItem[] helpItems { get => _helpItems; set { SetProperty( ref _helpItems, value ); } }
	}

	public class HelpOverlayItem : ObservableObject
	{
		private string _id;
		private string _helpText;

		public string id { get => _id; set { SetProperty( ref _id, value ); } }
		public string helpText { get => _helpText; set { SetProperty( ref _helpText, value ); } }
	}
}