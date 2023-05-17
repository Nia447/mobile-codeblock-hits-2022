using System;
using System.Collections.Generic;

namespace Codeblock.Model
{
	public class ElderCodeBlock : Node
	{
		public List<List<Variable>> AreaVariable = new List<List<Variable>>();
		public List<CodeBlock> AreaFunctions = new List<CodeBlock>();
		public CodeBlock StartCodeBlock;
		public CodeBlock Main;
		public CodeBlock MainForCompilation;

		public ElderCodeBlock() : base()
		{
			AreaVariable.Add(new List<Variable>());
			StartCodeBlock = new CodeBlock(AreaVariable, AreaFunctions, true);
			Main = new CodeBlock(AreaVariable, AreaFunctions);
		}
		public void AddFunction()
		{
			AreaFunctions.Add(new CodeBlock(AreaVariable, AreaFunctions));
			AreaFunctions[AreaFunctions.Count - 1].Name = "Function " + AreaFunctions.Count;
		}
		public void AddVariable()
		{
			AreaVariable[0].Add(new Variable());
		}
		public void StartCompilation()
		{
			Console.WriteLine("CodeBlock: Starting Compilation...");
			StartCodeBlock.Compilation();
			Main.Compilation();
/*
            MainForCompilation = (CodeBlock)Main.Copy();
            MainForCompilation.Compilation();
*/
        }
	}
}
