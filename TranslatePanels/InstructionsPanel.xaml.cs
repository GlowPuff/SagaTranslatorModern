using System.Windows;
using System.Windows.Controls;
using Imperial_Commander_Editor;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for InstructionsPanel.xaml
	/// </summary>
	public partial class InstructionsPanel : PanelBase
	{
		public InstructionsPanel()
		{
			InitializeComponent();

			commonPanelData = new CommonPanelData()
			{
				windowTitle = "Saga Translator [Enemy Instructions] - NOT SAVED",
				infoText = "Editing instructions.json",
				showLanguageSelector = false,
				fileName = "instructions.json",
				translationName = "Enemy Instructions"
			};

			SetupUI<List<CardInstruction>>( mainTree );
		}

		private void CreateUI( int index, string heading )
		{
			englishPanel.Children.Clear();
			translatePanel.Children.Clear();

			englishPanel.Children.Add( UIFactory.Heading( $"English Source - {heading}" ) );
			translatePanel.Children.Add( UIFactory.Heading( $"Translation - {heading}" ) );

			CreateUI( sourceUI, index, englishPanel, false );
			CreateUI( translatedUI, index, translatePanel, true );
		}

		private void CreateUI( List<CardInstruction> source, int index, StackPanel panel, bool isEnabled )
		{
			panel.Children.Add( UIFactory.TBlock( "Name" ) );
			panel.Children.Add( UIFactory.TBox( source[index].instName, "", false, isEnabled, ( a, b ) =>
			{
				source[index].instName = (a as TextBox).Text.Trim();
			} ) );

			for ( int contentIdx = 0; contentIdx < source[index].content.Count; contentIdx++ )
			{
				StackPanel stackPanel = new StackPanel();

				stackPanel.Children.Add( UIFactory.TBlock( $"Instruction Group {contentIdx + 1}" ) );
				for ( int instIdx = 0; instIdx < source[index].content[contentIdx].instruction.Count; instIdx++ )
				{
					stackPanel.Children.Add( UIFactory.TBox( source[index].content[contentIdx].instruction[instIdx], new int[] { contentIdx, instIdx }, true, isEnabled, ( a, b ) =>
					{
						int[] eidx = (int[])(a as TextBox).DataContext;
						source[index].content[eidx[0]].instruction[eidx[1]] = (a as TextBox).Text.Trim();
					} ) );
				}
				panel.Children.Add( UIFactory.Border( stackPanel ) );
			}
		}

		public override void PopulateUIMainTree()
		{
			mainTree.Items.Clear();

			int idx = 0;
			foreach ( var item in sourceUI )
			{
				TreeViewItem uiItem = new TreeViewItem();
				uiItem.Header = $"{item.instName}";
				uiItem.DataContext = new DynamicContext() { arrayIndex = idx++, translationType = TranslationType.EnemyInstructions, dataContext = item };
				uiItem.Padding = new Thickness( 3, 3, 3, 3 );
				mainTree.Items.Add( uiItem );
			}
		}

		public override void OnOpenFile()
		{
			base.OnOpenFileCallback<List<CardInstruction>>( ( result ) =>
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
			if ( !res.stringifiedFile.Contains( "\"instName\"" )
				|| !res.stringifiedFile.Contains( "\"instID\"" ) )
			{
				res.isSuccess = false;
				res.isError = true;
				res.exceptionMsg = "The file doesn't appear to be an Instructions translation.";
			}

			return res;
		}
	}
}
