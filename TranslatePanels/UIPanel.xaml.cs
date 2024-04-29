using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Imperial_Commander_Editor;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for UIPanel.xaml
	/// </summary>
	public partial class UIPanel : PanelBase
	{
		public UIPanel()
		{
			InitializeComponent();

			commonPanelData = new()
			{
				windowTitle = "Saga Translator [User Interface] - NOT SAVED",
				infoText = "Editing ui.json",
				showLanguageSelector = true,
				fileName = "ui.json",
				translationName = "User Interface"
			};

			SetupUI<UILanguage>( mainTree );
		}

		public override void SetLanguage( string language )
		{
			translatedUI.languageID = language;
		}

		private void CreateUI( Type rootType, string propName, string heading )
		{
			englishPanel.Children.Clear();
			translatePanel.Children.Clear();

			//create UI for each English SOURCE property
			var props = rootType.GetProperties();
			var rootProp = sourceUI.GetType().GetProperty( propName ).GetValue( sourceUI );
			var transProp = translatedUI.GetType().GetProperty( propName ).GetValue( translatedUI );

			englishPanel.Children.Add( UIFactory.Heading( $"English Source - {heading}" ) );
			foreach ( var prop in props )
			{
				englishPanel.Children.Add( UIFactory.TBlock( prop.Name ) );
				englishPanel.Children.Add( UIFactory.TBox( prop.GetValue( rootProp ) as string, "", false, false ) );
			}
			//create UI for each TRANSLATED property
			translatePanel.Children.Add( UIFactory.Heading( $"Translation - {heading}" ) );
			foreach ( var prop in props )
			{
				translatePanel.Children.Add( UIFactory.TBlock( prop.Name, Utils.missingUITranslations.Contains( prop.Name.ToLower() ) ) );
				translatePanel.Children.Add( UIFactory.TBox( prop.GetValue( transProp ) as string, "", false, true, ( a, b ) =>
				{
					prop.SetValue( transProp, (a as TextBox).Text.Trim() );
				} ) );
			}
		}

		private void mainTree_SelectedItemChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
		{
			if ( e.NewValue is TreeViewItem )
			{
				englishPanel.Children.Clear();
				translatePanel.Children.Clear();

				if ( ((TreeViewItem)e.NewValue).DataContext is UITitle )
				{
					CreateUI( typeof( UITitle ), "uiTitle", ((TreeViewItem)e.NewValue).Header as string );
				}
				else if ( ((TreeViewItem)e.NewValue).DataContext is UISettings )
				{
					CreateUI( typeof( UISettings ), "uiSettings", ((TreeViewItem)e.NewValue).Header as string );
				}
				else if ( ((TreeViewItem)e.NewValue).DataContext is SagaUISetup )
				{
					CreateUI( typeof( SagaUISetup ), "sagaUISetup", ((TreeViewItem)e.NewValue).Header as string );
				}
				else if ( ((TreeViewItem)e.NewValue).DataContext is UIExpansions )
				{
					CreateUI( typeof( UIExpansions ), "uiExpansions", ((TreeViewItem)e.NewValue).Header as string );
				}
				else if ( ((TreeViewItem)e.NewValue).DataContext is SagaMainApp )
				{
					CreateUI( typeof( SagaMainApp ), "sagaMainApp", ((TreeViewItem)e.NewValue).Header as string );
				}
				else if ( ((TreeViewItem)e.NewValue).DataContext is UISetup )
				{
					CreateUI( typeof( UISetup ), "uiSetup", ((TreeViewItem)e.NewValue).Header as string );
				}
				else if ( ((TreeViewItem)e.NewValue).DataContext is UICampaign )
				{
					CreateUI( typeof( UICampaign ), "uiCampaign", ((TreeViewItem)e.NewValue).Header as string );
				}
				else if ( ((TreeViewItem)e.NewValue).DataContext is UIMainApp )
				{
					CreateUI( typeof( UIMainApp ), "uiMainApp", ((TreeViewItem)e.NewValue).Header as string );
				}
				else if ( ((TreeViewItem)e.NewValue).DataContext is UILogger )
				{
					CreateUI( typeof( UILogger ), "uiLogger", ((TreeViewItem)e.NewValue).Header as string );
				}
			}
		}

		public override void PopulateUIMainTree()
		{
			mainTree.Items.Clear();

			ValidateProperties();

			TreeViewItem uiItem = new TreeViewItem();
			uiItem.Header = "Settings";
			uiItem.DataContext = translatedUI.uiSettings;
			uiItem.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( uiItem );

			uiItem = new TreeViewItem();
			uiItem.Header = "Title Screen";
			uiItem.DataContext = translatedUI.uiTitle;
			uiItem.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( uiItem );

			uiItem = new TreeViewItem();
			uiItem.Header = "Saga Setup Screen";
			uiItem.DataContext = translatedUI.sagaUISetup;
			uiItem.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( uiItem );

			uiItem = new TreeViewItem();
			uiItem.Header = "Expansions";
			uiItem.DataContext = translatedUI.uiExpansions;
			uiItem.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( uiItem );

			uiItem = new TreeViewItem();
			uiItem.Header = "Main App";
			uiItem.DataContext = translatedUI.sagaMainApp;
			uiItem.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( uiItem );

			uiItem = new TreeViewItem();
			uiItem.Header = "Setup Screen";
			uiItem.DataContext = translatedUI.uiSetup;
			uiItem.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( uiItem );

			uiItem = new TreeViewItem();
			uiItem.Header = "Campaign Screen";
			uiItem.DataContext = translatedUI.uiCampaign;
			uiItem.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( uiItem );

			uiItem = new TreeViewItem();
			uiItem.Header = "Classic Main App";
			uiItem.DataContext = translatedUI.uiMainApp;
			uiItem.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( uiItem );

			uiItem = new TreeViewItem();
			uiItem.Header = "Mission Logger";
			uiItem.DataContext = translatedUI.uiLogger;
			uiItem.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( uiItem );
		}

		private void UpdateMainTree()
		{
			foreach ( TreeViewItem item in mainTree.Items )//root items
			{
				string header = item.DataContext as string;
				if ( Utils.missingUITranslations.Contains( item.DataContext.GetType().Name.ToLower() ) )
					item.Foreground = new SolidColorBrush( Colors.Red );
				else
					item.Foreground = new SolidColorBrush( Colors.White );
			}
		}

		/// <summary>
		/// If any translated model objects are null, copy the English data over
		/// </summary>
		private void ValidateProperties()
		{
			//first, validate the ROOT UILanguage objects
			if ( translatedUI.uiSettings == null )
				translatedUI.uiSettings = TranslatorUtils.UniqueCopy( sourceUI.uiSettings );

			if ( translatedUI.uiTitle == null )
				translatedUI.uiTitle = TranslatorUtils.UniqueCopy( sourceUI.uiTitle );

			if ( translatedUI.sagaUISetup == null )
				translatedUI.sagaUISetup = TranslatorUtils.UniqueCopy( sourceUI.sagaUISetup );

			if ( translatedUI.uiExpansions == null )
				translatedUI.uiExpansions = TranslatorUtils.UniqueCopy( sourceUI.uiExpansions );

			if ( translatedUI.sagaMainApp == null )
				translatedUI.sagaMainApp = TranslatorUtils.UniqueCopy( sourceUI.sagaMainApp );

			if ( translatedUI.uiSetup == null )
				translatedUI.uiSetup = TranslatorUtils.UniqueCopy( sourceUI.uiSetup );

			if ( translatedUI.uiCampaign == null )
				translatedUI.uiCampaign = TranslatorUtils.UniqueCopy( sourceUI.uiCampaign );

			if ( translatedUI.uiMainApp == null )
				translatedUI.uiMainApp = TranslatorUtils.UniqueCopy( sourceUI.uiMainApp );

			if ( translatedUI.uiLogger == null )
				translatedUI.uiLogger = TranslatorUtils.UniqueCopy( sourceUI.uiLogger );

			Utils.missingTranslations.Clear();

			//then validate all the PROPERTIES under the root objects
			TranslatorUtils.ValidateProperties( translatedUI.uiSettings, "uiSettings", sourceUI );
			TranslatorUtils.ValidateProperties( translatedUI.uiTitle, "uiTitle", sourceUI );
			TranslatorUtils.ValidateProperties( translatedUI.sagaUISetup, "sagaUISetup", sourceUI );
			TranslatorUtils.ValidateProperties( translatedUI.uiExpansions, "uiExpansions", sourceUI );
			TranslatorUtils.ValidateProperties( translatedUI.sagaMainApp, "sagaMainApp", sourceUI );
			TranslatorUtils.ValidateProperties( translatedUI.uiSetup, "uiSetup", sourceUI );
			TranslatorUtils.ValidateProperties( translatedUI.uiCampaign, "uiCampaign", sourceUI );
			TranslatorUtils.ValidateProperties( translatedUI.uiMainApp, "uiMainApp", sourceUI );
			TranslatorUtils.ValidateProperties( translatedUI.uiLogger, "uiLogger", sourceUI );
		}

		public override void OnOpenFile()
		{
			base.OnOpenFileCallback<UILanguage>( ( result ) =>
			{
				Utils.missingUITranslations.Clear();
				translatedUI = result;
				ValidateProperties();
				Utils.mainWindow.selectedLanguageID = translatedUI.languageID;
				(mainTree.ItemContainerGenerator.Items[0] as TreeViewItem).IsSelected = true;
				CreateUI( typeof( UISettings ), "uiSettings", "Settings" );

				//update the error colors on main tree
				UpdateMainTree();
			} );
		}

		public override FileOpResult OnVerifyOpenedFile( FileOpResult res )
		{
			if ( !res.stringifiedFile.Contains( "\"uiTitle\"" )
				|| !res.stringifiedFile.Contains( "\"uiSetup\"" )
				|| !res.stringifiedFile.Contains( "\"sagaUISetup\"" )
				|| !res.stringifiedFile.Contains( "\"uiMainApp\"" ) )
			{
				res.isSuccess = false;
				res.isError = true;
				res.exceptionMsg = "The file doesn't appear to be a UI translation.";
			}

			return res;
		}
	}
}
