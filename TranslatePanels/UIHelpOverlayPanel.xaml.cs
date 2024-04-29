using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Imperial_Commander_Editor;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for HelpOverlayPanel.xaml
	/// </summary>
	public partial class UIHelpOverlayPanel : PanelBase
	{
		//only mark missing in red after a file is loaded
		bool markMissing = false;

		public UIHelpOverlayPanel()
		{
			InitializeComponent();

			commonPanelData = new CommonPanelData()
			{
				windowTitle = "Saga Translator [Help Overlay] - NOT SAVED",
				infoText = "Editing help.json",
				showLanguageSelector = false,
				fileName = "help.json",
				translationName = "Help Overlay"
			};

			SetupUI<List<HelpOverlayPanel>>( mainTree );
		}

		public override void PopulateUIMainTree()
		{
			mainTree.Items.Clear();

			for ( int i = 0; i < (translatedUI as List<HelpOverlayPanel>).Count; i++ )
			{
				TreeViewItem uiItem = new TreeViewItem();
				uiItem.Header = translatedUI[i].panelHelpID;
				uiItem.DataContext = new DynamicContext() { arrayIndex = i, dataContext = translatedUI[i] };
				uiItem.Padding = new Thickness( 3, 3, 3, 3 );
				mainTree.Items.Add( uiItem );
			}
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

		private void CreateUI( int index, string heading )
		{
			englishPanel.Children.Clear();
			translatePanel.Children.Clear();

			englishPanel.Children.Add( UIFactory.Heading( $"English Source - {heading}" ) );
			translatePanel.Children.Add( UIFactory.Heading( $"Translation - {heading}" ) );

			CreateUI( (sourceUI as List<HelpOverlayPanel>)[index], englishPanel, false, (sourceUI as List<HelpOverlayPanel>)[index] );
			CreateUI( (translatedUI as List<HelpOverlayPanel>)[index], translatePanel, true, (sourceUI as List<HelpOverlayPanel>)[index] );
		}

		private void CreateUI( HelpOverlayPanel source, StackPanel panel, bool isEnabled, HelpOverlayPanel englishSource )
		{
			for ( int i = 0; i < source.helpItems.Length; i++ )
			{
				bool missing = markMissing && isEnabled && source.helpItems[i].helpText.ToLower() == englishSource.helpItems[i].helpText.ToLower();

				//only mark missing for translated items
				panel.Children.Add( UIFactory.TBlock( source.helpItems[i].id, missing ) );
				panel.Children.Add( UIFactory.TBox( source.helpItems[i].helpText, i, true, isEnabled, ( a, b ) =>
				{
					int eidx = (int)(a as TextBox).DataContext;
					source.helpItems[eidx].helpText = (a as TextBox).Text.Trim();
				} ) );
			}
		}

		void ValidateProperties()
		{
			var expectedProps = typeof( HelpOverlayPanel ).GetProperties();
			var sourcePanels = (sourceUI as List<HelpOverlayPanel>).Select( x => x.panelHelpID );
			var transPanels = (translatedUI as List<HelpOverlayPanel>).Select( x => x.panelHelpID );
			var topLevelMissing = sourcePanels.Where( x => !transPanels.Contains( x ) );
			//add missing top level panels


			//add missing panel items
			foreach ( var sPanel in sourceUI as List<HelpOverlayPanel> )
			{
				//add missing top level items
				if ( !(translatedUI as List<HelpOverlayPanel>).Any( x => x.panelHelpID == sPanel.panelHelpID ) )
				{
					Utils.Log( $"Top level Panel {sPanel.panelHelpID} not found" );
					int index = (sourceUI as List<HelpOverlayPanel>).IndexOf( sPanel );
					(translatedUI as List<HelpOverlayPanel>).Insert( index, sPanel );
					Utils.missingUITranslations.Add( sPanel.panelHelpID.ToLower() );
				}

				var tItems = (translatedUI as List<HelpOverlayPanel>).Where( x => x.panelHelpID == sPanel.panelHelpID ).SelectMany( x => x.helpItems );

				var tPanel = (translatedUI as List<HelpOverlayPanel>).Where( x => x.panelHelpID == sPanel.panelHelpID ).First();

				foreach ( var sItem in sPanel.helpItems )
				{
					//if the expected item doesn't exist in the translation, add it
					if ( !tItems.Any( x => x.id == sItem.id ) )
					{
						Utils.Log( $"Item {sItem.id} not found in {tPanel.panelHelpID}" );
						Utils.missingUITranslations.Add( tPanel.panelHelpID.ToLower() );

						var missing = (translatedUI as List<HelpOverlayPanel>).Where( x => x.panelHelpID == sPanel.panelHelpID ).First();
						missing.helpItems = missing.helpItems.Append( sItem ).ToArray();
					}
				}
			}
		}

		private void UpdateMainTree()
		{
			foreach ( TreeViewItem item in mainTree.Items )//root items
			{
				string header = item.DataContext as string;
				if ( Utils.missingUITranslations.Contains( item.Header.ToString().ToLower() ) )
					item.Foreground = new SolidColorBrush( Colors.Red );
				else
					item.Foreground = new SolidColorBrush( Colors.White );
			}
		}

		public override void OnOpenFile()
		{
			base.OnOpenFileCallback<List<HelpOverlayPanel>>( ( result ) =>
			{
				markMissing = true;
				Utils.missingUITranslations.Clear();
				translatedUI = result;
				ValidateProperties();
				(mainTree.ItemContainerGenerator.Items[0] as TreeViewItem).IsSelected = true;
				CreateUI( 0, (mainTree.ItemContainerGenerator.Items[0] as TreeViewItem).Header as string );

				//update the error colors on main tree
				UpdateMainTree();
			} );
		}

		public override FileOpResult OnVerifyOpenedFile( FileOpResult res )
		{
			if ( !res.stringifiedFile.Contains( "\"panelHelpID\"" )
				|| !res.stringifiedFile.Contains( "\"helpItems\"" ) )
			{
				res.isSuccess = false;
				res.isError = true;
				res.exceptionMsg = "The file doesn't appear to be a Help Overlay translation.";
			}

			return res;
		}
	}
}
