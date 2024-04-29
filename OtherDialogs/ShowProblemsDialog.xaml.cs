using System.Windows;
using Imperial_Commander_Editor;

namespace Saga_Translator_V2
{
	/// <summary>
	/// Interaction logic for ShowProblemsDialog.xaml
	/// </summary>
	public partial class ShowProblemsDialog : Window
	{
		public ShowProblemsDialog( List<string> problems )
		{
			InitializeComponent();

			if ( Utils.missingTranslations.Count > 0 )
			{
				DataContext = new List<string>( new[] { $"Found missing translations. They will be highlighted in red inside the tree view." } ).Concat( problems );
			}
			else
				DataContext = problems;
		}

		private void continueBtn_Click( object sender, RoutedEventArgs e )
		{
			Close();
		}
	}
}
