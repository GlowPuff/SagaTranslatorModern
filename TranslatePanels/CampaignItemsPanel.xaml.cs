using System.Windows;
using System.Windows.Controls;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for CampaignItemsPanel.xaml
	/// </summary>
	public partial class CampaignItemsPanel : PanelBase
	{
		public CampaignItemsPanel()
		{
			InitializeComponent();

			commonPanelData = new CommonPanelData()
			{
				windowTitle = "Saga Translator [Campaign Items] - NOT SAVED",
				infoText = "Editing items.json",
				showLanguageSelector = false,
				fileName = "items.json",
				translationName = "Campaign Items"
			};

			SetupUI<List<CampaignItem>>( mainTree );
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

		private void CreateUI( int index, List<CampaignItem> source, StackPanel panel, bool isEnabled )
		{
			panel.Children.Add( UIFactory.TBlock( "Name" ) );
			panel.Children.Add( UIFactory.TBox( source[index].name, "", false, isEnabled, ( a, b ) =>
			{
				source[index].name = (a as TextBox).Text.Trim();
			} ) );
		}

		public override void PopulateUIMainTree()
		{
			mainTree.Items.Clear();

			int idx = 0;
			foreach ( var item in sourceUI )
			{
				TreeViewItem uiItem = new TreeViewItem();
				uiItem.Header = $"{item.name} / {item.id.ToUpper()}";
				uiItem.DataContext = new DynamicContext() { arrayIndex = idx++, translationType = TranslationType.CampaignItems, dataContext = item };
				uiItem.Padding = new Thickness( 3, 3, 3, 3 );
				mainTree.Items.Add( uiItem );
			}
		}

		public override void OnOpenFile()
		{
			base.OnOpenFileCallback<List<CampaignItem>>( ( result ) =>
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
			if ( !res.stringifiedFile.Contains( "\"tier\"" )
				|| !res.stringifiedFile.Contains( "\"id\"" )
				|| !res.stringifiedFile.Contains( "\"name\"" )
				|| !res.stringifiedFile.Contains( "\"type\"" )
				|| !res.stringifiedFile.Contains( "\"cost\"" ) )
			{
				res.isSuccess = false;
				res.isError = true;
				res.exceptionMsg = "The file doesn't appear to be a Campaign Items translation.";
			}

			return res;
		}
	}
}
