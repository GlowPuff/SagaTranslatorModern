using System.Diagnostics;
using System.Windows;
using Saga_Translator_V2;

namespace Imperial_Commander_Editor
{
	public static class Utils
	{
		public const string formatVersion = "22";
		public const string appVersionString = "Version: 3.4";

		public static List<DeploymentCard> allyData { get; set; }
		public static List<DeploymentCard> enemyData { get; set; }//enemies + villains
		public static List<DeploymentCard> villainData { get; set; }
		public static List<DeploymentCard> heroData { get; set; }
		public static List<MissionNameData> missionNames { get; set; }

		//quick hack to keep track of missing translations in Mission properties
		public static HashSet<Guid> missingTranslations = new();
		public static HashSet<string> missingUITranslations = new();
		public static bool checkMissingTranslation = true;

		public static MainWindow mainWindow
		{
			get { return Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(); }
		}

		public static void InitUtils()
		{
			LoadCardData();//toon data

			//rewrite the mission names with the expansion ID
			missionNames = LoadMissionNames( "core" )
				.Concat( LoadMissionNames( "twin" ) )
				.Concat( LoadMissionNames( "hoth" ) )
				.Concat( LoadMissionNames( "bespin" ) )
				.Concat( LoadMissionNames( "jabba" ) )
				.Concat( LoadMissionNames( "empire" ) )
				.Concat( LoadMissionNames( "lothal" ) )
				.Concat( LoadMissionNames( "other" ) ).ToList();
		}

		public static void Log( string s )
		{
			Debug.WriteLine( s );
		}

		/// <summary>
		/// allies, enemies, villains, heroes card data
		/// </summary>
		public static void LoadCardData()
		{
			allyData = FileManager.LoadAsset<List<DeploymentCard>>( "allies.json" );
			enemyData = FileManager.LoadAsset<List<DeploymentCard>>( "enemies-short.json" );
			villainData = FileManager.LoadAsset<List<DeploymentCard>>( "villains-short.json" );
			heroData = FileManager.LoadAsset<List<DeploymentCard>>( "heroes.json" );

			enemyData = enemyData.Concat( villainData ).ToList();
		}

		private static List<MissionNameData> LoadMissionNames( string id )
		{
			return FileManager.LoadAsset<List<MissionNameData>>( $"{id}.json" )
				.Select( x => new MissionNameData() { id = x.id, name = $"({x.id.ToUpper()}) {x.name}" } ).ToList();
		}

		public static void ThrowErrorDialog( Exception e, string customMessage = null, string customTitle = null )
		{
			MessageBox.Show( $"{customMessage ?? "An error has occurred."}\r\n\r\nException:\r\n" + e.Message + "\r\n" + e.StackTrace, $"{customTitle ?? "App Exception"}", MessageBoxButton.OK, MessageBoxImage.Error );
		}

		public static void ShowError( string customMessage = null, string customTitle = null )
		{
			MessageBox.Show( $"{customMessage ?? "An error has occurred."}", $"{customTitle ?? "App Exception"}", MessageBoxButton.OK, MessageBoxImage.Error );
		}
	}
}
