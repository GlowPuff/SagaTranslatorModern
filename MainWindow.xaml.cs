using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		private System.Timers.Timer infoTimer;
		private PanelBase _translationPanel;
		private string _selectedLanguageID, oldStatusText;
		private SolidColorBrush _statusColor;

		public ProjectListData translatorData;
		public ObservableCollection<string> languageList { get; set; }

		public SolidColorBrush statusColor { get => _statusColor; set { _statusColor = value; PC(); } }
		public string selectedLanguageID
		{
			get => _selectedLanguageID; set
			{
				_selectedLanguageID = value;
				translationPanel?.SetLanguage( value );
				PC();
			}
		}
		public PanelBase translationPanel { get { return _translationPanel; } set { _translationPanel = value; PC(); } }

		public event PropertyChangedEventHandler PropertyChanged;
		public void PC( [CallerMemberName] string n = "" )
		{
			if ( !string.IsNullOrEmpty( n ) )
				PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( n ) );
		}

		public MainWindow( TranslationType projectType )
		{
			InitializeComponent();
			DataContext = this;

			languageList = new( new[] { "English (EN)", "German (DE)", "Spanish (ES)", "French (FR)", "Polski (PL)", "Italian (IT)", "Magyar (HU)", "Norwegian (NO)", "Russian (RU)" } );
			selectedLanguageID = "English (EN)";

			InitTimer();

			switch ( projectType )
			{
				case TranslationType.UI:
					translationPanel = new UIPanel();
					break;
				case TranslationType.BonusEffects:
					translationPanel = new BonusEffectsPanel();
					break;
				case TranslationType.EnemyInstructions:
					translationPanel = new InstructionsPanel();
					break;
				case TranslationType.CampaignItems:
					translationPanel = new CampaignItemsPanel();
					break;
				case TranslationType.CampaignRewards:
					translationPanel = new CampaignRewardsPanel();
					break;
				case TranslationType.CampaignSkills:
					translationPanel = new CampaignSkillsPanel();
					break;
				case TranslationType.HelpOverlays:
					translationPanel = new UIHelpOverlayPanel();
					break;
			}
		}

		public MainWindow( TranslationType projectType, SourceData data )
		{
			InitializeComponent();
			DataContext = this;

			languageList = new( new[] { "English (EN)", "German (DE)", "Spanish (ES)", "French (FR)", "Polski (PL)", "Italian (IT)", "Magyar (HU)", "Norwegian (NO)", "Russian (RU)" } );
			selectedLanguageID = "English (EN)";

			InitTimer();

			switch ( projectType )
			{
				case TranslationType.CampaignInfo:
					translationPanel = new CampaignInfoPanel( data );
					break;
				case TranslationType.DeploymentCardText:
					translationPanel = new DeploymentGroupsPanel( data );
					break;
				case TranslationType.MissionInfoRules:
					translationPanel = new MissionInfoPanel( data );
					break;
				case TranslationType.MissionCardText:
					translationPanel = new MissionCardTextPanel( data );
					break;
				case TranslationType.Mission:
				case TranslationType.Tutorials:
					translationPanel = new MissionPanel( data );
					break;
			}
		}

		private void InitTimer()
		{
			infoTimer = new( 3000 );
			infoTimer.AutoReset = false;
			infoTimer.Elapsed += ( s, e ) =>
			{
				Dispatcher.Invoke( () =>
				{
					translationPanel.commonPanelData.infoText = oldStatusText;
					statusColor = new SolidColorBrush( Color.FromRgb( 62, 62, 66 ) );
				} );
			};
		}

		private void openFileBtn_Click( object sender, RoutedEventArgs e )
		{
			translationPanel.OnOpenFile();
		}

		private void saveFileBtn_Click( object sender, RoutedEventArgs e )
		{
			translationPanel.OnSaveFile();
		}

		private void selectorBtn_Click( object sender, RoutedEventArgs e )
		{
			//translationPanel.debug();
			var ret = MessageBox.Show( "Are you sure you want to switch to a different translation data set?\n\nAny unsaved work will be lost.", "Leave This Translation?", MessageBoxButton.YesNo, MessageBoxImage.Question );
			if ( ret == MessageBoxResult.Yes )
			{
				var dlg = new ProjectSelectorWindow();
				dlg.Show();
				Close();
			}
		}

		private void Window_PreviewKeyDown( object sender, KeyEventArgs e )
		{
			if ( (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) )
			{
				translationPanel.OnSaveFile();
			}
		}

		public void SetStatus( string s )
		{
			oldStatusText = translationPanel.commonPanelData.infoText;
			infoTimer.Stop();
			translationPanel.commonPanelData.infoText = s;
			statusColor = new SolidColorBrush( Color.FromRgb( 131, 78, 207 ) );
			infoTimer.Start();
		}
	}
}