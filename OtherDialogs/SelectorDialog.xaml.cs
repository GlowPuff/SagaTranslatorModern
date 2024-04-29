using System.IO;
using System.Windows;
using Imperial_Commander_Editor;
using Microsoft.Win32;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for QueryCampaignInfoDialog.xaml
	/// </summary>
	public partial class SelectorDialog : Window
	{
		private TranslationType _translationType;
		private int[] missionCounts;

		public SelectorData queryCampaignInfoData { get; set; }
		public SourceData sourceData;
		public bool canClose = false;

		public SelectorDialog( TranslationType tt )
		{
			InitializeComponent();

			_translationType = tt;
			queryCampaignInfoData = new( tt );
			switch ( tt )
			{
				case TranslationType.CampaignInfo:
					queryCampaignInfoData.combo1Label = "Choose an Expansion";
					break;
				case TranslationType.DeploymentCardText:
					queryCampaignInfoData.combo1Label = "Choose a Deployment Group";
					break;
				case TranslationType.MissionInfoRules:
					queryCampaignInfoData.combo1Label = "Choose an Expansion";
					queryCampaignInfoData.combo2Label = "Choose a Data File";
					break;
				case TranslationType.MissionCardText:
					queryCampaignInfoData.combo1Label = "Choose an Expansion";
					break;
				case TranslationType.Mission:
					queryCampaignInfoData.combo1Label = "Choose an Expansion";
					queryCampaignInfoData.combo2Label = "Choose a Mission";
					break;
				case TranslationType.Tutorials:
					queryCampaignInfoData.combo1Label = "Choose a Tutorial";
					break;
			};

			missionCounts = [32, 6, 16, 6, 16, 16, 6, 40];
			sourceData = new();
			DataContext = queryCampaignInfoData;
		}

		private void continueBtn_Click( object sender, RoutedEventArgs e )
		{
			var dlg = new MainWindow( _translationType, sourceData );
			dlg.Show();
			canClose = true;
			Close();
		}

		private void cancelBtn_Click( object sender, RoutedEventArgs e )
		{
			Close();
		}

		private void combo1CB_SelectionChanged( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
		{
			queryCampaignInfoData.comboList2 = new ComboBoxMeta[0];

			//multiple selections
			if ( _translationType == TranslationType.MissionInfoRules )
			{
				group2.Visibility = Visibility.Visible;
				sourceData.metaDisplay = combo1CB.SelectedItem as ComboBoxMeta;
				var files = FileManager.FindAssetsWithName( sourceData.metaDisplay.assetName );
				queryCampaignInfoData.comboList2 = files.Select( x =>
				{
					var split = x.Split( '.' ).Reverse().Take( 2 ).Reverse().ToArray();
					return new ComboBoxMeta( $"{split[0]}.{split[1]}", x );
				} ).ToArray();
			}
			else if ( _translationType == TranslationType.Mission )
			{
				//if NOT custom Mission selected
				if ( combo1CB.SelectedIndex != 8 )
				{
					customMissionGroup.Visibility = Visibility.Collapsed;
					group2.Visibility = Visibility.Visible;
					sourceData.metaDisplay = TranslatorUtils.UniqueCopy( combo1CB.SelectedItem as ComboBoxMeta );
					queryCampaignInfoData.combo2Label = $"Select a '{sourceData.metaDisplay.displayName}' Mission:";
					string[] files = new string[missionCounts[combo1CB.SelectedIndex]];
					int mIndex = 0;
					for ( int i = 0; i < combo1CB.SelectedIndex; i++ )
					{
						//missionNames[] contains Mission names across ALL expansions in one big array
						//get an index into the array for the selected expansion
						mIndex += missionCounts[i];
					}
					queryCampaignInfoData.comboList2 = files.Select( x =>
					{
						return new ComboBoxMeta( Utils.missionNames[mIndex++].name, $"{sourceData.metaDisplay.assetName}" );
					} ).ToArray();
				}
				else
				{
					customMissionGroup.Visibility = Visibility.Visible;
					group2.Visibility = Visibility.Collapsed;
				}
			}
			else//single selection
			{
				//load the data based on the asset selected
				sourceData.metaDisplay = TranslatorUtils.UniqueCopy( combo1CB.SelectedItem as ComboBoxMeta );
				sourceData.stringifiedJsonData = FileManager.LoadBuiltinJSON( sourceData.metaDisplay.assetName );

				if ( sourceData.stringifiedJsonData != string.Empty )
					continueBtn.IsEnabled = true;
			}
		}

		private void combo2CB_SelectionChanged( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
		{
			if ( combo2CB.SelectedItem is null )
			{
				return;
			}

			if ( _translationType == TranslationType.MissionInfoRules )
			{
				sourceData.metaDisplay = TranslatorUtils.UniqueCopy( combo2CB.SelectedItem as ComboBoxMeta );
				sourceData.stringifiedJsonData = FileManager.LoadBuiltinJSON( sourceData.metaDisplay.assetName );
			}
			else if ( _translationType == TranslationType.Mission )
			{
				sourceData.metaDisplay = TranslatorUtils.UniqueCopy( combo2CB.SelectedItem as ComboBoxMeta );
				sourceData.metaDisplay.assetName = $"{sourceData.metaDisplay.assetName.ToUpper()}{combo2CB.SelectedIndex + 1}_EN.json";
				sourceData.stringifiedJsonData = FileManager.LoadBuiltinJSON( sourceData.metaDisplay.assetName );
			}

			if ( sourceData.stringifiedJsonData != string.Empty )
				continueBtn.IsEnabled = true;
		}

		private void chooseCustomMissionBtn_Click( object sender, RoutedEventArgs e )
		{
			customMissionFilename.Text = "";
			continueBtn.IsEnabled = false;

			OpenFileDialog od = new();
			od.InitialDirectory = FileManager.baseFolder;
			od.Filter = "Mission Translation (*.json)|*.json";
			od.Title = "Choose a Custom Mission Translation";
			if ( od.ShowDialog() == true )
			{
				//check if it's a translation
				FileInfo fi = new( od.FileName );
				string s = File.ReadAllText( od.FileName );
				if ( s.Contains( "timeTick" ) )//it's a Mission
				{
					Utils.ShowError( $"The file appears to be a Mission.  Open a Mission Translation, instead.", "Error Opening File" );
					return;
				}
				else if ( !s.Contains( "\"missionProperties\"" )//it's not a translation
				|| !s.Contains( "\"events\"" )
				|| !s.Contains( "\"mapEntities\"" )
				|| !s.Contains( "\"initialGroups\"" ) )
				{
					Utils.ShowError( $"The file doesn't appear to be a Mission Translation.", "Error Opening File" );
					return;
				}

				sourceData.stringifiedJsonData = s;
				sourceData.metaDisplay = new()
				{
					displayName = fi.Name,
					assetName = fi.Name,
				};
				customMissionFilename.Text = od.FileName;
				continueBtn.IsEnabled = true;
			}
		}
	}
}
