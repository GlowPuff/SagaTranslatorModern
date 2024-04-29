using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Imperial_Commander_Editor;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Saga_Translator_V2
{
	//ENUMS
	public enum TranslationType { UI, BonusEffects, EnemyInstructions, CampaignItems, CampaignRewards, CampaignSkills, CampaignInfo, DeploymentCardText, MissionCardText, MissionInfoRules, Mission, Tutorials, HelpOverlays }
	public enum MissionDataType { Event, Entity, InitialGroup }

	//QUICK TEXTBOX GENERATOR
	public static class UIFactory
	{
		public static TextBlock Heading( string text )
		{
			return new()
			{
				Text = text,
				Margin = new( 0, 10, 0, 5 ),
				FontSize = 16,
				FontWeight = FontWeights.Bold,
				Foreground = new SolidColorBrush( Colors.White ),
				TextWrapping = TextWrapping.Wrap,
			};
		}

		public static TextBlock SubHeading( string text, bool flagRed = false )
		{
			//override if we're not checking for a missing translation
			//if ( !Utils.checkMissingTranslation )
			//	flagRed = false;

			return new()
			{
				Text = text,
				Margin = new( 0, 10, 0, 5 ),
				FontSize = 14,
				FontWeight = FontWeights.Bold,
				Foreground = flagRed ? new SolidColorBrush( Colors.Red ) : new SolidColorBrush( Colors.White ),
				TextWrapping = TextWrapping.Wrap,
			};
		}

		public static Border Border( StackPanel stackPanel )
		{
			var border = new Border()
			{
				Padding = new( 5 ),
				BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom( "#808aab" ),
				BorderThickness = new( 1 ),
				Background = (SolidColorBrush)new BrushConverter().ConvertFrom( "#333140" ),
				CornerRadius = new( 3 ),
				Margin = new( 0, 0, 0, 10 )
			};

			border.Child = stackPanel;

			return border;
		}

		public static Border InnerBorder( StackPanel stackPanel )
		{
			var border = new Border()
			{
				Padding = new( 5 ),
				BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom( "#808aab" ),
				BorderThickness = new( 1 ),
				Background = (SolidColorBrush)new BrushConverter().ConvertFrom( "#596077" ),
				CornerRadius = new( 3 ),
				Margin = new( 5 )
			};

			border.Child = stackPanel;

			return border;
		}

		public static TextBlock TBlock( string title, bool flagRed = false )
		{
			//override if we're not checking for a missing translation
			//if ( Utils.checkMissingTranslation )
			//	flagRed = false;

			return new()
			{
				Text = title,
				FontSize = 12,
				Margin = new( 0, 10, 0, 5 ),
				Foreground = flagRed ? new SolidColorBrush( Colors.Red ) : new SolidColorBrush( Colors.White ),
				TextWrapping = TextWrapping.Wrap,
			};
		}

		public static TextBox TBox( string text, object dtx, bool isMulti, bool enabled, TextChangedEventHandler onChanged = null )
		{
			var tb = new TextBox()
			{
				Text = text,
				FontSize = 14,
				BorderThickness = new( 2 ),
				Padding = new Thickness( 5 ),
				IsEnabled = enabled,
				DataContext = dtx,
				Margin = new( 0, 0, 0, 10 )
			};

			tb.GotFocus += ( s, e ) => { (s as TextBox).SelectAll(); };

			if ( onChanged != null )
			{
				tb.TextChanged += onChanged;
			}

			if ( isMulti )
				tb.Style = Application.Current.FindResource( "multi" ) as Style;

			return tb;
		}
	}

	//SUPPORT CLASSES
	public class MissionNameData
	{
		public string id { get; set; }
		public string name { get; set; }
	}

	public class SourceData
	{
		public string stringifiedJsonData;
		public ComboBoxMeta metaDisplay;
		public string fileName
		{
			get
			{
				var split = metaDisplay.assetName.Split( '.' ).Reverse().Take( 2 ).Reverse().ToArray();
				return $"{split[0]}.{split[1]}";
			}
		}

		public SourceData()
		{
			stringifiedJsonData = string.Empty;
			metaDisplay = new();
		}
	}

	public class ComboBoxMeta
	{
		public string displayName { get; set; }
		public string comboBoxTitle { get; set; }
		public string assetName { get; set; }

		public ComboBoxMeta( string dname = "", string aname = "" )
		{
			displayName = dname;
			assetName = aname;
			comboBoxTitle = displayName;
		}
	}

	public class FileOpResult
	{
		public bool isSuccess = false;
		public bool isError = false;
		public string basePath, fileName, exceptionMsg;
		public dynamic loadedObject;
		//public bool isMission = false;
		public string stringifiedFile = "";
	}

	public class DynamicContext
	{
		public int arrayIndex;
		public dynamic dataContext;
		public dynamic translatedDataContext;
		public TranslationType translationType;
		public MissionDataType missionDataType;
	}

	//UTILITIES
	public static class TranslatorUtils
	{
		/// <summary>
		/// Handle SaveFileDialog for regular text
		/// </summary>
		public static FileOpResult HandleSaveTextFile( string data, string fileName, string filter, string title, string basePath )
		{
			string outpath = Path.Combine( basePath, fileName );

			try
			{
				SaveFileDialog od = new SaveFileDialog();
				od.AddExtension = true;
				od.InitialDirectory = basePath;
				od.Filter = filter;
				od.Title = title;
				od.FileName = fileName;

				if ( od.ShowDialog() == true )
				{
					var res = new FileOpResult();
					res.basePath = Path.GetDirectoryName( od.FileName );
					res.fileName = od.SafeFileName;

					if ( FileManager.SaveText( data, res.fileName, res.basePath ) )
					{
						res.isSuccess = true;
						return res;
					}
				}

				return new() { isSuccess = false };
			}
			catch ( Exception e )
			{
				MessageBox.Show( "Could not save the file.\r\n\r\nException:\r\n" + e.Message, "App Exception", MessageBoxButton.OK, MessageBoxImage.Error );
				return new() { isSuccess = false, exceptionMsg = e.Message };
			}
		}

		/// <summary>
		/// Handle SaveFileDialog
		/// </summary>
		/// <returns></returns>
		public static FileOpResult HandleSaveFile<T>( T objectToSave, string fileName, string filter, string title, string basePath )
		{
			try
			{
				SaveFileDialog od = new SaveFileDialog();
				od.AddExtension = true;
				od.InitialDirectory = basePath;
				od.Filter = filter;
				od.Title = title;
				od.FileName = fileName;
				if ( od.ShowDialog() == true )
				{
					var res = new FileOpResult();
					res.basePath = Path.GetDirectoryName( od.FileName );
					res.fileName = od.SafeFileName;

					if ( FileManager.SaveJSON( objectToSave, res.fileName, res.basePath ) )
					{
						res.isSuccess = true;
						return res;
					}
				}

				return new() { isSuccess = false };
			}
			catch ( Exception e )
			{
				return new() { isSuccess = false, exceptionMsg = e.Message };
			}
		}

		public static FileOpResult HandleLoadFile<T>( string filename, string filter, string title, string basePath )
		{
			try
			{
				OpenFileDialog od = new();
				od.InitialDirectory = basePath;
				od.FileName = filename;
				od.Filter = filter;
				od.Title = title;
				if ( od.ShowDialog() == true )
				{
					var res = new FileOpResult();
					res.basePath = Path.GetDirectoryName( od.FileName );
					res.fileName = od.SafeFileName;
					res.loadedObject = FileManager.LoadJSON<T>( od.FileName );

					//if opening a translation, check if it's a whole Mission
					//string content = File.ReadAllText( od.FileName );
					//if ( content.Contains( "timeTick" ) )
					//	res.isMission = true;
					res.stringifiedFile = File.ReadAllText( od.FileName );

					if ( res.loadedObject != null )
					{
						res.isSuccess = true;
						return res;
					}
					else
						return new() { isSuccess = false, isError = true, exceptionMsg = "HandleLoadFile()::Loaded object is null" };
				}

				return new() { isSuccess = false, isError = false };
			}
			catch ( Exception e )
			{
				return new() { isSuccess = false, exceptionMsg = e.Message, isError = true };
			}
		}

		public static FileOpResult HandleLoadTextFile( string filename, string filter, string title, string basePath )
		{
			try
			{
				OpenFileDialog od = new();
				od.InitialDirectory = basePath;
				od.FileName = filename;
				od.Filter = filter;
				od.Title = title;
				if ( od.ShowDialog() == true )
				{
					var res = new FileOpResult();
					res.basePath = Path.GetDirectoryName( od.FileName );
					res.fileName = od.SafeFileName;

					using ( StreamReader reader = new( od.FileName ) )
					{
						res.loadedObject = reader.ReadToEnd();
					}

					res.isSuccess = true;
					return res;
				}

				return new() { isSuccess = false, isError = false };
			}
			catch ( Exception e )
			{
				return new() { isSuccess = false, exceptionMsg = e.Message, isError = true };
			}

		}

		public static T UniqueCopy<T>( T copyFrom ) where T : class
		{
			string copy = JsonConvert.SerializeObject( copyFrom );
			return JsonConvert.DeserializeObject<T>( copy );
		}

		/// <summary>
		/// Tests a translated object's property to make sure it has a value, otherwise set it to the source prop's English value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The translated object to test</param>
		/// <param name="propName">Property name in the object to test</param>
		/// <param name="sourceUI">The source object (English) to use for a default value</param>
		public static void ValidateProperties<T>( T obj, string propName, dynamic sourceUI )
		{
			try
			{
				var props = typeof( T ).GetProperties();
				foreach ( var prop in props )
				{
					if ( string.IsNullOrEmpty( (string)prop.GetValue( obj ) ) )
					{
						Utils.Log( $"ValidateProperties()::Found missing property [{prop.Name}]" );
						Utils.missingUITranslations.Add( propName.ToLower() );
						Utils.missingUITranslations.Add( prop.Name.ToLower() );

						var sourceObjectValue = sourceUI.GetType().GetProperty( propName ).GetValue( sourceUI );
						var sourceValue = props.Where( x => x.Name == prop.Name ).FirstOrDefault().GetValue( sourceObjectValue );

						typeof( T ).GetProperty( prop.Name ).SetValue( obj, sourceValue );
					}
				}
			}
			catch ( Exception e )
			{
				MessageBox.Show( $"ValidateProperties()::Could not validate {propName}.\n{e.Message}", "App Exception", MessageBoxButton.OK, MessageBoxImage.Error );
			}
		}
	}
}
