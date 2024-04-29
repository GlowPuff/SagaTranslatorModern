using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for DeploymentGroupsPanel.xaml
	/// </summary>
	public partial class DeploymentGroupsPanel : PanelBase
	{
		private SourceData _sourceData;

		public DeploymentGroupsPanel( SourceData data )
		{
			InitializeComponent();

			commonPanelData = new CommonPanelData()
			{
				windowTitle = $"Saga Translator [{data.metaDisplay.displayName}] - NOT SAVED",
				infoText = $"Editing {data.fileName}",
				showLanguageSelector = false,
				fileName = data.fileName,
				translationName = data.metaDisplay.displayName
			};

			_sourceData = data;
			SetupUI();
		}

		void SetupUI()
		{
			//set the ENGLISH SOURCE
			sourceUI = JsonConvert.DeserializeObject<List<CardLanguage>>( _sourceData.stringifiedJsonData );
			//set the TRANSLATED UI
			translatedUI = JsonConvert.DeserializeObject<List<CardLanguage>>( _sourceData.stringifiedJsonData );

			//generate main list headings
			PopulateUIMainTree();
			(mainTree.ItemContainerGenerator.Items[0] as TreeViewItem).IsSelected = true;
		}

		private void CreateUI( int index, string heading )
		{
			englishPanel.Children.Clear();
			translatePanel.Children.Clear();

			englishPanel.Children.Add( UIFactory.Heading( $"English Source - {heading}" ) );
			translatePanel.Children.Add( UIFactory.Heading( $"Translation - {heading}" ) );

			CreateUI( index, sourceUI, englishPanel, false );
			CreateUI( index, translatedUI, translatePanel, true );
		}

		private void CreateUI( int index, List<CardLanguage> source, StackPanel panel, bool isEnabled )
		{
			panel.Children.Add( UIFactory.TBlock( "Name" ) );
			panel.Children.Add( UIFactory.TBox( source[index].name, "", false, isEnabled, ( a, b ) =>
			{
				source[index].name = (a as TextBox).Text.Trim();
			} ) );

			panel.Children.Add( UIFactory.TBlock( "Subname" ) );
			panel.Children.Add( UIFactory.TBox( source[index].subname, "", false, isEnabled, ( a, b ) =>
			{
				source[index].subname = (a as TextBox).Text.Trim();
			} ) );

			panel.Children.Add( UIFactory.TBlock( "Traits" ) );
			for ( int traitIdx = 0; traitIdx < source[index].traits?.Length; traitIdx++ )
			{
				panel.Children.Add( UIFactory.TBox( source[index].traits[traitIdx], traitIdx, false, isEnabled, ( a, b ) =>
				{
					int eidx = (int)(a as TextBox).DataContext;
					source[index].traits[eidx] = (a as TextBox).Text.Trim();
				} ) );
			}

			panel.Children.Add( UIFactory.TBlock( "Keywords" ) );
			for ( int keywordsIdx = 0; keywordsIdx < source[index].keywords?.Length; keywordsIdx++ )
			{
				panel.Children.Add( UIFactory.TBox( source[index].keywords[keywordsIdx], keywordsIdx, false, isEnabled, ( a, b ) =>
				{
					int eidx = (int)(a as TextBox).DataContext;
					source[index].keywords[eidx] = (a as TextBox).Text.Trim();
				} ) );
			}

			panel.Children.Add( UIFactory.TBlock( "Surges" ) );
			for ( int surgesIdx = 0; surgesIdx < source[index].surges?.Length; surgesIdx++ )
			{
				panel.Children.Add( UIFactory.TBox( source[index].surges[surgesIdx], surgesIdx, false, isEnabled, ( a, b ) =>
				{
					int eidx = (int)(a as TextBox).DataContext;
					source[index].surges[eidx] = (a as TextBox).Text.Trim();
				} ) );
			}

			for ( int abilityIdx = 0; abilityIdx < source[index].abilities?.Length; abilityIdx++ )
			{
				panel.Children.Add( UIFactory.TBlock( $"Ability {abilityIdx + 1}" ) );
				//name
				panel.Children.Add( UIFactory.TBox( source[index].abilities[abilityIdx].name, abilityIdx, false, isEnabled, ( a, b ) =>
				{
					int eidx = (int)(a as TextBox).DataContext;
					source[index].abilities[eidx].name = (a as TextBox).Text.Trim();
				} ) );
				//ability
				panel.Children.Add( UIFactory.TBox( source[index].abilities[abilityIdx].text, abilityIdx, true, isEnabled, ( a, b ) =>
				{
					int eidx = (int)(a as TextBox).DataContext;
					source[index].abilities[eidx].text = (a as TextBox).Text.Trim();
				} ) );
			}
		}

		public override void PopulateUIMainTree()
		{
			mainTree.Items.Clear();

			int idx = 0;
			foreach ( var item in sourceUI )
			{
				TreeViewItem uiItem = new TreeViewItem();
				uiItem.Header = $"{item.name} / {item.id.ToUpper()}";
				uiItem.DataContext = new DynamicContext() { arrayIndex = idx++, translationType = TranslationType.DeploymentCardText, dataContext = item };
				uiItem.Padding = new Thickness( 3, 3, 3, 3 );
				mainTree.Items.Add( uiItem );
			}
		}

		public override void OnOpenFile()
		{
			base.OnOpenFileCallback<List<CardLanguage>>( ( result ) =>
			{
				translatedUI = result;
				(mainTree.ItemContainerGenerator.Items[0] as TreeViewItem).IsSelected = true;
				CreateUI( 0, (mainTree.ItemContainerGenerator.Items[0] as TreeViewItem).Header as string );
			} );
		}

		private void mainTree_SelectedItemChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
		{
			if ( e.NewValue is TreeViewItem )
			{
				var dtx = (DynamicContext)(e.NewValue as TreeViewItem).DataContext;
				englishPanel.Children.Clear();
				translatePanel.Children.Clear();

				CreateUI( dtx.arrayIndex, (e.NewValue as TreeViewItem).Header.ToString() );
			}
		}

		public override FileOpResult OnVerifyOpenedFile( FileOpResult res )
		{
			if ( !res.stringifiedFile.Contains( "\"id\"" )
				|| !res.stringifiedFile.Contains( "\"name\"" )
				|| !res.stringifiedFile.Contains( "\"subname\"" )
				|| !res.stringifiedFile.Contains( "\"traits\"" )
				|| !res.stringifiedFile.Contains( "\"surges\"" )
				|| !res.stringifiedFile.Contains( "\"keywords\"" )
				|| !res.stringifiedFile.Contains( "\"abilities\"" )
				|| !res.stringifiedFile.Contains( "\"ignored\"" ) )
			{
				res.isSuccess = false;
				res.isError = true;
				res.exceptionMsg = "The file doesn't appear to be a Campaign Rewards translation.";
			}

			return res;
		}
	}
}
