namespace Imperial_Commander_Editor
{
	public class CardInstruction
	{
		public string instName, instID;
		public List<InstructionOption> content = new();
	}

	public class InstructionOption
	{
		public List<string> instruction = new();//line by line instructions
	}

	public class BonusEffect
	{
		public string bonusID;
		public List<string> effects = new();
	}
}