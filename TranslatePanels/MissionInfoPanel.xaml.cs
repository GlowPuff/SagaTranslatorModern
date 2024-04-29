using System.Windows.Controls;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for MissionInfoPanel.xaml
	/// </summary>
	public partial class MissionInfoPanel : PanelBase
	{
		private SourceData _sourceData;

		public MissionInfoPanel( SourceData data )
		{
			InitializeComponent();

			commonPanelData = new CommonPanelData()
			{
				windowTitle = "Saga Translator [Mission Info] - NOT SAVED",
				infoText = $"Editing {data.fileName}",
				showLanguageSelector = false,
				fileName = data.fileName,
				translationName = "Mission Info"
			};

			_sourceData = data;

			SetupUI();
		}

		void SetupUI()
		{
			sourceUI = translatedUI = _sourceData.stringifiedJsonData;

			PopulateUIMainTree();
		}

		public override void PopulateUIMainTree()
		{
			englishPanel.Children.Clear();
			translatePanel.Children.Clear();

			englishPanel.Children.Add( UIFactory.Heading( $"English Source - {_sourceData.metaDisplay.displayName}" ) );
			translatePanel.Children.Add( UIFactory.Heading( $"Translation - {_sourceData.metaDisplay.displayName}" ) );

			englishPanel.Children.Add( UIFactory.TBox( sourceUI, "", true, false ) );
			translatePanel.Children.Add( UIFactory.TBox( translatedUI as string, "", true, true, ( a, b ) =>
			{
				translatedUI = (a as TextBox).Text.Trim();
			} ) );
		}

		public override void OnSaveFile()
		{
			OnSaveTextFile();
		}

		public override void OnOpenFile()
		{
			base.OnOpenTextFileCallback( ( result ) =>
			{
				translatedUI = result;
				translatePanel.Children.Clear();
				translatePanel.Children.Add( UIFactory.TBox( translatedUI as string, "", true, true, ( a, b ) =>
				{
					translatedUI = (a as TextBox).Text.Trim();
				} ) );
			} );
		}
	}
}
