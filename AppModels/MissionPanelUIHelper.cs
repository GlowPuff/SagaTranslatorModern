using System.Windows.Controls;
using Imperial_Commander_Editor;

namespace Saga_Translator_V2
{
	public static class MissionPanelUIHelper
	{
		public static void CreateM2( int eaIdx, TranslatedEvent dataSource, StackPanel panel, bool isEnabled, bool useContext )
		{
			var ea = dataSource.eventActions[eaIdx] as TranslatedModifyMapEntity;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.eaName} ({ea.translatedEntityProperties.Count})", Utils.missingTranslations.Contains( ea.GUID ) ) );

			for ( int eProps = 0; eProps < ea.translatedEntityProperties.Count; eProps++ )
			{
				StackPanel eStack = new StackPanel();
				eStack.Children.Add( UIFactory.SubHeading( $"Entity - {ea.translatedEntityProperties[eProps].entityName}" ) );
				eStack.Children.Add( UIFactory.TBlock( $"Entity Text" ) );
				//the text
				eStack.Children.Add( UIFactory.TBox( ea.translatedEntityProperties[eProps].theText, ea.translatedEntityProperties[eProps], true, isEnabled, ( a, b ) =>
				{
					if ( useContext )//use this to avoid English side getting its text updated
					{
						var x = (TranslatedEntityProperties)(a as TextBox).DataContext;
						x.theText = (a as TextBox).Text.Trim();
					}
				} ) );

				//buttons
				eStack.Children.Add( UIFactory.SubHeading( $"{ea.translatedEntityProperties[eProps].buttonList.Count} Button(s)" ) );
				for ( int buttonIdx = 0; buttonIdx < ea.translatedEntityProperties[eProps].buttonList.Count; buttonIdx++ )
				{
					var buttonList = ea.translatedEntityProperties[eProps].buttonList;
					eStack.Children.Add( UIFactory.TBox( buttonList[buttonIdx].theText, buttonList[buttonIdx], false, isEnabled, ( a, b ) =>
					{
						if ( useContext )//use this to avoid English side getting its text updated
						{
							var x = (TranslatedGUIDText)(a as TextBox).DataContext;
							x.theText = (a as TextBox).Text.Trim();
						}
					} ) );
				}
				stackPanel.Children.Add( UIFactory.InnerBorder( eStack ) );
			}

			panel.Children.Add( UIFactory.Border( stackPanel ) );
		}

		public static void CreateD1( int eaIdx, TranslatedEvent dataSource, StackPanel panel, bool isEnabled, bool useContext )
		{
			var ea = dataSource.eventActions[eaIdx] as TranslatedEnemyDeployment;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.eaName}", Utils.missingTranslations.Contains( ea.GUID ) ) );

