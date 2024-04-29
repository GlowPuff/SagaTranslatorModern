using Newtonsoft.Json;
using Saga_Translator_V2.Converters;

namespace Imperial_Commander_Editor
{
	public interface ITranslatedEventAction
	{
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		string eaName { get; set; }
		List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false );
	}

	public class TranslatedEntityProperties
	{
		public string entityName { get; set; }
		public Guid GUID { get; set; }
		public string theText { get; set; } = "";
		public List<TranslatedGUIDText> buttonList { get; set; } = new();
		public TranslatedEntityProperties() { }
	}

	/// <summary>
	/// used for buttons, input prompt items
	/// </summary>
	public class TranslatedGUIDText
	{
		public Guid GUID { get; set; }
		public string theText { get; set; }
	}

	/// <summary>
	/// The main translation container for missions props, events, entities, initial groups
	/// </summary>
	public sealed class TranslatedMission
	{
		public string languageID;
		public TranslatedMissionProperties missionProperties { get; set; }
		public List<TranslatedEvent> events { get; set; } = new();
		public List<TranslatedMapEntity> mapEntities { get; set; } = new();
		public List<TranslatedInitialGroup> initialGroups { get; set; } = new();

		/// <summary>
		/// make this class only useable from within CreateTranslation()
		/// </summary>
		private TranslatedMission() { }
	}

	public class TranslatedMissionProperties
	{
		public string missionName { get; set; }
		public string missionDescription { get; set; }
		public string missionInfo { get; set; }
		public string campaignName { get; set; }
		public string startingObjective { get; set; }
		public string repositionOverride { get; set; }
		public string additionalMissionInfo { get; set; }

		public TranslatedMissionProperties()
		{

		}
	}

	public class TranslatedInitialGroup
	{
		public string cardName;
		public string customInstructions { get; set; }
	}

	public class TranslatedMapEntity
	{
		public string entityName { get; set; }
		public Guid GUID { get; set; }
		public string mainText { get; set; }
		public List<TranslatedGUIDText> buttonList { get; set; } = new();
	}

	public class TranslatedEvent
	{
		public string eventName { get; set; }
		public Guid GUID { get; set; }
		public string eventText { get; set; }
		[JsonConverter( typeof( TranslatedEventActionConverter ) )]
		public List<ITranslatedEventAction> eventActions { get; set; }

		public TranslatedEvent() { }
	}

	#region event action models
	public class TranslatedModifyMapEntity : ITranslatedEventAction//M2
	{
		public List<TranslatedEntityProperties> translatedEntityProperties { get; set; } = new();
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		public string eaName { get; set; }

		public TranslatedModifyMapEntity()
		{

		}

		public List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false )
		{
			var problems = new List<string>();

			//for loose validation, we can't check GUID
			if ( !useLooseValidation )
			{
				for ( int entityIdx = 0; entityIdx < (loadedEA as TranslatedModifyMapEntity).translatedEntityProperties.Count; entityIdx++ )
				{
					//check if each TranslatedEntityProperties exists in source
					var validEntity = translatedEntityProperties.Where( x => x.GUID == (loadedEA as TranslatedModifyMapEntity).translatedEntityProperties[entityIdx].GUID ).FirstOr( null );
					if ( validEntity != null )
					{
						//entity exists, set props
						var loadedEntity = (loadedEA as TranslatedModifyMapEntity).translatedEntityProperties[entityIdx];

						//check if value is translated
						if ( !string.IsNullOrEmpty( validEntity.theText ) && validEntity.theText == loadedEntity.theText )
							Utils.missingTranslations.Add( validEntity.GUID );
						//set text
						validEntity.theText = loadedEntity.theText;

						//check buttons
						for ( int buttonIdx = 0; buttonIdx < loadedEntity.buttonList.Count; buttonIdx++ )
						{
							//check if button exists
							var validButton = validEntity.buttonList.Where( x => x.GUID == loadedEntity.buttonList[buttonIdx].GUID ).FirstOr( null );
							if ( validButton != null )
							{
								//check if value is translated
								if ( !string.IsNullOrEmpty( validButton.theText ) && validButton.theText == loadedEntity.buttonList[buttonIdx].theText )
									Utils.missingTranslations.Add( validEntity.GUID );

								//button exists, set props
								validButton.theText = loadedEntity.buttonList[buttonIdx].theText;
							}
							else
								problems.Add( $"For loaded Event Action '{loadedEA.eaName}', Entity Button '{loadedEntity.buttonList[buttonIdx].GUID}' doesn't exist in the source data, ignored" );
						}
					}
					else
						problems.Add( $"For loaded Event Action '{loadedEA.eaName}', Entity '{(loadedEA as TranslatedModifyMapEntity).translatedEntityProperties[entityIdx].GUID}' doesn't exist in the source data, ignored" );
				}
			}
			else//loose validation
			{
				//when loading a converted translation, entities in this event action type will have a different GUID than expected, so just iterate each item, copy text and their buttons without matching first, and hope for the best
				for ( int entityIdx = 0; entityIdx < Math.Min( (loadedEA as TranslatedModifyMapEntity).translatedEntityProperties.Count, translatedEntityProperties.Count ); entityIdx++ )
				{
					var loadedEntity = (loadedEA as TranslatedModifyMapEntity).translatedEntityProperties[entityIdx];

					//check if value is translated
					if ( !string.IsNullOrEmpty( translatedEntityProperties[entityIdx].theText ) && translatedEntityProperties[entityIdx].theText == loadedEntity.theText )
						Utils.missingTranslations.Add( translatedEntityProperties[entityIdx].GUID );

					//set text
					translatedEntityProperties[entityIdx].theText = loadedEntity.theText;

					//copy button text over
					for ( int buttonIdx = 0; buttonIdx < Math.Min( loadedEntity.buttonList.Count, translatedEntityProperties[entityIdx].buttonList.Count ); buttonIdx++ )
					{
						//check if value is translated
						if ( !string.IsNullOrEmpty( translatedEntityProperties[entityIdx].buttonList[buttonIdx].theText ) && translatedEntityProperties[entityIdx].buttonList[buttonIdx].theText == loadedEntity.buttonList[buttonIdx].theText )
							Utils.missingTranslations.Add( translatedEntityProperties[entityIdx].GUID );

						//set button text
						translatedEntityProperties[entityIdx].buttonList[buttonIdx].theText = loadedEntity.buttonList[buttonIdx].theText;
					}
				}
			}

			return problems;
		}
	}

	public class TranslatedEnemyDeployment : ITranslatedEventAction//D1
	{
		//customText in the JSON is "custom instruction" in the model
		public string enemyName { get; set; }
		public string customText { get; set; }
		public string modification { get; set; }
		public string repositionInstructions { get; set; }
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		public string eaName { get; set; }

		public TranslatedEnemyDeployment()
		{

		}

		public List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false )
		{
			var problems = new List<string>();

			var props = typeof( TranslatedEnemyDeployment ).GetProperties();
			foreach ( var prop in props )
			{
				var transProp = typeof( TranslatedEnemyDeployment ).GetProperty( prop.Name ).GetValue( loadedEA );
				if ( transProp is string )
				{
					string p = (string)typeof( TranslatedEnemyDeployment ).GetProperty( prop.Name ).GetValue( this );
					//check if value is translated
					if ( !string.IsNullOrEmpty( p )
						&& prop.Name != "eaName"
						&& prop.Name != "modification"
						&& (string)transProp == p )
						Utils.missingTranslations.Add( loadedEA.GUID );

					typeof( TranslatedEnemyDeployment ).GetProperty( prop.Name ).SetValue( this, transProp );
				}
			}

			return problems;
		}
	}

	public class TranslatedInputPrompt : ITranslatedEventAction//G9
	{
		public string mainText { get; set; }
		public string failText { get; set; }
		public List<TranslatedGUIDText> inputList { get; set; } = new();
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		public string eaName { get; set; }

		public TranslatedInputPrompt()
		{

		}

		public List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false )
		{
			var problems = new List<string>();

			//check if value is translated
			if ( !string.IsNullOrEmpty( mainText ) && mainText == (loadedEA as TranslatedInputPrompt).mainText )
				Utils.missingTranslations.Add( loadedEA.GUID );
			if ( !string.IsNullOrEmpty( failText ) && failText == (loadedEA as TranslatedInputPrompt).failText )
				Utils.missingTranslations.Add( loadedEA.GUID );

			mainText = (loadedEA as TranslatedInputPrompt).mainText;
			failText = (loadedEA as TranslatedInputPrompt).failText;

			if ( !useLooseValidation )
			{
				for ( int i = 0; i < (loadedEA as TranslatedInputPrompt).inputList.Count; i++ )
				{
					//check if each inputList TranslatedGUIDText exists in source
					var validInput = inputList.Where( x => x.GUID == (loadedEA as TranslatedInputPrompt).inputList[i].GUID ).FirstOr( null );
					if ( validInput != null )
					{
						//input exists, set props
						var loadedInput = (loadedEA as TranslatedInputPrompt).inputList[i];

						//check if value is translated
						if ( !string.IsNullOrEmpty( validInput.theText ) && validInput.theText == loadedInput.theText )
							Utils.missingTranslations.Add( loadedEA.GUID );

						validInput.theText = loadedInput.theText;
					}
					else
						problems.Add( $"For loaded Event Action '{loadedEA.eaName}', Input Item '{(loadedEA as TranslatedInputPrompt).inputList[i].GUID}' doesn't exist in the source data, ignored" );
				}
			}
			else//loose validation
			{
				//when loading a converted translation, input items in this event action type will have a different GUID, so just iterate each item, copy text without matching first, and hope for the best
				for ( int i = 0; i < Math.Min( (loadedEA as TranslatedInputPrompt).inputList.Count, inputList.Count ); i++ )
				{
					//check if value is translated
					if ( !string.IsNullOrEmpty( inputList[i].theText ) && inputList[i].theText == (loadedEA as TranslatedInputPrompt).inputList[i].theText )
						Utils.missingTranslations.Add( loadedEA.GUID );

					inputList[i].theText = (loadedEA as TranslatedInputPrompt).inputList[i].theText;
				}
			}

			return problems;
		}
	}

	public class TranslatedTextBox : ITranslatedEventAction//G7
	{
		public string tbText { get; set; }
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		public string eaName { get; set; }

		public TranslatedTextBox()
		{

		}

		public List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false )
		{
			var problems = new List<string>();

			//check if value is translated
			if ( !string.IsNullOrEmpty( tbText ) && tbText == (loadedEA as TranslatedTextBox).tbText )
				Utils.missingTranslations.Add( loadedEA.GUID );

			tbText = (loadedEA as TranslatedTextBox).tbText;

			return problems;
		}
	}

	public class TranslatedChangeMissionInfo : ITranslatedEventAction//G2
	{
		public string theText { get; set; }
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		public string eaName { get; set; }

		public TranslatedChangeMissionInfo()
		{

		}

		public List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false )
		{
			var problems = new List<string>();

			//check if value is translated
			if ( !string.IsNullOrEmpty( theText ) && theText == (loadedEA as TranslatedChangeMissionInfo).theText )
				Utils.missingTranslations.Add( loadedEA.GUID );

			theText = (loadedEA as TranslatedChangeMissionInfo).theText;

			return problems;
		}
	}

	public class TranslatedChangeObjective : ITranslatedEventAction//G3
	{
		public string shortText { get; set; }
		public string longText { get; set; }
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		public string eaName { get; set; }

		public TranslatedChangeObjective()
		{

		}

		public List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false )
		{
			var problems = new List<string>();

			//check if value is translated
			if ( !string.IsNullOrEmpty( shortText ) && shortText == (loadedEA as TranslatedChangeObjective).shortText )
				Utils.missingTranslations.Add( loadedEA.GUID );
			if ( !string.IsNullOrEmpty( longText ) && longText == (loadedEA as TranslatedChangeObjective).longText )
				Utils.missingTranslations.Add( loadedEA.GUID );


			shortText = (loadedEA as TranslatedChangeObjective).shortText;
			longText = (loadedEA as TranslatedChangeObjective).longText;

			return problems;
		}
	}

	public class TranslatedQuestionPrompt : ITranslatedEventAction//G6
	{
		public string mainText { get; set; }
		public List<TranslatedGUIDText> buttonList { get; set; } = new();
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		public string eaName { get; set; }

		public TranslatedQuestionPrompt()
		{

		}

		public List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false )
		{
			var problems = new List<string>();

			//check if value is translated
			if ( !string.IsNullOrEmpty( mainText ) && mainText == (loadedEA as TranslatedQuestionPrompt).mainText )
				Utils.missingTranslations.Add( loadedEA.GUID );

			mainText = (loadedEA as TranslatedQuestionPrompt).mainText;

			if ( !useLooseValidation )
			{
				//check buttons
				for ( int buttonIdx = 0; buttonIdx < (loadedEA as TranslatedQuestionPrompt).buttonList.Count; buttonIdx++ )
				{
					//check if button exists
					var validButton = buttonList.Where( x => x.GUID == (loadedEA as TranslatedQuestionPrompt).buttonList[buttonIdx].GUID ).FirstOr( null );
					if ( validButton != null )
					{
						//check if value is translated
						if ( !string.IsNullOrEmpty( validButton.theText ) && validButton.theText == (loadedEA as TranslatedQuestionPrompt).buttonList[buttonIdx].theText )
							Utils.missingTranslations.Add( loadedEA.GUID );

						//button exists, set props
						validButton.theText = (loadedEA as TranslatedQuestionPrompt).buttonList[buttonIdx].theText;
					}
					else
						problems.Add( $"For loaded Event Action '{loadedEA.eaName}', Button '{(loadedEA as TranslatedQuestionPrompt).buttonList[buttonIdx].GUID}' doesn't exist in the source data, ignored" );
				}
			}
			else//loose validation
			{
				//when loading a converted translation, entities in this event action type will have a different GUID than expected, so just iterate each item, copy text and their buttons without matching first, and hope for the best
				for ( int buttonIdx = 0; buttonIdx < Math.Min( (loadedEA as TranslatedQuestionPrompt).buttonList.Count, buttonList.Count ); buttonIdx++ )
				{
					bool isDigit = int.TryParse( buttonList[buttonIdx].theText, out int number );
					//check if value is translated
					if ( !string.IsNullOrEmpty( buttonList[buttonIdx].theText )
						&& buttonList[buttonIdx].theText == (loadedEA as TranslatedQuestionPrompt).buttonList[buttonIdx].theText
						&& !isDigit )
						Utils.missingTranslations.Add( loadedEA.GUID );

					buttonList[buttonIdx].theText = (loadedEA as TranslatedQuestionPrompt).buttonList[buttonIdx].theText;
				}
			}

			return problems;
		}
	}

	public class TranslatedAllyDeployment : ITranslatedEventAction//D2
	{
		public string customName { get; set; }
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		public string eaName { get; set; }

		public TranslatedAllyDeployment()
		{

		}

		public List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false )
		{
			var problems = new List<string>();

			//check if value is translated
			if ( !string.IsNullOrEmpty( customName ) && customName == (loadedEA as TranslatedAllyDeployment).customName )
				Utils.missingTranslations.Add( loadedEA.GUID );

			customName = (loadedEA as TranslatedAllyDeployment).customName;

			return problems;
		}
	}

	public class TranslatedChangeGroupInstructions : ITranslatedEventAction//GM1
	{
		public string newInstructions { get; set; }
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		public string eaName { get; set; }

		public TranslatedChangeGroupInstructions()
		{

		}

		public List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false )
		{
			var problems = new List<string>();

			//check if value is translated
			if ( !string.IsNullOrEmpty( newInstructions ) && newInstructions == (loadedEA as TranslatedChangeGroupInstructions).newInstructions )
				Utils.missingTranslations.Add( loadedEA.GUID );

			newInstructions = (loadedEA as TranslatedChangeGroupInstructions).newInstructions;

			return problems;
		}
	}

	public class TranslatedChangeRepositionInstructions : ITranslatedEventAction//GM4
	{
		public string repositionText { get; set; }
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		public string eaName { get; set; }

		public TranslatedChangeRepositionInstructions()
		{

		}

		public List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false )
		{
			var problems = new List<string>();

			//check if value is translated
			if ( !string.IsNullOrEmpty( repositionText ) && repositionText == (loadedEA as TranslatedChangeRepositionInstructions).repositionText )
				Utils.missingTranslations.Add( loadedEA.GUID );

			repositionText = (loadedEA as TranslatedChangeRepositionInstructions).repositionText;

			return problems;
		}
	}

	public class TranslatedCustomEnemyDeployment : ITranslatedEventAction//D6
	{
		public Guid GUID { get; set; }
		public EventActionType eventActionType { get; set; }
		public string eaName { get; set; }

		//customText is custom instructions
		public string repositionInstructions { get; set; }
		public string surges { get; set; }
		public string bonuses { get; set; }
		public string keywords { get; set; }
		public string abilities { get; set; }
		public string customText { get; set; }
		public string cardName { get; set; }

		public TranslatedCustomEnemyDeployment()
		{

		}

		public List<string> Validate( ITranslatedEventAction loadedEA, bool useLooseValidation = false )
		{
			var problems = new List<string>();

			var props = typeof( TranslatedCustomEnemyDeployment ).GetProperties();
			foreach ( var prop in props )
			{
				var transProp = typeof( TranslatedCustomEnemyDeployment ).GetProperty( prop.Name ).GetValue( loadedEA );
				if ( transProp is string )
				{
					string p = (string)typeof( TranslatedCustomEnemyDeployment ).GetProperty( prop.Name ).GetValue( this );
					//check if value is translated
					if ( !string.IsNullOrEmpty( p ) && prop.Name != "eaName" && (string)transProp == p )
						Utils.missingTranslations.Add( loadedEA.GUID );

					typeof( TranslatedCustomEnemyDeployment ).GetProperty( prop.Name ).SetValue( this, transProp );
				}
			}

			return problems;
		}
	}
	#endregion
}
