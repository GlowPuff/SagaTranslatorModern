using System.Windows;
using System.Windows.Controls;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for MissionCardTextPanel.xaml
	/// </summary>
	public partial class MissionCardTextPanel : PanelBase
	{

		public MissionCardTextPanel( SourceData data )
		{
			InitializeComponent();

			commonPanelData = new CommonPanelData()
			{
				windowTitle = "Saga Translator [Mission Card Text] - NOT SAVED",
				infoText = $"Editing {data.fileName}",
				showLanguageSelector = false,
				fileName = data.fileName,
				translationName = "Mission Card Text"
			};

			SetupUI<List<MissionCardText>>( mainTree );
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

		private void CreateUI( int index, List<MissionCardText> source, StackPanel panel, bool isEnabled )
		{
			panel.Children.Add( UIFactory.TBlock( "Name" ) );
			panel.Children.Add( UIFactory.TBox( source[index].name, "", false, isEnabled, ( a, b ) =>
			{
				source[index].name = (a as TextBox).Text.Trim();
			} ) );

			panel.Children.Add( UIFactory.TBlock( "Description" ) );
			panel.Children.Add( UIFactory.TBox( source[index].descriptionText, "", true, isEnabled, ( a, b ) =>
			{
				source[index].descriptionText = (a as TextBox).Text.Trim();
			} ) );

			panel.Children.Add( UIFactory.TBlock( "Bonus Text" ) );
			panel.Children.Add( UIFactory.TBox( source[index].bonusText, "", false, isEnabled, ( a, b ) =>
			{
				source[index].bonusText = (a as TextBox).Text.Trim();
			} ) );

			panel.Children.Add( UIFactory.TBlock( "Hero Text" ) );
			panel.Children.Add( UIFactory.TBox( source[index].heroText, "", false, isEnabled, ( a, b ) =>
			{
				source[index].heroText = (a as TextBox).Text.Trim();
			} ) );

			panel.Children.Add( UIFactory.TBlock( "Ally Text" ) );
			panel.Children.Add( UIFactory.TBox( source[index].allyText, "", false, isEnabled, ( a, b ) =>
			{
				source[index].allyText = (a as TextBox).Text.Trim();
			} ) );

			panel.Children.Add( UIFactory.TBlock( "Villain Text" ) );
			panel.Children.Add( UIFactory.TBox( source[index].villainText, "", false, isEnabled, ( a, b ) =>
			{
				source[index].villainText = (a as TextBox).Text.Trim();
			} ) );

			panel.Children.Add( UIFactory.TBlock( "Tags" ) );
			for ( int tagsIdx = 0; tagsIdx < source[index].tagsText.Length; tagsIdx++ )
			{
				panel.Children.Add( UIFactory.TBox( source[index].tagsText[tagsIdx], tagsIdx, false, isEnabled, ( a, b ) =>
				{
					int eidx = (int)(a as TextBox).DataContext;
					source[index].tagsText[eidx] = (a as TextBox).Text.Trim();
				} ) );
			}

			panel.Children.Add( UIFactory.TBlock( "Expansion Text" ) );
			panel.Children.Add( UIFactory.TBox( source[index].expansionText, "", false, isEnabled, ( a, b ) =>
			{
				source[index].expansionText = (a as TextBox).Text.Trim();
			} ) );

			panel.Children.Add( UIFactory.TBlock( "Rebel Reward Text" ) );
			panel.Children.Add( UIFactory.TBox( source[index].rebelRewardText, "", false, isEnabled, ( a, b ) =>
			{
				source[index].rebelRewardText = (a as TextBox).Text.Trim();
			} ) );

			panel.Children.Add( UIFactory.TBlock( "Imperial Reward Text" ) );
			panel.Children.Add( UIFactory.TBox( source[index].imperialRewardText, "", false, isEnabled, ( a, b ) =>
			{
				source[index].imperialRewardText = (a as TextBox).Text.Trim();
			} ) );
		}

		public override void PopulateUIMainTree()
		{
			mainTree.Items.Clear();

			int idx = 0;
			foreach ( var item in sourceUI )
			{
				TreeViewItem uiItem = new TreeViewItem();
				uiItem.Header = $"{item.id.ToUpper()}";
				uiItem.DataContext = new DynamicContext() { arrayIndex = idx++, translationType = TranslationType.MissionCardText, dataContext = item };
				uiItem.Padding = new Thickness( 3, 3, 3, 3 );
				mainTree.Items.Add( uiItem );
			}
		}

		public override void OnOpenFile()
		{
			base.OnOpenFileCallback<List<MissionCardText>>( ( result ) =>
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
				|| !res.stringifiedFile.Contains( "\"descriptionText\"" )
				|| !res.stringifiedFile.Contains( "\"bonusText\"" )
				|| !res.stringifiedFile.Contains( "\"heroText\"" )
				|| !res.stringifiedFile.Contains( "\"allyText\"" )
				|| !res.stringifiedFile.Contains( "\"villainText\"" )
				|| !res.stringifiedFile.Contains( "\"expansionText\"" )
				|| !res.stringifiedFile.Contains( "\"rebelRewardText\"" )
				|| !res.stringifiedFile.Contains( "\"imperialRewardText\"" )
				|| !res.stringifiedFile.Contains( "\"tagsText\"" ) )
			{
				res.isSuccess = false;
				res.isError = true;
				res.exceptionMsg = "The file doesn't appear to be a Campaign Rewards translation.";
			}

			return res;
		}
	}
}
