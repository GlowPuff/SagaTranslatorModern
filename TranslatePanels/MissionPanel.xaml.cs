using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Imperial_Commander_Editor;
using Newtonsoft.Json;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for MissionPanel.xaml
	/// </summary>
	public partial class MissionPanel : PanelBase
	{
		private SourceData _sourceData;

		public MissionPanel( SourceData data )
		{
			InitializeComponent();

			commonPanelData = new CommonPanelData()
			{
				windowTitle = $"Saga Translator [{data.metaDisplay.displayName}] - NOT SAVED",
				infoText = $"Editing {data.fileName}",
				showLanguageSelector = true,
				fileName = data.fileName,
				translationName = data.fileName
			};

			_sourceData = data;
			SetupUI();
		}

		public override void SetLanguage( string language )
		{
			translatedUI.languageID = language;
			commonPanelData.fileName =
				commonPanelData.fileName.Substring( 0, commonPanelData.fileName.IndexOf( '_' ) )
				+ "_"
				+ language.Substring( language.IndexOf( '(' ) + 1, 2 )
				+ ".json";
			commonPanelData.translationName = commonPanelData.fileName;
			commonPanelData.infoText = $"Editing {commonPanelData.fileName}";
		}

		public void SetupUI()
		{
			//set the ENGLISH SOURCE
			sourceUI = JsonConvert.DeserializeObject<TranslatedMission>( _sourceData.stringifiedJsonData );
			//set the TRANSLATED UI
			translatedUI = JsonConvert.DeserializeObject<TranslatedMission>( _sourceData.stringifiedJsonData );

			PopulateUIMainTree();
			(mainTree.ItemContainerGenerator.Items[0] as TreeViewItem).IsSelected = true;
		}

		public override void PopulateUIMainTree()
		{
			mainTree.Items.Clear();

			//mission props
			TreeViewItem propItems = new TreeViewItem();
			propItems.Header = "Mission Properties";
			propItems.DataContext = sourceUI.missionProperties;
			propItems.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( propItems );

			//events
			var mEvents = (List<TranslatedEvent>)sourceUI.events;
			TreeViewItem eventItems = new TreeViewItem();
			eventItems.Header = "Events";
			eventItems.DataContext = sourceUI.events as List<TranslatedEvent>;
			eventItems.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( eventItems );
			for ( int i = 0; i < mEvents.Count(); i++ )
			{
				TreeViewItem ev = new TreeViewItem();
				ev.Header = $"{mEvents[i].eventName}";
				ev.Padding = new( 3 );
				ev.DataContext = new DynamicContext()
				{
					arrayIndex = i,
					dataContext = mEvents[i],
					translatedDataContext = (TranslatedEvent)translatedUI.events[i],
					missionDataType = MissionDataType.Event
				};
				eventItems.Items.Add( ev );
			}

			//entities
			var mEntities = (List<TranslatedMapEntity>)sourceUI.mapEntities;
			TreeViewItem entityItems = new TreeViewItem();
			entityItems.Header = "Entities";
			entityItems.DataContext = sourceUI.events;
			entityItems.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( entityItems );
			for ( int i = 0; i < mEntities.Count(); i++ )
			{
				TreeViewItem ev = new TreeViewItem();
				ev.Header = $"{mEntities[i].entityName}";
				ev.Padding = new( 3 );
				ev.DataContext = new DynamicContext()
				{
					arrayIndex = i,
					dataContext = mEntities[i],
					translatedDataContext = (TranslatedMapEntity)translatedUI.mapEntities[i],
					missionDataType = MissionDataType.Entity
				};
				entityItems.Items.Add( ev );
			}

			//initial groups
			var mGroup = (List<TranslatedInitialGroup>)sourceUI.initialGroups;
			TreeViewItem initGroupItems = new TreeViewItem();
			initGroupItems.Header = "Initial Groups";
			initGroupItems.DataContext = sourceUI.events;
			initGroupItems.Padding = new Thickness( 3, 3, 3, 3 );
			mainTree.Items.Add( initGroupItems );
			for ( int i = 0; i < mGroup.Count(); i++ )
			{
				TreeViewItem ev = new TreeViewItem();
				ev.Header = $"{mGroup[i].cardName}";
				ev.Padding = new( 3 );
				ev.DataContext = new DynamicContext()
				{
					arrayIndex = i,
					dataContext = mGroup[i],
					translatedDataContext = (TranslatedInitialGroup)translatedUI.initialGroups[i],
					missionDataType = MissionDataType.InitialGroup
				};
				initGroupItems.Items.Add( ev );
			}
		}

		private void UpdateMainTree()
		{
			foreach ( TreeViewItem item in mainTree.Items )
			{
				if ( (string)item.Header == "Events" )
				{
					foreach ( TreeViewItem ev in item.Items )
					{
						var dyn = ((DynamicContext)ev.DataContext).dataContext as TranslatedEvent;
						//ev.DataContext is type DynamicContext with dataContext of type TranslatedEvent
						if ( Utils.missingTranslations.Contains( dyn.GUID ) )
						{
							ev.Foreground = new SolidColorBrush( Colors.Red );
						}
					}
				}
				if ( (string)item.Header == "Entities" )
				{
					foreach ( TreeViewItem ev in item.Items )
					{
						var dyn = ((DynamicContext)ev.DataContext).dataContext as TranslatedMapEntity;
						//ev.DataContext is type DynamicContext with dataContext of type TranslatedMapEntity
						if ( Utils.missingTranslations.Contains( dyn.GUID ) )
						{
							ev.Foreground = new SolidColorBrush( Colors.Red );
						}
					}
				}
			}
		}

		private void CreateMissionPropsUI( Type rootType, string propName, string heading )
		{
			CreateMissionPropsUI( sourceUI, englishPanel, false, rootType, propName, $"English Source - {heading}" );
			CreateMissionPropsUI( translatedUI, translatePanel, true, rootType, propName, $"Translation - {heading}" );
		}

		private void CreateMissionPropsUI( TranslatedMission dataSource, StackPanel panel, bool isEnabled, Type rootType, string propName, string heading )
		{
			panel.Children.Clear();

			//create UI for each property
			var props = rootType.GetProperties();
			var rootProp = dataSource.GetType().GetProperty( propName ).GetValue( dataSource );

			panel.Children.Add( UIFactory.Heading( heading ) );
			foreach ( var prop in props )
			{
				panel.Children.Add( UIFactory.TBlock( prop.Name ) );
				bool isMulti = prop.Name == "missionInfo"
					|| prop.Name == "missionDescription"
					|| prop.Name == "startingObjective"
					|| prop.Name == "repositionOverride"
					|| prop.Name == "additionalMissionInfo" ? true : false;
				panel.Children.Add( UIFactory.TBox( prop.GetValue( rootProp ) as string, "", isMulti, isEnabled, ( a, b ) =>
				{
					prop.SetValue( rootProp, (a as TextBox).Text.Trim() );
				} ) );
			}
		}

		void CreateEventUI( TranslatedEvent dataSource, StackPanel panel, bool isEnabled, string heading, bool useContext )
		{
			panel.Children.Add( UIFactory.Heading( heading ) );

			panel.Children.Add( UIFactory.TBlock( "Event Text" ) );
			panel.Children.Add( UIFactory.TBox( dataSource.eventText, "", true, isEnabled, ( a, b ) =>
			{
				dataSource.eventText = (a as TextBox).Text.Trim();
			} ) );

			panel.Children.Add( UIFactory.SubHeading( $"{dataSource.eventActions.Count} Event Action(s)" ) );
			for ( int eaIdx = 0; eaIdx < dataSource.eventActions.Count; eaIdx++ )
			{
				switch ( dataSource.eventActions[eaIdx].eventActionType )
				{
					case EventActionType.M2:
						MissionPanelUIHelper.CreateM2( eaIdx, dataSource, panel, isEnabled, useContext );
						break;
					case EventActionType.D1:
						MissionPanelUIHelper.CreateD1( eaIdx, dataSource, panel, isEnabled, useContext );
						break;
					case EventActionType.G9:
						MissionPanelUIHelper.CreateG9( eaIdx, dataSource, panel, isEnabled, useContext );
						break;
					case EventActionType.G7:
						MissionPanelUIHelper.CreateG7( eaIdx, dataSource, panel, isEnabled, useContext );
						break;
					case EventActionType.G2:
						MissionPanelUIHelper.CreateG2( eaIdx, dataSource, panel, isEnabled, useContext );
						break;
					case EventActionType.G3:
						MissionPanelUIHelper.CreateG3( eaIdx, dataSource, panel, isEnabled, useContext );
						break;
					case EventActionType.G6:
						MissionPanelUIHelper.CreateG6( eaIdx, dataSource, panel, isEnabled, useContext );
						break;
					case EventActionType.D2:
						MissionPanelUIHelper.CreateD2( eaIdx, dataSource, panel, isEnabled, useContext );
						break;
					case EventActionType.GM1:
						MissionPanelUIHelper.CreateGM1( eaIdx, dataSource, panel, isEnabled, useContext );
						break;
					case EventActionType.GM4:
						MissionPanelUIHelper.CreateGM4( eaIdx, dataSource, panel, isEnabled, useContext );
						break;
					case EventActionType.D6:
						MissionPanelUIHelper.CreateD6( eaIdx, dataSource, panel, isEnabled, useContext );
						break;
					case EventActionType.GM2:
						MissionPanelUIHelper.CreateGM2(eaIdx, dataSource, panel, isEnabled, useContext);
						break;
				}
			}
		}

		void CreateEntityUI( TranslatedMapEntity dataSource, StackPanel panel, bool isEnabled, string heading, bool useContext )
		{
			panel.Children.Add( UIFactory.Heading( heading ) );

			panel.Children.Add( UIFactory.TBlock( "Entity Text" ) );
			panel.Children.Add( UIFactory.TBox( dataSource.mainText, "", true, isEnabled, ( a, b ) =>
			{
				dataSource.mainText = (a as TextBox).Text.Trim();
			} ) );

			StackPanel stackPanel = new StackPanel();
			//buttons
			stackPanel.Children.Add( UIFactory.SubHeading( $"{dataSource.buttonList.Count} Button(s)" ) );
			for ( int buttonIdx = 0; buttonIdx < dataSource.buttonList.Count; buttonIdx++ )
			{
				stackPanel.Children.Add( UIFactory.TBox( dataSource.buttonList[buttonIdx].theText, dataSource.buttonList[buttonIdx], false, isEnabled, ( a, b ) =>
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

		void CreateGroupUI( TranslatedInitialGroup dataSource, StackPanel panel, bool isEnabled, string heading, bool useContext )
		{
			panel.Children.Add( UIFactory.Heading( heading ) );

			panel.Children.Add( UIFactory.TBlock( "Custom Instructions" ) );
			panel.Children.Add( UIFactory.TBox( dataSource.customInstructions, "", true, isEnabled, ( a, b ) =>
			{
				dataSource.customInstructions = (a as TextBox).Text.Trim();
			} ) );
		}

		public override FileOpResult OnVerifyOpenedFile( FileOpResult res )
		{
			//check if it's a Mission file
			if ( res.stringifiedFile.Contains( "timeTick" ) )
			{
				res.isSuccess = false;
				res.isError = true;
				res.exceptionMsg = "The file appears to be a Mission. Translator does not support importing Missions.";
			}
			else if ( !res.stringifiedFile.Contains( "\"missionProperties\"" )
				|| !res.stringifiedFile.Contains( "\"events\"" )
				|| !res.stringifiedFile.Contains( "\"mapEntities\"" )
				|| !res.stringifiedFile.Contains( "\"initialGroups\"" ) )
			{
				res.isSuccess = false;
				res.isError = true;
				res.exceptionMsg = "The file doesn't appear to be a Mission Translation.";
			}

			return res;
		}

		public override void OnOpenFile()
		{
			base.OnOpenFileCallback<TranslatedMission>( OnOpenFileCallback );
		}

		private void OnOpenFileCallback( TranslatedMission result )
		{
			try
			{
				//reset translatedUI to English source data 
				translatedUI = JsonConvert.DeserializeObject<TranslatedMission>( _sourceData.stringifiedJsonData );

				//reset datacontext on the tree view items
				PopulateUIMainTree();

				Utils.mainWindow.selectedLanguageID = result.languageID;
				SetLanguage( result.languageID );
				(mainTree.ItemContainerGenerator.Items[0] as TreeViewItem).IsSelected = true;

				//validate loaded data and assign it to translatedUI
				Validate( result, useLooseValidationCB.IsChecked.Value, useLooseEAValidationCB.IsChecked.Value );

				CreateMissionPropsUI( typeof( TranslatedMissionProperties ), "missionProperties", "Mission Properties" );

				//update the error colors on main tree
				UpdateMainTree();
			}
			catch ( Exception e )
			{
				Utils.ThrowErrorDialog( e, "There was an error loading the Mission", "Import Error" );
			}
		}

		private void mainTree_SelectedItemChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
		{
			if ( e.NewValue is TreeViewItem )
			{
				englishPanel.Children.Clear();
				translatePanel.Children.Clear();

				dynamic ctx = ((TreeViewItem)e.NewValue).DataContext;

				if ( ctx is TranslatedMissionProperties )
				{
					CreateMissionPropsUI( typeof( TranslatedMissionProperties ), "missionProperties", ((TreeViewItem)e.NewValue).Header as string );
				}
				else if ( ctx is DynamicContext )
				{
					switch ( ((DynamicContext)ctx).missionDataType )
					{
						case MissionDataType.Event:
							CreateEventUI( ((DynamicContext)ctx).translatedDataContext, translatePanel, true, $"Translation - Event", true );
							CreateEventUI( ((DynamicContext)ctx).dataContext, englishPanel, false, $"English Source - Event", false );
							break;
						case MissionDataType.Entity:
							CreateEntityUI( ((DynamicContext)ctx).translatedDataContext, translatePanel, true, $"Translation - Entity", true );
							CreateEntityUI( ((DynamicContext)ctx).dataContext, englishPanel, false, $"English Source - Entity", false );
							break;
						case MissionDataType.InitialGroup:
							CreateGroupUI( ((DynamicContext)ctx).translatedDataContext, translatePanel, true, $"Translation - Group", true );
							CreateGroupUI( ((DynamicContext)ctx).dataContext, englishPanel, false, $"English Source - Group", false );
							break;
					}
				}
			}
		}

		//validate loaded translation against the source
		void Validate( TranslatedMission loadedData, bool useLooseValidation, bool useLooseEventActionChecks )
		{
			Utils.missingTranslations.Clear();

			List<string> fixes = new();
			List<string> sourceGUIDS = CollectGUIDS( sourceUI );
			List<string> translationGUIDS = CollectGUIDS( loadedData );

			if ( sourceGUIDS.Count != translationGUIDS.Count )
				fixes.Add( $"Object count mismatch: Expected = {sourceGUIDS.Count}, Found = {translationGUIDS.Count}" );

			//only check for missing translations if the loaded language is different than the source language
			Utils.checkMissingTranslation = (translatedUI as TranslatedMission).languageID != (sourceUI as TranslatedMission).languageID;

			//just assign mission properties, no checks necessary
			(translatedUI as TranslatedMission).missionProperties = loadedData.missionProperties;

			//at this point, translatedUI = sourceUI (English source)
			//to get the translated data completely loaded into translatedUI, set property values into translatedUI one by one after they are validated from loadedData
			//missing properties in the loaded data are already set on translatedUI since it is equal to the English source, and English default values will be seen
			//extra properties found in loadedData that are NOT in the English source are ignored

			//groups
			//just do 1 to 1 cardName comparison by index, don't bother fixing out of order items
			//it either exists at the expected index or not
			for ( int i = 0; i < Math.Min( loadedData.initialGroups.Count, (sourceUI as TranslatedMission).initialGroups.Count ); i++ )
			{
				//only assign the string value if it exists in the source, otherwise ignore
				if ( (translatedUI as TranslatedMission).initialGroups[i].cardName == loadedData.initialGroups[i].cardName )
				{
					(translatedUI as TranslatedMission).initialGroups[i].customInstructions = loadedData.initialGroups[i].customInstructions;
				}
				else
				{
					fixes.Add( $"Initial Group Index {i}: Expected {(translatedUI as TranslatedMission).initialGroups[i].cardName}, found {loadedData.initialGroups[i].cardName} in the loaded data, ignored" );
				}
			}

			//entities
			//iterate loaded data, check if entity exists in the known good source
			loadedData.mapEntities.ForEach( loadedEntity =>
			{
				var validEntity = (translatedUI as TranslatedMission).mapEntities.Where( x => x.GUID == loadedEntity.GUID ).FirstOr( null );
				if ( validEntity != null )
				{
					//check if value is translated
					if ( !string.IsNullOrEmpty( validEntity.mainText )
					&& validEntity.mainText == loadedEntity.mainText
					&& Utils.checkMissingTranslation )
						Utils.missingTranslations.Add( validEntity.GUID );

					//the loaded entity exists in the source
					validEntity.mainText = loadedEntity.mainText;

					if ( !useLooseValidation )
					{
						//now check loaded buttons
						//only want to assign buttons that exist in the source, ignore any others as they are already filled with default English data
						foreach ( var loadedBtn in loadedEntity.buttonList )
						{
							if ( validEntity.buttonList.Any( x => x.GUID == loadedBtn.GUID ) )
							{
								//button does exist, assign its text to the source
								var validBtn = validEntity.buttonList.Where( x => x.GUID == loadedBtn.GUID ).First();

								//check if value is translated
								if ( !string.IsNullOrEmpty( validBtn.theText )
								&& validBtn.theText == loadedBtn.theText
								&& Utils.checkMissingTranslation )
									Utils.missingTranslations.Add( validEntity.GUID );

								validBtn.theText = loadedBtn.theText;
							}
							else
							{
								fixes.Add( $"Loaded Entity Button '{loadedBtn.GUID}' doesn't exist in the source data, ignored" );
							}
						}
					}
					else//loose validation
					{
						//button GUID will be different in imported translated data, so just iterate them and set their data into the source
						for ( int buttonIdx = 0; buttonIdx < Math.Min( loadedEntity.buttonList.Count, validEntity.buttonList.Count ); buttonIdx++ )
						{
							//check if value is translated
							if ( !string.IsNullOrEmpty( validEntity.buttonList[buttonIdx].theText )
							&& validEntity.buttonList[buttonIdx].theText == loadedEntity.buttonList[buttonIdx].theText
							&& Utils.checkMissingTranslation )
								Utils.missingTranslations.Add( validEntity.GUID );

							validEntity.buttonList[buttonIdx].theText = loadedEntity.buttonList[buttonIdx].theText;
						}
					}
				}
				else
				{
					fixes.Add( $"Loaded Entity '{loadedEntity.entityName}' doesn't exist in the source data, ignored" );
				}
			} );

			//events
			//iterate loaded data, check if event and its actions exist in the known good source
			loadedData.events.ForEach( loadedEvent =>
			{
				var validEvent = (translatedUI as TranslatedMission).events.Where( x => x.GUID == loadedEvent.GUID ).FirstOr( null );
				if ( validEvent != null )
				{
					//if translated value is the same as English source, mark it as untranslated
					if ( !string.IsNullOrEmpty( validEvent.eventText )
					&& validEvent.eventText == loadedEvent.eventText
					&& Utils.checkMissingTranslation )
					{
						Utils.missingTranslations.Add( validEvent.GUID );
						fixes.Add( $"Loaded Event '{validEvent.eventName}' doesn't have a translation" );
					}

					//the loaded event exists in the source
					validEvent.eventText = loadedEvent.eventText;
					//now check loaded event actions
					foreach ( var loadedEA in loadedEvent.eventActions )
					{
						var sourceEA = validEvent.eventActions.Where( x => x.GUID == loadedEA.GUID ).FirstOr( null );
						//fall back to event name - dangerous
						if ( useLooseEventActionChecks && sourceEA is null )
							sourceEA = validEvent.eventActions.Where( x => x.eaName == loadedEA.eaName ).FirstOr( null );

						//add problems found, IF ANY.  Returned List can be empty if none found
						if ( sourceEA != null )
						{
							fixes.AddRange( sourceEA.Validate( loadedEA, useLooseValidation ) );
							//if a missing translation was just found, add the owner's Event to the missing list
							if ( Utils.missingTranslations.Contains( sourceEA.GUID ) && Utils.checkMissingTranslation )
							{
								Utils.missingTranslations.Add( validEvent.GUID );
								Utils.missingTranslations.Add( sourceEA.GUID );
							}
						}
						else
						{
							fixes.Add( $"Loaded Event Action '{loadedEA.eaName}' in Event '{loadedEvent.eventName}' doesn't exist in the source data, ignored" );
							Utils.missingTranslations.Add( validEvent.GUID );
						}
					}
				}
				else
				{
					fixes.Add( $"Loaded Event '{loadedEvent.eventName}' doesn't exist in the source data, ignored" );
				}
			} );

			var loadedEAs = loadedData.events.SelectMany( x => x.eventActions ).ToList();
			(sourceUI as TranslatedMission).events.ForEach( x =>
				{
					if ( !loadedData.events.Any( y => y.GUID == x.GUID ) )
					{
						fixes.Add( $"Event '{x.eventName}' doesn't exist in the loaded data" );
						if ( !string.IsNullOrEmpty( x.eventText ) )
							Utils.missingTranslations.Add( x.GUID );
					}
					else//event does exist, now check missing EAs
					{
						var sourceEAs = x.eventActions;
						sourceEAs.ForEach( sea =>
						{
							if ( !loadedEAs.Any( y => y.GUID == sea.GUID ) )
							{
								fixes.Add( $"Event Action '{x.eventName}'->'{sea.eaName}' doesn't exist in the loaded data" );
								Utils.missingTranslations.Add( x.GUID );
								Utils.missingTranslations.Add( sea.GUID );
							}
						} );
					}
				}
			);

			if ( fixes.Count > 0 )
			{
				var probDlg = new ShowProblemsDialog( fixes );
				probDlg.ShowDialog();
			}
		}

		private List<string> CollectGUIDS( TranslatedMission dataSource )
		{
			List<string> identifiers = new();

			//entities
			foreach ( var item in (dataSource as TranslatedMission).mapEntities )
			{
				identifiers.Add( item.GUID.ToString() );
				foreach ( var btn in item.buttonList )
				{
					identifiers.Add( btn.GUID.ToString() );
				}
			}
			//groups, use the card name
			foreach ( var item in (dataSource as TranslatedMission).initialGroups )
			{
				identifiers.Add( item.cardName );
			}
			//events
			foreach ( var item in (dataSource as TranslatedMission).events )
			{
				//main event
				identifiers.Add( item.GUID.ToString() );
				//event actions
				foreach ( var ea in item.eventActions )
				{
					//event action guid
					identifiers.Add( ea.GUID.ToString() );
					//add any additional data for some action types
					switch ( ea.eventActionType )
					{
						case EventActionType.M2:
							(ea as TranslatedModifyMapEntity).translatedEntityProperties.ForEach( x =>
							{
								//entity guid
								identifiers.Add( x.GUID.ToString() );
								//buttons in the entity
								x.buttonList.ForEach( y => identifiers.Add( y.GUID.ToString() ) );
							} );
							break;
						case EventActionType.G9:
							(ea as TranslatedInputPrompt).inputList.ForEach( x => identifiers.Add( x.GUID.ToString() ) );
							break;
						case EventActionType.G6:
							(ea as TranslatedQuestionPrompt).buttonList.ForEach( x => identifiers.Add( x.GUID.ToString() ) );
							break;
					}
				}
			}

			return identifiers;
		}

		private void missionPanel_DragEnter( object sender, DragEventArgs e )
		{
			if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
			{
				string[] filename = e.Data.GetData( DataFormats.FileDrop ) as string[];
				if ( filename.Length == 1 && Path.GetExtension( filename[0] ) == ".json" )
					e.Effects = DragDropEffects.All;
			}
		}

		private void missionPanel_Drop( object sender, DragEventArgs e )
		{
			if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
			{
				//grab the filename
				string[] filename = e.Data.GetData( DataFormats.FileDrop ) as string[];
				if ( filename.Length == 1 )
				{
					var res = new FileOpResult();

					try
					{
						FileInfo fi = new( filename[0] );
						res.basePath = Path.GetDirectoryName( filename[0] );
						res.fileName = fi.Name;
						res.loadedObject = FileManager.LoadJSON<TranslatedMission>( filename[0] );
						res.stringifiedFile = File.ReadAllText( filename[0] );
						if ( res.loadedObject != null )
							res.isSuccess = true;
						else
							res = new() { isSuccess = false, isError = true, exceptionMsg = "HandleLoadFile()::Loaded object is null" };
					}
					catch ( Exception ex )
					{
						res = new() { isSuccess = false, isError = true, exceptionMsg = $"HandleLoadFile()::{ex.Message}" };
					}

					if ( res.isSuccess )
						res = OnVerifyOpenedFile( res );

					if ( res.isSuccess )
					{
						commonPanelData.hasSaved = true;
						commonPanelData.basePath = res.basePath;
						commonPanelData.fileName = res.fileName;
						commonPanelData.windowTitle = $"Saga Translator [{res.fileName}] - " + Path.Combine( commonPanelData.basePath, commonPanelData.fileName );
						OnOpenFileCallback( res.loadedObject );
						Utils.mainWindow.SetStatus( "File Loaded" );
					}
					else if ( res.isError )
					{
						Utils.ShowError( $"There was a problem opening the file.\nException:\n{res.exceptionMsg}", "Error Opening File" );
						Utils.mainWindow.SetStatus( "File Not Loaded" );
					}
				}
			}
		}
	}
}