			//enemyName
			stackPanel.Children.Add( UIFactory.TBlock( "Enemy Name" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.enemyName, ea, false, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					var x = (TranslatedEnemyDeployment)(a as TextBox).DataContext;
					x.enemyName = (a as TextBox).Text.Trim();
				}
			} ) );

			//customText
			stackPanel.Children.Add( UIFactory.TBlock( "Custom Instructions" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.customText, ea, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					var x = (TranslatedEnemyDeployment)(a as TextBox).DataContext;
					x.customText = (a as TextBox).Text.Trim();
				}
			} ) );

			//modification
			stackPanel.Children.Add( UIFactory.TBlock( "Modification" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.modification, ea, false, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					var x = (TranslatedEnemyDeployment)(a as TextBox).DataContext;
					x.modification = (a as TextBox).Text.Trim();
				}
			} ) );

			//repositionInstructions
			stackPanel.Children.Add( UIFactory.TBlock( "Reposition Instructions" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.repositionInstructions, ea, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					var x = (TranslatedEnemyDeployment)(a as TextBox).DataContext;
					x.repositionInstructions = (a as TextBox).Text.Trim();
				}
			} ) );

			panel.Children.Add( UIFactory.Border( stackPanel ) );
		}

		public static void CreateG9( int eaIdx, TranslatedEvent dataSource, StackPanel panel, bool isEnabled, bool useContext )
		{
			var ea = dataSource.eventActions[eaIdx] as TranslatedInputPrompt;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.eaName}", Utils.missingTranslations.Contains( ea.GUID ) ) );

			//mainText
			stackPanel.Children.Add( UIFactory.TBlock( "Main Text" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.mainText, null, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					ea.mainText = (a as TextBox).Text.Trim();
				}
			} ) );

			//failText
			stackPanel.Children.Add( UIFactory.TBlock( "Fail Text" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.failText, null, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					ea.failText = (a as TextBox).Text.Trim();
				}
			} ) );

			//inputList
			for ( int inputIdx = 0; inputIdx < ea.inputList.Count; inputIdx++ )
			{
				stackPanel.Children.Add( UIFactory.TBlock( $"Input Text [{inputIdx + 1}]" ) );
				stackPanel.Children.Add( UIFactory.TBox( ea.inputList[inputIdx].theText, ea.inputList[inputIdx], true, isEnabled, ( a, b ) =>
				{
					if ( useContext )
					{
						TranslatedGUIDText dtx = (TranslatedGUIDText)(a as TextBox).DataContext;
						dtx.theText = (a as TextBox).Text.Trim();
					}
				} ) );
			}

			panel.Children.Add( UIFactory.Border( stackPanel ) );
		}

		public static void CreateG7( int eaIdx, TranslatedEvent dataSource, StackPanel panel, bool isEnabled, bool useContext )
		{
			var ea = dataSource.eventActions[eaIdx] as TranslatedTextBox;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.eaName}", Utils.missingTranslations.Contains( ea.GUID ) ) );

			stackPanel.Children.Add( UIFactory.TBlock( "Main Text" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.tbText, null, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					ea.tbText = (a as TextBox).Text.Trim();
				}
			} ) );

			panel.Children.Add( UIFactory.Border( stackPanel ) );
		}

		public static void CreateG2( int eaIdx, TranslatedEvent dataSource, StackPanel panel, bool isEnabled, bool useContext )
		{
			var ea = dataSource.eventActions[eaIdx] as TranslatedChangeMissionInfo;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.eaName}", Utils.missingTranslations.Contains( ea.GUID ) ) );

			stackPanel.Children.Add( UIFactory.TBlock( "Main Text" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.theText, null, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					ea.theText = (a as TextBox).Text.Trim();
				}
			} ) );

			panel.Children.Add( UIFactory.Border( stackPanel ) );
		}

		public static void CreateG3( int eaIdx, TranslatedEvent dataSource, StackPanel panel, bool isEnabled, bool useContext )
		{
			var ea = dataSource.eventActions[eaIdx] as TranslatedChangeObjective;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.eaName}", Utils.missingTranslations.Contains( ea.GUID ) ) );

			stackPanel.Children.Add( UIFactory.TBlock( "Short Text" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.shortText, null, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					ea.shortText = (a as TextBox).Text.Trim();
				}
			} ) );

			stackPanel.Children.Add( UIFactory.TBlock( "Long Text" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.longText, null, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					ea.longText = (a as TextBox).Text.Trim();
				}
			} ) );

			panel.Children.Add( UIFactory.Border( stackPanel ) );
		}

		public static void CreateG6( int eaIdx, TranslatedEvent dataSource, StackPanel panel, bool isEnabled, bool useContext )
		{
			var ea = dataSource.eventActions[eaIdx] as TranslatedQuestionPrompt;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.eaName}", Utils.missingTranslations.Contains( ea.GUID ) ) );

			stackPanel.Children.Add( UIFactory.TBlock( "Main Text" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.mainText, null, false, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					ea.mainText = (a as TextBox).Text.Trim();
				}
			} ) );

			//buttons
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.buttonList.Count} Button(s)" ) );
			for ( int buttonIdx = 0; buttonIdx < ea.buttonList.Count; buttonIdx++ )
			{
				stackPanel.Children.Add( UIFactory.TBox( ea.buttonList[buttonIdx].theText, ea.buttonList[buttonIdx], false, isEnabled, ( a, b ) =>
				{
					if ( useContext )
					{
						var x = (TranslatedGUIDText)(a as TextBox).DataContext;
						x.theText = (a as TextBox).Text.Trim();
					}
				} ) );
			}

			panel.Children.Add( UIFactory.Border( stackPanel ) );
		}

		public static void CreateD2( int eaIdx, TranslatedEvent dataSource, StackPanel panel, bool isEnabled, bool useContext )
		{
			var ea = dataSource.eventActions[eaIdx] as TranslatedAllyDeployment;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.eaName}", Utils.missingTranslations.Contains( ea.GUID ) ) );

			stackPanel.Children.Add( UIFactory.TBlock( "Custom Name" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.customName, null, false, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					ea.customName = (a as TextBox).Text.Trim();
				}
			} ) );

			panel.Children.Add( UIFactory.Border( stackPanel ) );
		}

		public static void CreateGM1( int eaIdx, TranslatedEvent dataSource, StackPanel panel, bool isEnabled, bool useContext )
		{
			var ea = dataSource.eventActions[eaIdx] as TranslatedChangeGroupInstructions;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.eaName}", Utils.missingTranslations.Contains( ea.GUID ) ) );

			stackPanel.Children.Add( UIFactory.TBlock( "Instructions" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.newInstructions, null, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					ea.newInstructions = (a as TextBox).Text.Trim();
				}
			} ) );

			panel.Children.Add( UIFactory.Border( stackPanel ) );
		}

		public static void CreateGM4( int eaIdx, TranslatedEvent dataSource, StackPanel panel, bool isEnabled, bool useContext )
		{
			var ea = dataSource.eventActions[eaIdx] as TranslatedChangeRepositionInstructions;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.eaName}", Utils.missingTranslations.Contains( ea.GUID ) ) );

			stackPanel.Children.Add( UIFactory.TBlock( "Reposition Text" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.repositionText, null, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					ea.repositionText = (a as TextBox).Text.Trim();
				}
			} ) );

			panel.Children.Add( UIFactory.Border( stackPanel ) );
		}

		public static void CreateD6( int eaIdx, TranslatedEvent dataSource, StackPanel panel, bool isEnabled, bool useContext )
		{
			var ea = dataSource.eventActions[eaIdx] as TranslatedCustomEnemyDeployment;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add( UIFactory.SubHeading( $"{ea.eaName}", Utils.missingTranslations.Contains( ea.GUID ) ) );

			//cardName
			stackPanel.Children.Add( UIFactory.TBlock( "Enemy Name" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.cardName, ea, false, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					var x = (TranslatedCustomEnemyDeployment)(a as TextBox).DataContext;
					x.cardName = (a as TextBox).Text.Trim();
				}
			} ) );

			//customText
			stackPanel.Children.Add( UIFactory.TBlock( "Custom Instructions" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.customText, ea, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					var x = (TranslatedCustomEnemyDeployment)(a as TextBox).DataContext;
					x.customText = (a as TextBox).Text.Trim();
				}
			} ) );

			//abilities
			stackPanel.Children.Add( UIFactory.TBlock( "Abilities" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.abilities, ea, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					var x = (TranslatedCustomEnemyDeployment)(a as TextBox).DataContext;
					x.abilities = (a as TextBox).Text.Trim();
				}
			} ) );

			//keywords
			stackPanel.Children.Add( UIFactory.TBlock( "Keywords" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.keywords, ea, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					var x = (TranslatedCustomEnemyDeployment)(a as TextBox).DataContext;
					x.keywords = (a as TextBox).Text.Trim();
				}
			} ) );

			//bonuses
			stackPanel.Children.Add( UIFactory.TBlock( "Bonuses" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.bonuses, ea, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					var x = (TranslatedCustomEnemyDeployment)(a as TextBox).DataContext;
					x.bonuses = (a as TextBox).Text.Trim();
				}
			} ) );

			//surges
			stackPanel.Children.Add( UIFactory.TBlock( "Surges" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.surges, ea, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					var x = (TranslatedCustomEnemyDeployment)(a as TextBox).DataContext;
					x.surges = (a as TextBox).Text.Trim();
				}
			} ) );

			//repositionInstructions
			stackPanel.Children.Add( UIFactory.TBlock( "Reposition Instructions" ) );
			stackPanel.Children.Add( UIFactory.TBox( ea.repositionInstructions, ea, true, isEnabled, ( a, b ) =>
			{
				if ( useContext )
				{
					var x = (TranslatedCustomEnemyDeployment)(a as TextBox).DataContext;
					x.repositionInstructions = (a as TextBox).Text.Trim();
				}
			} ) );

			panel.Children.Add( UIFactory.Border( stackPanel ) );
		}
	}
}
