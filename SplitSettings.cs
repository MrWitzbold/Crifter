using System;

namespace MSG
{
	// Token: 0x02000003 RID: 3
	public class SplitSettings
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00051726 File Offset: 0x0004F926
		public SplitSettings(int splitType, string str)
		{
			Console.WriteLine("Splitter: " + splitType);
			this.DoSplit(str);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0005174A File Offset: 0x0004F94A
		public bool Status()
		{
			return Convert.ToBoolean(1);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00051754 File Offset: 0x0004F954
		private void DoSplit(string str)
		{
			foreach (char value in str)
			{
				Console.Write(value);
			}
			Console.WriteLine();
		}
	}
}
