using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using Imperial_Commander_Editor;

namespace Saga_Translator_V2
{
	public class CommonPanelData : ObservableObject
	{
		private string _windowTitle, _infoText;
		private bool _showLanguageSelector;

		public string windowTitle { get => _windowTitle; set { SetProperty( ref _windowTitle, value ); } }
		public string infoText { get => _infoText; set { SetProperty( ref _infoText, value ); } }

		public bool showLanguageSelector { get => _showLanguageSelector; set { SetProperty( ref _showLanguageSelector, value ); } }

		public bool hasSaved = false;
		public string basePath = FileManager.baseFolder;
		public string fileName;
		public string translationName;//used for open/save file dialog window title

		public CommonPanelData()
		{
		}

		public void HandleSave<T>( T translatedUI )
		{
			if ( !hasSaved )
			{
				var res = TranslatorUtils.HandleSaveFile( translatedUI, fileName, $"{translationName} (*.json)|*.json", "Save Translation", basePath );
				if ( res.isSuccess )
				{
					hasSaved = true;
					basePath = res.basePath;
					fileName = res.fileName;
					windowTitle = $"Saga Translator [{translationName}] - " + Path.Combine( basePath, fileName );
					Utils.mainWindow.SetStatus( "File Saved" );
				}
				else
				{
					Utils.mainWindow.SetStatus( "File Not Saved" );
				}
			}
			else
			{
				if ( FileManager.SaveJSON( translatedUI, fileName, basePath ) )
				{
					hasSaved = true;
					windowTitle = $"Saga Translator [{translationName}] - " + Path.Combine( basePath, fileName );
					Utils.mainWindow.SetStatus( "File Saved" );
				}
				else
				{
					Utils.mainWindow.SetStatus( "File Not Saved" );
					hasSaved = false;
				}
			}
		}

		public void HandleSaveText( string translatedUI )
		{
			if ( !hasSaved )
			{
				var res = TranslatorUtils.HandleSaveTextFile( translatedUI, fileName, $"{translationName} (*.txt)|*.txt", "Save Translation", basePath );
				if ( res.isSuccess )
				{
					hasSaved = true;
					basePath = res.basePath;
					fileName = res.fileName;
					windowTitle = $"Saga Translator [{translationName}] - " + Path.Combine( basePath, fileName );
					Utils.mainWindow.SetStatus( "File Saved" );
				}
				else
				{
					Utils.mainWindow.SetStatus( "File Not Saved" );
				}
			}
			else
			{
				if ( FileManager.SaveText( translatedUI, fileName, basePath ) )
				{
					hasSaved = true;
					windowTitle = $"Saga Translator [{translationName}] - " + Path.Combine( basePath, fileName );
					Utils.mainWindow.SetStatus( "File Saved" );
				}
				else
				{
					Utils.mainWindow.SetStatus( "File Not Saved" );
					hasSaved = false;
				}
			}
		}

		public void HandleLoad<T>( string filename, Action<T> callback, Func<FileOpResult, FileOpResult> verifyFile )
		{
			var res = TranslatorUtils.HandleLoadFile<T>( filename, $"{translationName} (*.json)|*.json", "Open Translated UI", basePath );

			//check the file for expected properties
			if ( res.isSuccess )
				res = verifyFile( res );

			if ( res.isSuccess )
			{
				hasSaved = true;
				basePath = res.basePath;
				fileName = res.fileName;
				windowTitle = $"Saga Translator [{translationName}] - " + Path.Combine( basePath, fileName );

				callback.Invoke( res.loadedObject );
				Utils.mainWindow.SetStatus( "File Loaded" );
			}
			else if ( res.isError )
			{
				Utils.ShowError( $"There was a problem opening the file.\nException:\n{res.exceptionMsg}", "Error Opening File" );
				Utils.mainWindow.SetStatus( "File Not Loaded" );
			}
		}

		public void HandleLoadText( string filename, Action<string> callback )
		{
			var res = TranslatorUtils.HandleLoadTextFile( filename, $"{translationName} (*.txt)|*.txt", "Open Translated UI", basePath );
			if ( res.isSuccess )
			{
				hasSaved = true;
				basePath = res.basePath;
				fileName = res.fileName;
				windowTitle = $"Saga Translator [{translationName}] - " + Path.Combine( basePath, fileName );
				Utils.mainWindow.SetStatus( "File Loaded" );

				callback.Invoke( res.loadedObject );
			}
			else if ( res.isError )
			{
				Utils.ShowError( $"There was a problem opening the file.\nException:\n{res.exceptionMsg}", "Error Opening File" );
				Utils.mainWindow.SetStatus( "File Not Loaded" );
			}
		}
	}
}
