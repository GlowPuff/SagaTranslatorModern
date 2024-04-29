using System.Windows.Controls;
using Imperial_Commander_Editor;
using Newtonsoft.Json;

namespace Saga_Translator_V2
{
	public abstract class PanelBase : UserControl
	{
		//translation sources
		public dynamic sourceUI { get; set; }
		public dynamic translatedUI { get; set; }
		public CommonPanelData commonPanelData { get; set; }

		public PanelBase()
		{

		}

		virtual public void SetupUI<T>( TreeView mainTree )
		{
			var stringifiedJsonData = FileManager.LoadBuiltinJSON( commonPanelData.fileName );
			//set the ENGLISH SOURCE
			sourceUI = JsonConvert.DeserializeObject<T>( stringifiedJsonData );
			//set the TRANSLATED UI
			translatedUI = JsonConvert.DeserializeObject<T>( stringifiedJsonData );

			//generate main list headings
			PopulateUIMainTree();
			(mainTree.ItemContainerGenerator.Items[0] as TreeViewItem).IsSelected = true;
		}
		virtual public void PopulateUIMainTree() { }
		virtual public void OnOpenFile() { }
		virtual public void OnOpenFileCallback<T>( Action<T> callback )
		{
			commonPanelData.HandleLoad<T>( commonPanelData.fileName, callback.Invoke, OnVerifyOpenedFile );
		}
		virtual public void OnOpenTextFileCallback( Action<string> callback )
		{
			commonPanelData.HandleLoadText( commonPanelData.fileName, callback.Invoke );
		}
		virtual public void OnSaveFile() { commonPanelData.HandleSave( translatedUI ); }
		virtual public void OnSaveTextFile() { commonPanelData.HandleSaveText( translatedUI ); }
		virtual public void SetLanguage( string language ) { }
		virtual public FileOpResult OnVerifyOpenedFile( FileOpResult res ) { return res; }
		virtual public void debug() { }
	}
}
