using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Imperial_Commander_Editor;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for ProjectSelectorWindow.xaml
	/// </summary>
	public partial class ProjectSelectorWindow : Window
	{
		public ProjectListData translatorData { get; set; }

		public ProjectSelectorWindow()
		{
			InitializeComponent();
			DataContext = translatorData = new();

			FileManager.CreateBaseDirectory();
			Utils.InitUtils();//load json data
		}

		private void Window_MouseDown( object sender, MouseButtonEventArgs e )
		{
			if ( e.LeftButton == MouseButtonState.Pressed )
				DragMove();
		}

		private void cancelButton_Click( object sender, RoutedEventArgs e )
		{
			Close();
		}

		private void projectButton_Click( object sender, RoutedEventArgs e )
		{
			var item = (sender as Button).DataContext as ProjectListItem;

			switch ( item.translationType )
			{
				//single shots
				case TranslationType.UI:
				case TranslationType.BonusEffects:
				case TranslationType.EnemyInstructions:
				case TranslationType.CampaignItems:
				case TranslationType.CampaignRewards:
				case TranslationType.CampaignSkills:
				case TranslationType.HelpOverlays:
					var dlg = new MainWindow( item.translationType );
					dlg.Show();
					Close();
					break;
				//these use a combobox
				case TranslationType.CampaignInfo:
				case TranslationType.DeploymentCardText:
				case TranslationType.MissionInfoRules:
				case TranslationType.MissionCardText:
				case TranslationType.Mission:
				case TranslationType.Tutorials:
					var ci = new SelectorDialog( item.translationType );
					ci.ShowDialog();
					if ( ci.canClose )
						Close();
					break;
			}
		}
	}
}
