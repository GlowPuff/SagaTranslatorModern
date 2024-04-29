using System.Windows;
using System.Windows.Controls;
using Imperial_Commander_Editor;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for BonusEffectsPanel.xaml
	/// </summary>
	public partial class BonusEffectsPanel : PanelBase
	{
		public BonusEffectsPanel()
		{
			InitializeComponent();

			commonPanelData = new CommonPanelData()
			{
				windowTitle = "Saga Translator [Bonus Effects] - NOT SAVED",
				infoText = "Editing bonuseffects.json",
				showLanguageSelector = false,
				fileName = "bonuseffects.json",
				translationName = "Bonus Effects"
			};

			SetupUI<List<BonusEffect>>( mainTree );
		}

		public override void PopulateUIMainTree()
		{
			mainTree.Items.Clear();

			int idx = 0;
			foreach ( var item in sourceUI )
			{
				if ( item.bonusID != "DG070" )
				{
					var enemy = Utils.enemyData.Where( x => x.id == item.bonusID ).FirstOr( null );
					if ( enemy != null )
					{
						TreeViewItem uiItem = new TreeViewItem();
						uiItem.Header = $"{item.bonusID} / {enemy.name}";
						uiItem.DataContext = new DynamicContext() { arrayIndex = idx++, translationType = TranslationType.BonusEffects, dataContext = item };
						uiItem.Padding = new Thickness( 3, 3, 3, 3 );
						mainTree.Items.Add( uiItem );
					}
				}
				else//dummy token
				{
					TreeViewItem uiItem = new TreeViewItem();
					uiItem.Header = $"{item.bonusID} / Dummy Token";
					uiItem.DataContext = new DynamicContext() { arrayIndex = idx++, translationType = TranslationType.BonusEffects, dataContext = item };
					uiItem.Padding = new Thickness( 3, 3, 3, 3 );
					mainTree.Items.Add( uiItem );
				}
			}
		}

		private void CreateUI( int index, string heading )
		{
			englishPanel.Children.Clear();
			translatePanel.Children.Clear();

			englishPanel.Children.Add( UIFactory.Heading( $"English Source - {heading}" ) );
			translatePanel.Children.Add( UIFactory.Heading( $"Translation - {heading}" ) );

			if ( sourceUI[index].effects.Count > 0 )
			{
				for ( int i = 0; i < sourceUI[index].effects.Count; i++ )
				{
					englishPanel.Children.Add( UIFactory.TBox( sourceUI[index].effects[i], "", true, false ) );
				}

				if ( translatedUI[index].effects.Count > 0 )
				{
					for ( int i = 0; i < translatedUI[index].effects.Count; i++ )
					{
						translatePanel.Children.Add( UIFactory.TBox( ((List<BonusEffect>)translatedUI)[index].effects[i], i, true, true, ( a, b ) =>
						{
							int eidx = (int)(a as TextBox).DataContext;
							translatedUI[index].effects[eidx] = (a as TextBox).Text.Trim();
						} ) );
					}
				}
			}
			else
			{
				englishPanel.Children.Add( UIFactory.TBlock( "No effects for this item" ) );
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

		public override void OnOpenFile()
		{
			base.OnOpenFileCallback<List<BonusEffect>>( ( result ) =>
			{
				translatedUI = result;
				(mainTree.ItemContainerGenerator.Items[0] as TreeViewItem).IsSelected = true;
				CreateUI( 0, (mainTree.ItemContainerGenerator.Items[0] as TreeViewItem).Header as string );
			} );
		}

		public override FileOpResult OnVerifyOpenedFile( FileOpResult res )
		{
			if ( !res.stringifiedFile.Contains( "\"bonusID\"" )
				|| !res.stringifiedFile.Contains( "\"effects\"" ) )
			{
				res.isSuccess = false;
				res.isError = true;
				res.exceptionMsg = "The file doesn't appear to be a Bonus Effects translation.";
			}

			return res;
		}
	}
}